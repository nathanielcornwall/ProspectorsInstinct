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

    public void Spawn(Vec3d desiredPosition)
    {
        Vec3d visiblePosition = FindVisiblePosition(desiredPosition);

        var particle = new SimpleParticleProperties
        {
            MinQuantity = 3,
            AddQuantity = 2,

            MinPos = visiblePosition,
            AddPos = new Vec3d(0.15, 0.15, 0.15),

            MinVelocity = new Vec3f(-0.02f, 0.04f, -0.02f),
            AddVelocity = new Vec3f(0.04f, 0.05f, 0.04f),

            LifeLength = 0.8f,
            GravityEffect = 0f,

            MinSize = 0.05f,
            MaxSize = 0.10f,

            Color = ColorUtil.ToRgba(220, 255, 230, 120),
            ParticleModel = EnumParticleModel.Quad
        };

        capi.World.SpawnParticles(particle);
    }

    public void SpawnLocated(Vec3d playerPosition)
    {
        double time = capi.World.ElapsedMilliseconds / 1000.0;
        double radius = 0.85;
        double orbitSpeed = 2.0;

        for (int i = 0; i < 6; i++)
        {
            double angle =
                (time * orbitSpeed) +
                i * Math.PI * 2 / 6;

            Vec3d position = new(
                playerPosition.X + Math.Cos(angle) * radius,
                playerPosition.Y + 0.15 + Math.Sin(time * 2 + i) * 0.08,
                playerPosition.Z + Math.Sin(angle) * radius
            );

            Spawn(position);
        }
    }

    private Vec3d FindVisiblePosition(Vec3d desiredPosition)
    {
        var player = capi.World.Player;

        if (player?.Entity == null)
        {
            return desiredPosition;
        }

        Vec3d eyePosition = player.Entity.Pos.XYZ.AddCopy(0, 1.5, 0);

        Vec3d difference = desiredPosition.SubCopy(eyePosition);
        double distance = difference.Length();

        if (distance <= 0.01)
        {
            return eyePosition;
        }

        Vec3d direction = difference.Normalize();
        const double stepSize = 0.20;

        Vec3d lastVisiblePosition = eyePosition.AddCopy(
            direction.X * 0.5,
            direction.Y * 0.5,
            direction.Z * 0.5
        );

        for (double travelled = 0.5; travelled <= distance; travelled += stepSize)
        {
            Vec3d testPosition = eyePosition.AddCopy(
                direction.X * travelled,
                direction.Y * travelled,
                direction.Z * travelled
            );

            if (IsInsideSolidBlock(testPosition))
            {
                return lastVisiblePosition;
            }

            lastVisiblePosition = testPosition;
        }

        return desiredPosition;
    }

    private bool IsInsideSolidBlock(Vec3d position)
    {
        BlockPos blockPos = new(
            (int)Math.Floor(position.X),
            (int)Math.Floor(position.Y),
            (int)Math.Floor(position.Z)
        );

        Block block = capi.World.BlockAccessor.GetBlock(blockPos);

        return block != null &&
               block.BlockMaterial != EnumBlockMaterial.Air;
    }
}