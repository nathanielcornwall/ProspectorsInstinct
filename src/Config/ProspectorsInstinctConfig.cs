using System.Collections.Generic;

namespace ProspectorsInstinct.Config;

public class ProspectorsInstinctConfig
{
    public bool Enabled { get; set; } = true;
    public bool DebugMode { get; set; } = false;
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
    ["Meteoric Iron"] = true,
    ["Bog Iron"] = true,

    ["Sphalerite"] = true,
    ["Smithsonite"] = true,
    ["Bismuthinite"] = true,
    ["Galena"] = true,
    ["Cerussite"] = true,
    ["Malachite"] = true,

    ["Gold"] = true,
    ["Silver"] = true,
    ["Uranium"] = true,

    ["Lignite"] = true,

    ["Quartz"] = true,
    ["Coal"] = false,
    ["Sulfur"] = true,
    ["Borax"] = false,
    ["Saltpeter"] = false
};
}