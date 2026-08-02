using System.Collections.Generic;
using System.Linq;
using ProspectorsInstinct.Metadata;

namespace ProspectorsInstinct.Config;

public class ProspectorsInstinctConfig
{
    public bool Enabled { get; set; } = true;
    public bool DebugMode { get; set; } = false;
    public bool RequireProspectingPick { get; set; } = true;
    public int ScanRadius { get; set; } = 8;
    public int ScanIntervalMs { get; set; } = 500;
    public float ParticleDensity { get; set; } = 1.0f;

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
        Enabled = Enabled,
        DebugMode = DebugMode,
        RequireProspectingPick = RequireProspectingPick,
        ScanRadius = ScanRadius,
        ScanIntervalMs = ScanIntervalMs,
        ParticleDensity = ParticleDensity,
        DetectOres = new Dictionary<string, bool>(DetectOres)
    };
}
public void CopyFrom(
    ProspectorsInstinctConfig source)
{
    Enabled = source.Enabled;
    DebugMode = source.DebugMode;
    RequireProspectingPick =
        source.RequireProspectingPick;
    ScanRadius = source.ScanRadius;
    ScanIntervalMs = source.ScanIntervalMs;
    ParticleDensity = source.ParticleDensity;

    DetectOres =
        new Dictionary<string, bool>(
            source.DetectOres);
}
}