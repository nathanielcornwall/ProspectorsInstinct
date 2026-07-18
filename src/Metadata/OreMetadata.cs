namespace ProspectorsInstinct.Metadata;

public class OreMetadata
{
    /// <summary>
    /// Runtime identifier from the Ore Database.
    /// Example: nativegold
    /// </summary>
    public required string RuntimeName { get; init; }

    /// <summary>
    /// Human-readable name shown in the config.
    /// Example: Gold
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    /// Category used for grouping.
    /// </summary>
    public OreCategory Category { get; init; } = OreCategory.Misc;

    /// <summary>
    /// Whether the ore should be enabled in a newly created config.
    /// </summary>
    public bool EnabledByDefault { get; init; } = true;
}