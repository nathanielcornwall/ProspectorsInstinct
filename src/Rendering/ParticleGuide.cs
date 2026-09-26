using System;
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
        double distance = double.MaxValue)
    {
        Vec3d visiblePosition =
            FindVisiblePosition(desiredPosition);

        float density = Math.Clamp(
            ProspectorsInstinctModSystem.Config.ParticleDensity,
            0.1f,
            2.0f
        );

        float size = Math.Clamp(
            ProspectorsInstinctModSystem.Config.ParticleSize,
            0.5f,
            2.0f
        );

        float proximityStrength =
            CalculateProximityStrength(distance);

        float responsiveSize =
            size * proximityStrength;

        float responsiveLife =
            0.8f * proximityStrength;

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

            Color = ColorUtil.ToRgba(
                220,
                255,
                230,
                120
            ),

            ParticleModel = EnumParticleModel.Quad
        };

        capi.World.SpawnParticles(particle);
    }

    public void SpawnLocated(Vec3d playerPosition)
    {
        double time =
            capi.World.ElapsedMilliseconds / 1000.0;

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

            SpawnLocatedParticle(position);
        }
    }

    private void SpawnLocatedParticle(
        Vec3d position)
    {
        float density = Math.Clamp(
            ProspectorsInstinctModSystem.Config.ParticleDensity,
            0.1f,
            2.0f
        );

        float size = Math.Clamp(
            ProspectorsInstinctModSystem.Config.ParticleSize,
            0.5f,
            2.0f
        );

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

            LifeLength = 0.9f,
            GravityEffect = 0f,

            MinSize = 0.06f * size,
            MaxSize = 0.11f * size,

            Color = ColorUtil.ToRgba(
                220,
                255,
                230,
                120
            ),

            ParticleModel = EnumParticleModel.Quad
        };

        capi.World.SpawnParticles(particle);
    }

    private static float CalculateProximityStrength(
        double distance)
    {
        if (double.IsInfinity(distance) ||
            distance == double.MaxValue)
        {
            return 1.0f;
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

        return 1.0f +
               (float)normalized * 0.35f;
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

            if (IsInsideSolidBlock(testPosition))
            {
                return lastVisiblePosition;
            }

            lastVisiblePosition =
                testPosition;
        }

        return desiredPosition;
    }

    private bool IsInsideSolidBlock(
        Vec3d position)
    {
        BlockPos blockPos = new(
            (int)Math.Floor(position.X),
            (int)Math.Floor(position.Y),
            (int)Math.Floor(position.Z)
        );

        Block block =
            capi.World.BlockAccessor
                .GetBlock(blockPos);

        return block != null &&
               block.BlockMaterial !=
               EnumBlockMaterial.Air;
    }
}