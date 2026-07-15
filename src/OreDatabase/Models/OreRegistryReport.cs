using System;
using System.Collections.Generic;

namespace ProspectorsInstinct.OreDatabase.Models;

public sealed class OreRegistryReport
{
    public int SchemaVersion { get; init; } = 1;

    public DateTime GeneratedUtc { get; init; }

    public string GameVersion { get; init; } = string.Empty;

    public int TotalRegisteredBlocks { get; init; }

    public int CandidateCount { get; set; }

    public List<OreCandidate> Candidates { get; init; } = new();
}