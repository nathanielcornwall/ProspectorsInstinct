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
        var particle = new SimpleParticleProperties
        {
            MinQuantity = 3,
            AddQuantity = 2,

            MinPos = position,
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
}