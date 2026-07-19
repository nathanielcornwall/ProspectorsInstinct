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

    /// <summary>
    /// Indicates this metadata entry exists only as an alias
    /// and is not expected to appear in the runtime ore database.
    /// </summary>
    public bool IsAlias { get; init; } = false;

    /// <summary>
    /// Alternative names that should resolve to this ore.
    /// </summary>

}