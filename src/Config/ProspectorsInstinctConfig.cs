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
}