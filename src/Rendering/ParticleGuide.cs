using System;
using System.Linq;
using ProspectorsInstinct.Metadata;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace ProspectorsInstinct.Rendering;

public class ParticleGuide
{
    private readonly ICoreClientAPI capi;

    public ParticleGuide(ICoreClientAPI capi)
    {
        this.capi = capi;
    }

    public void Spawn(
        Vec3d desiredPosition,
        double distance = double.MaxValue, string? oreName = null)
    {
        Vec3d visiblePosition =
            FindVisiblePosition(desiredPosition);


        var config = ProspectorsInstinctModSystem.Config;
        float density = config.ParticleDensity;
        float size = config.ParticleSize;
        bool highVisibility = config.HighVisibilityParticles;
        int color = GetColor(oreName, highVisibility ? 200 : 120);
        float proximity = CalculateProximity(distance);
        float proximityStrength = 1f + proximity * 0.35f;
        density *= 0.85f + proximity * 0.15f;

        float responsiveSize =
            size * proximityStrength;

        float responsiveLife =
            0.8f * proximityStrength;

        if (highVisibility)
        {
            responsiveLife *= 1.35f;
        }

        var particle = new SimpleParticleProperties
        {
            MinQuantity = 3 * density,
            AddQuantity = 2 * density,

            MinPos = visiblePosition,
            AddPos = new Vec3d(
                0.15,
                0.15,
                0.15
            ),

            MinVelocity = new Vec3f(
                -0.02f,
                0.04f,
                -0.02f
            ),

            AddVelocity = new Vec3f(
                0.04f,
                0.05f,
                0.04f
            ),

            LifeLength = responsiveLife,
            GravityEffect = 0f,

            MinSize = 0.05f * responsiveSize,
            MaxSize = 0.10f * responsiveSize,

            Color = color,

            ParticleModel = EnumParticleModel.Quad
        };

        capi.World.SpawnParticles(particle);
    }

    public void SpawnLocated(Vec3d playerPosition, string? oreName = null)
    {
        double time =
            capi.World.ElapsedMilliseconds / 1000.0;

        var config = ProspectorsInstinctModSystem.Config;
        int color = GetColor(oreName, config.HighVisibilityParticles ? 200 : 120);
        const double radius = 1.0;
        const double orbitSpeed = 1.5;
        const int orbitPoints = 4;

        for (int i = 0; i < orbitPoints; i++)
        {
            double angle =
                (time * orbitSpeed) +
                i * Math.PI * 2 / orbitPoints;

            Vec3d position = new(
                playerPosition.X +
                    Math.Cos(angle) * radius,

                playerPosition.Y +
                    0.20 +
                    Math.Sin(time * 1.5 + i) * 0.06,

                playerPosition.Z +
                    Math.Sin(angle) * radius
            );

            SpawnLocatedParticle(position, color, config.ParticleDensity,
                config.ParticleSize, config.HighVisibilityParticles);
        }
    }

    private void SpawnLocatedParticle(
        Vec3d position, int color, float density, float size, bool highVisibility)
    {

        float lifeLength =
            highVisibility
                ? 1.2f
                : 0.9f;

        var particle = new SimpleParticleProperties
        {
            MinQuantity = 1 * density,
            AddQuantity = 1 * density,

            MinPos = position,

            AddPos = new Vec3d(
                0.08,
                0.08,
                0.08
            ),

            MinVelocity = new Vec3f(
                -0.01f,
                0.025f,
                -0.01f
            ),

            AddVelocity = new Vec3f(
                0.02f,
                0.025f,
                0.02f
            ),

            LifeLength = lifeLength,
            GravityEffect = 0f,

            MinSize = 0.06f * size,
            MaxSize = 0.11f * size,

            Color = color,

            ParticleModel = EnumParticleModel.Quad
        };

        capi.World.SpawnParticles(particle);
    }

    private static int GetColor(string? oreName, int alpha)
    {
        OreCategory? category = null;
        if (ProspectorsInstinctModSystem.Config.ResourceCategoryColors && oreName != null)
        {
            category = OreMetadataProvider.GetAll().FirstOrDefault(ore =>
                string.Equals(ore.DisplayName, oreName, StringComparison.OrdinalIgnoreCase))?.Category;
        }

        var (red, green, blue) = category switch
        {
            OreCategory.Metal => (240, 167, 116),
            OreCategory.PreciousMetal => (255, 208, 130),
            OreCategory.Industrial => (160, 206, 230),
            OreCategory.Chemical => (177, 222, 137),
            OreCategory.Fuel => (215, 174, 165),
            OreCategory.Gemstone => (214, 165, 238),
            _ => (255, 230, 120)
        };

        return ColorUtil.ToRgba(alpha, red, green, blue);
    }

    private static float CalculateProximity(
        double distance)
    {
        if (!double.IsFinite(distance))
        {
            return 0f;
        }

        const double farDistance = 12.0;
        const double nearDistance = 2.0;

        double normalized =
            (farDistance - distance) /
            (farDistance - nearDistance);

        normalized = Math.Clamp(
            normalized,
            0.0,
            1.0
        );

        return (float)(normalized * normalized * (3 - 2 * normalized));
    }

    private Vec3d FindVisiblePosition(
        Vec3d desiredPosition)
    {
        var player = capi.World.Player;

        if (player?.Entity == null)
        {
            return desiredPosition;
        }

        Vec3d eyePosition =
            player.Entity.Pos.XYZ.AddCopy(
                0,
                1.5,
                0
            );

        Vec3d difference =
            desiredPosition.SubCopy(
                eyePosition
            );

        double distance =
            difference.Length();

        if (distance <= 0.01)
        {
            return eyePosition;
        }

        Vec3d direction =
            difference.Normalize();
        BlockPos sample = player.Entity.Pos.AsBlockPos;
        BlockPos previous = sample.Copy();
        bool hasPrevious = false;
        var accessor = capi.World.BlockAccessor;

        const double stepSize = 0.20;

        Vec3d lastVisiblePosition =
            eyePosition.AddCopy(
                direction.X * 0.5,
                direction.Y * 0.5,
                direction.Z * 0.5
            );

        for (
            double travelled = 0.5;
            travelled <= distance;
            travelled += stepSize)
        {
            Vec3d testPosition =
                eyePosition.AddCopy(
                    direction.X * travelled,
                    direction.Y * travelled,
                    direction.Z * travelled
                );

            sample.Set((int)Math.Floor(testPosition.X),
                (int)Math.Floor(testPosition.Y), (int)Math.Floor(testPosition.Z));
            if (!hasPrevious || !sample.Equals(previous))
            {
                var block = accessor.GetBlock(sample);
                if (block != null && block.BlockMaterial != EnumBlockMaterial.Air)
                {
                    return lastVisiblePosition;
                }

                previous.Set(sample.X, sample.Y, sample.Z);
                hasPrevious = true;
            }

            lastVisiblePosition =
                testPosition;
        }

        return desiredPosition;
    }

}
