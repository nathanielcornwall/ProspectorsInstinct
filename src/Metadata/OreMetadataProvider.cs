using System.Collections.Generic;

namespace ProspectorsInstinct.Metadata;

public static class OreMetadataProvider
{
    private static readonly Dictionary<string, OreMetadata> metadata =
        new()
        {
            ["nativecopper"] = new OreMetadata
            {
                RuntimeName = "nativecopper",
                DisplayName = "Native Copper",
                Category = OreCategory.Metal
            },

            ["nativegold"] = new OreMetadata
            {
                RuntimeName = "nativegold",
                DisplayName = "Gold",
                Category = OreCategory.PreciousMetal
            },

            ["nativesilver"] = new OreMetadata
            {
                RuntimeName = "nativesilver",
                DisplayName = "Silver",
                Category = OreCategory.PreciousMetal
            }
        };
             public static OreMetadata Get(string runtimeName)
    {
        if (metadata.TryGetValue(runtimeName.ToLowerInvariant(), out var ore))
        {
            return ore;
        }

        return new OreMetadata
        {
            RuntimeName = runtimeName,
            DisplayName = runtimeName,
            Category = OreCategory.Misc
        };
    }

    public static IReadOnlyCollection<OreMetadata> GetAll()
    {
        return metadata.Values;
    }
}