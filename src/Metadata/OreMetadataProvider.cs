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

            ["anthracite"] = new OreMetadata
{
    RuntimeName = "anthracite",
    DisplayName = "Anthracite",
    Category = OreCategory.Fuel
},

["bituminouscoal"] = new OreMetadata
{
    RuntimeName = "bituminouscoal",
    DisplayName = "Bituminous Coal",
    Category = OreCategory.Fuel
},

["graphite"] = new OreMetadata
{
    RuntimeName = "graphite",
    DisplayName = "Graphite",
    Category = OreCategory.Industrial
},

["fluorite"] = new OreMetadata
{
    RuntimeName = "fluorite",
    DisplayName = "Fluorite",
    Category = OreCategory.Industrial
},

["phosphorite"] = new OreMetadata
{
    RuntimeName = "phosphorite",
    DisplayName = "Phosphorite",
    Category = OreCategory.Industrial
},

["alum"] = new OreMetadata
{
    RuntimeName = "alum",
    DisplayName = "Alum",
    Category = OreCategory.Chemical
},

["kernite"] = new OreMetadata
{
    RuntimeName = "kernite",
    DisplayName = "Kernite",
    Category = OreCategory.Chemical
},

["sylvite"] = new OreMetadata
{
    RuntimeName = "sylvite",
    DisplayName = "Sylvite",
    Category = OreCategory.Chemical
},

["seabed"] = new OreMetadata
{
    RuntimeName = "seabed",
    DisplayName = "Seabed Deposit",
    Category = OreCategory.Misc
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

            ,

            ["berylaquamarine"] = new OreMetadata
{
    RuntimeName = "berylaquamarine",
    DisplayName = "Aquamarine",
    Category = OreCategory.Gemstone
},

["berylbixbite"] = new OreMetadata
{
    RuntimeName = "berylbixbite",
    DisplayName = "Bixbite",
    Category = OreCategory.Gemstone
},

["corundum"] = new OreMetadata
{
    RuntimeName = "corundum",
    DisplayName = "Corundum",
    Category = OreCategory.Gemstone
},

["corundumruby"] = new OreMetadata
{
    RuntimeName = "corundumruby",
    DisplayName = "Ruby",
    Category = OreCategory.Gemstone
},

["corundumsapphire"] = new OreMetadata
{
    RuntimeName = "corundumsapphire",
    DisplayName = "Sapphire",
    Category = OreCategory.Gemstone
},

["diamond"] = new OreMetadata
{
    RuntimeName = "diamond",
    DisplayName = "Diamond",
    Category = OreCategory.Gemstone
},

["emerald"] = new OreMetadata
{
    RuntimeName = "emerald",
    DisplayName = "Emerald",
    Category = OreCategory.Gemstone
},

["flint"] = new OreMetadata
{
    RuntimeName = "flint",
    DisplayName = "Flint",
    Category = OreCategory.Misc
},

["garnetalmandine"] = new OreMetadata
{
    RuntimeName = "garnetalmandine",
    DisplayName = "Almandine Garnet",
    Category = OreCategory.Gemstone
},

["garnetandradite"] = new OreMetadata
{
    RuntimeName = "garnetandradite",
    DisplayName = "Andradite Garnet",
    Category = OreCategory.Gemstone
},

["garnetgrossular"] = new OreMetadata
{
    RuntimeName = "garnetgrossular",
    DisplayName = "Grossular Garnet",
    Category = OreCategory.Gemstone
},

["garnetpyrope"] = new OreMetadata
{
    RuntimeName = "garnetpyrope",
    DisplayName = "Pyrope Garnet",
    Category = OreCategory.Gemstone
},

["garnetspessartine"] = new OreMetadata
{
    RuntimeName = "garnetspessartine",
    DisplayName = "Spessartine Garnet",
    Category = OreCategory.Gemstone
},

["garnetuvarovite"] = new OreMetadata
{
    RuntimeName = "garnetuvarovite",
    DisplayName = "Uvarovite Garnet",
    Category = OreCategory.Gemstone
},

["lapislazuli"] = new OreMetadata
{
    RuntimeName = "lapislazuli",
    DisplayName = "Lapis Lazuli",
    Category = OreCategory.Gemstone
},

["olivine"] = new OreMetadata
{
    RuntimeName = "olivine",
    DisplayName = "Olivine",
    Category = OreCategory.Gemstone
},

["olivine_peridot"] = new OreMetadata
{
    RuntimeName = "olivine_peridot",
    DisplayName = "Peridot",
    Category = OreCategory.Gemstone
},

["spinelred"] = new OreMetadata
{
    RuntimeName = "spinelred",
    DisplayName = "Red Spinel",
    Category = OreCategory.Gemstone
},

["topazamber"] = new OreMetadata
{
    RuntimeName = "topazamber",
    DisplayName = "Amber Topaz",
    Category = OreCategory.Gemstone
},

["topazblue"] = new OreMetadata
{
    RuntimeName = "topazblue",
    DisplayName = "Blue Topaz",
    Category = OreCategory.Gemstone
},

["topazpink"] = new OreMetadata
{
    RuntimeName = "topazpink",
    DisplayName = "Pink Topaz",
    Category = OreCategory.Gemstone
},

["tourmalinerubellite"] = new OreMetadata
{
    RuntimeName = "tourmalinerubellite",
    DisplayName = "Rubellite",
    Category = OreCategory.Gemstone
},

["tourmalineschorl"] = new OreMetadata
{
    RuntimeName = "tourmalineschorl",
    DisplayName = "Schorl",
    Category = OreCategory.Gemstone
},

["tourmalineverdelite"] = new OreMetadata
{
    RuntimeName = "tourmalineverdelite",
    DisplayName = "Verdelite",
    Category = OreCategory.Gemstone
},

["tourmalinewatermelon"] = new OreMetadata
{
    RuntimeName = "tourmalinewatermelon",
    DisplayName = "Watermelon Tourmaline",
    Category = OreCategory.Gemstone
},
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