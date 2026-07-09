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

    public void Spawn(Vec3d position)
    {
        Vec3d visiblePosition = FindVisiblePosition(position);

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
        double angle = (time * orbitSpeed) + i * Math.PI * 2 / 6;

        Vec3d position = new(
            playerPosition.X + Math.Cos(angle) * radius,
            playerPosition.Y + 0.15 + Math.Sin(time * 2 + i) * 0.08,
            playerPosition.Z + Math.Sin(angle) * radius
        );

        Spawn(position);
    }
}

    private Vec3d FindVisiblePosition(Vec3d position)
    {
        Vec3d testPosition = position.Clone();

        for (int i = 0; i < 6; i++)
        {
            if (!IsInsideSolidBlock(testPosition))
            {
                return testPosition;
            }

            testPosition.Y += 0.5;
        }

        return position;
    }

    private bool IsInsideSolidBlock(Vec3d position)
    {
        BlockPos blockPos = new(
            (int)position.X,
            (int)position.Y,
            (int)position.Z
        );

        Block block = capi.World.BlockAccessor.GetBlock(blockPos);

        return block != null && block.BlockMaterial != EnumBlockMaterial.Air;
    }
}