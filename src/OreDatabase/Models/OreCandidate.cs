using System.Collections.Generic;

namespace ProspectorsInstinct.OreDatabase.Models;

public sealed class OreCandidate
{
    public int BlockId { get; init; }

    public string Code { get; init; } = string.Empty;

    public string Domain { get; init; } = string.Empty;

    public string Path { get; init; } = string.Empty;

    public string ClassName { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public Dictionary<string, string> Variants { get; init; } = new();

    public List<string> Drops { get; init; } = new();
}