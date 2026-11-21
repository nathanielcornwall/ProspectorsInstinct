using System;
using System.Collections.Generic;
using System.Linq;
using ProspectorsInstinct.Metadata;

namespace ProspectorsInstinct.Config;

public enum ParticlePreset
{
    Custom,
    Subtle,
    Balanced,
    HighVisibility
}

public class ProspectorsInstinctConfig
{
    public const int CurrentConfigVersion = 2;

    // Missing version fields in pre-0.9.4 configs deserialize as zero.
    public int ConfigVersion { get; set; }

    public bool Normalize()
    {
        bool changed = false;
        if (ConfigVersion < CurrentConfigVersion)
        {
            ConfigVersion = CurrentConfigVersion;
            changed = true;
        }

        int radius = Math.Clamp(ScanRadius, 1, 12);
        int interval = Math.Clamp(ScanIntervalMs, 100, 60000);
        float density = float.IsFinite(ParticleDensity) ? Math.Clamp(ParticleDensity, 0.1f, 2f) : 1f;
        float size = float.IsFinite(ParticleSize) ? Math.Clamp(ParticleSize, 0.5f, 2f) : 1f;
        changed |= radius != ScanRadius || interval != ScanIntervalMs
            || density != ParticleDensity || size != ParticleSize;
        ScanRadius = radius;
        ScanIntervalMs = interval;
        ParticleDensity = density;
        ParticleSize = size;

        var preset = Enum.IsDefined(Preset) ? Preset : ParticlePreset.Custom;
        var previous = (Preset, ParticleDensity, ParticleSize, HighVisibilityParticles);
        ApplyPreset(preset);
        changed |= previous != (Preset, ParticleDensity, ParticleSize, HighVisibilityParticles);

        if (DetectOres == null)
        {
            DetectOres = new Dictionary<string, bool>();
            changed = true;
        }

        foreach (var ore in OreMetadataProvider.GetAll())
        {
            changed |= DetectOres.TryAdd(ore.DisplayName, ore.EnabledByDefault);
        }

        return changed;
    }

    public bool Enabled { get; set; } = true;
    public bool DebugMode { get; set; } = false;
    public bool RequireProspectingPick { get; set; } = true;
    public bool HighVisibilityParticles { get; set; } = false;

    public ParticlePreset Preset { get; set; }

    public void ApplyPreset(ParticlePreset preset)
    {
        Preset = preset;
        (ParticleDensity, ParticleSize, HighVisibilityParticles) = preset switch
        {
            ParticlePreset.Subtle => (0.6f, 0.8f, false),
            ParticlePreset.Balanced => (1f, 1f, false),
            ParticlePreset.HighVisibility => (1.4f, 1.3f, true),
            _ => (ParticleDensity, ParticleSize, HighVisibilityParticles)
        };
    }

    public bool ResourceCategoryColors { get; set; }

    public int ScanRadius { get; set; } = 8;
    public int ScanIntervalMs { get; set; } = 500;

    public float ParticleDensity { get; set; } = 1.0f;
    public float ParticleSize { get; set; } = 1.0f;

    public Dictionary<string, bool> DetectOres { get; set; } =
        OreMetadataProvider
            .GetAll()
            .ToDictionary(
                ore => ore.DisplayName,
                ore => ore.EnabledByDefault
            );

    public ProspectorsInstinctConfig Clone()
    {
        return new ProspectorsInstinctConfig
        {
            ConfigVersion = ConfigVersion,
            Enabled = Enabled,
            DebugMode = DebugMode,
            RequireProspectingPick = RequireProspectingPick,
            HighVisibilityParticles = HighVisibilityParticles,
            Preset = Preset,
            ResourceCategoryColors = ResourceCategoryColors,
            ScanRadius = ScanRadius,
            ScanIntervalMs = ScanIntervalMs,
            ParticleDensity = ParticleDensity,
            ParticleSize = ParticleSize,
            DetectOres = new Dictionary<string, bool>(DetectOres)
        };
    }

    public void CopyFrom(ProspectorsInstinctConfig source)
    {
        ConfigVersion = source.ConfigVersion;
        Enabled = source.Enabled;
        DebugMode = source.DebugMode;
        RequireProspectingPick = source.RequireProspectingPick;
        HighVisibilityParticles = source.HighVisibilityParticles;
        Preset = source.Preset;
        ResourceCategoryColors = source.ResourceCategoryColors;
        ScanRadius = source.ScanRadius;
        ScanIntervalMs = source.ScanIntervalMs;
        ParticleDensity = source.ParticleDensity;
        ParticleSize = source.ParticleSize;

        DetectOres =
            new Dictionary<string, bool>(
                source.DetectOres
            );
    }
}
