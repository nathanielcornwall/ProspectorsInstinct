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

            ["azurite"] = new OreMetadata
            {
                RuntimeName = "azurite",
                DisplayName = "Azurite",
                Category = OreCategory.Metal
            },

            ["chalcopyrite"] = new OreMetadata
            {
                RuntimeName = "chalcopyrite",
                DisplayName = "Chalcopyrite",
                Category = OreCategory.Metal
            },

            ["chromite"] = new OreMetadata
            {
                RuntimeName = "chromite",
                DisplayName = "Chromite",
                Category = OreCategory.Metal
            },

            ["chalcocite"] = new OreMetadata
            {
                RuntimeName = "chalcocite",
                DisplayName = "Chalcocite",
                Category = OreCategory.Metal
            },

            ["freibergite"] = new OreMetadata
            {
                RuntimeName = "freibergite",
                DisplayName = "Freibergite",
                Category = OreCategory.Metal
            },

            ["ilmenite"] = new OreMetadata
            {
                RuntimeName = "ilmenite",
                DisplayName = "Ilmenite",
                Category = OreCategory.Metal
            },

            ["pentlandite"] = new OreMetadata
            {
                RuntimeName = "pentlandite",
                DisplayName = "Pentlandite",
                Category = OreCategory.Metal
            },

            ["teallite"] = new OreMetadata
            {
                RuntimeName = "teallite",
                DisplayName = "Teallite",
                Category = OreCategory.Metal
            },

            ["tetrahedrite"] = new OreMetadata
            {
                RuntimeName = "tetrahedrite",
                DisplayName = "Tetrahedrite",
                Category = OreCategory.Metal
            },

            ["pyrite"] = new OreMetadata
            {
                RuntimeName = "pyrite",
                DisplayName = "Pyrite",
                Category = OreCategory.Industrial
            },

            ["franckeite"] = new OreMetadata
            {
                RuntimeName = "franckeite",
                DisplayName = "Franckeite",
                Category = OreCategory.Industrial
            },

            ["hemimorphite"] = new OreMetadata
            {
                RuntimeName = "hemimorphite",
                DisplayName = "Hemimorphite",
                Category = OreCategory.Industrial
            },

            ["rhodochrosite"] = new OreMetadata
            {
                RuntimeName = "rhodochrosite",
                DisplayName = "Rhodochrosite",
                Category = OreCategory.Industrial
            },

            ["vanadinite"] = new OreMetadata
            {
                RuntimeName = "vanadinite",
                DisplayName = "Vanadinite",
                Category = OreCategory.Industrial
            },

            ["wulfenite"] = new OreMetadata
            {
                RuntimeName = "wulfenite",
                DisplayName = "Wulfenite",
                Category = OreCategory.Industrial
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

            ["galena_nativesilver"] = new OreMetadata
            {
                RuntimeName = "galena_nativesilver",
                DisplayName = "Silver-Bearing Galena",
                Category = OreCategory.PreciousMetal
            },

            ["nativeplatinum"] = new OreMetadata
            {
                RuntimeName = "nativeplatinum",
                DisplayName = "Native Platinum",
                Category = OreCategory.PreciousMetal
            },

            ["quartz_nativegold"] = new OreMetadata
            {
                RuntimeName = "quartz_nativegold",
                DisplayName = "Gold-Bearing Quartz",
                Category = OreCategory.PreciousMetal
            },

            ["quartz_nativesilver"] = new OreMetadata
            {
                RuntimeName = "quartz_nativesilver",
                DisplayName = "Silver-Bearing Quartz",
                Category = OreCategory.PreciousMetal
            },

            ["sperrylite"] = new OreMetadata
            {
                RuntimeName = "sperrylite",
                DisplayName = "Sperrylite",
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
        if (metadata.TryGetValue(
                runtimeName.ToLowerInvariant(),
                out OreMetadata? ore))
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