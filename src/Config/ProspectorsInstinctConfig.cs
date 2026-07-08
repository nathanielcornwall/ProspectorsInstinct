using System.Collections.Generic;

namespace ProspectorsInstinct.Config;

public class ProspectorsInstinctConfig
{
    public bool Enabled { get; set; } = true;
    public bool DebugMode { get; set; } = true;
    public bool RequireProspectingPick { get; set; } = true;
    public int ScanRadius { get; set; } = 8;
    public int ScanIntervalMs { get; set; } = 500;
    public float ParticleDensity { get; set; } = 1.0f;

    public Dictionary<string, bool> DetectOres { get; set; } = new()
    {
        ["Native Copper"] = true,
        ["Cassiterite"] = true,
        ["Hematite"] = true,
        ["Limonite"] = true,
        ["Magnetite"] = true,
        ["Sphalerite"] = true,
        ["Bismuthinite"] = true,
        ["Galena"] = true,
        ["Malachite"] = true,
        ["Gold"] = true,
        ["Silver"] = true,
        ["Quartz"] = false,
        ["Coal"] = false,
        ["Sulfur"] = false,
        ["Borax"] = false,
        ["Saltpeter"] = false,
        ["Uranium"] = true
    };
}