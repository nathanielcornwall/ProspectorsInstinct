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

            ["cassiterite"] = new OreMetadata
            {
                RuntimeName = "cassiterite",
                DisplayName = "Cassiterite",
                Category = OreCategory.Metal
            },

            ["hematite"] = new OreMetadata
            {
                RuntimeName = "hematite",
                DisplayName = "Hematite",
                Category = OreCategory.Metal
            },

            ["limonite"] = new OreMetadata
            {
                RuntimeName = "limonite",
                DisplayName = "Limonite",
                Category = OreCategory.Metal
            },

            ["magnetite"] = new OreMetadata
            {
                RuntimeName = "magnetite",
                DisplayName = "Magnetite",
                Category = OreCategory.Metal
            },

            ["meteoriciron"] = new OreMetadata
            {
                RuntimeName = "meteoriciron",
                DisplayName = "Meteoric Iron",
                Category = OreCategory.Metal
            },

            ["bogiron"] = new OreMetadata
            {
                RuntimeName = "bogiron",
                DisplayName = "Bog Iron",
                Category = OreCategory.Metal
            },

            ["sphalerite"] = new OreMetadata
            {
                RuntimeName = "sphalerite",
                DisplayName = "Sphalerite",
                Category = OreCategory.Metal
            },

            ["smithsonite"] = new OreMetadata
            {
                RuntimeName = "smithsonite",
                DisplayName = "Smithsonite",
                Category = OreCategory.Metal
            },

            ["bismuthinite"] = new OreMetadata
            {
                RuntimeName = "bismuthinite",
                DisplayName = "Bismuthinite",
                Category = OreCategory.Metal
            },

            ["galena"] = new OreMetadata
            {
                RuntimeName = "galena",
                DisplayName = "Galena",
                Category = OreCategory.Metal
            },

            ["cerussite"] = new OreMetadata
            {
                RuntimeName = "cerussite",
                DisplayName = "Cerussite",
                Category = OreCategory.Metal
            },

            ["malachite"] = new OreMetadata
            {
                RuntimeName = "malachite",
                DisplayName = "Malachite",
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
            },

            ["uranium"] = new OreMetadata
            {
                RuntimeName = "uranium",
                DisplayName = "Uranium",
                Category = OreCategory.Fuel
            },

            ["lignite"] = new OreMetadata
            {
                RuntimeName = "lignite",
                DisplayName = "Lignite",
                Category = OreCategory.Fuel
            },

            ["quartz"] = new OreMetadata
            {
                RuntimeName = "quartz",
                DisplayName = "Quartz",
                Category = OreCategory.Industrial
            },

            ["coal"] = new OreMetadata
            {
                RuntimeName = "coal",
                DisplayName = "Coal",
                Category = OreCategory.Fuel,
                EnabledByDefault = false
            },

            ["sulfur"] = new OreMetadata
            {
                RuntimeName = "sulfur",
                DisplayName = "Sulfur",
                Category = OreCategory.Chemical
            },

            ["borax"] = new OreMetadata
            {
                RuntimeName = "borax",
                DisplayName = "Borax",
                Category = OreCategory.Chemical,
                EnabledByDefault = false
            },

            ["saltpeter"] = new OreMetadata
            {
                RuntimeName = "saltpeter",
                DisplayName = "Saltpeter",
                Category = OreCategory.Chemical,
                EnabledByDefault = false
            }
        };

    public static OreMetadata Get(string runtimeName)
    {
        if (metadata.TryGetValue(runtimeName.ToLowerInvariant(), out OreMetadata? ore))
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