using System;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.MathTools;

namespace ProspectorsInstinct.Detection;

public class OreDetector
{
    private readonly ICoreClientAPI capi;

    private static readonly Dictionary<string, string[]> OreAliases =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Native Copper"] = new[]
            {
                "nativecopper",
                "copper"
            },

            ["Cassiterite"] = new[]
            {
                "cassiterite"
            },

            ["Hematite"] = new[]
            {
                "hematite"
            },

            ["Limonite"] = new[]
            {
                "limonite"
            },

            ["Magnetite"] = new[]
            {
                "magnetite"
            },

            ["Meteoric Iron"] = new[]
            {
                "meteoriciron",
                "meteoriteiron",
                "ironmeteorite",
                "meteorite"
            },

            ["Bog Iron"] = new[]
            {
                "bogiron",
                "ironbog"
            },

            ["Sphalerite"] = new[]
            {
                "sphalerite"
            },

            ["Smithsonite"] = new[]
            {
                "smithsonite"
            },

            ["Bismuthinite"] = new[]
            {
                "bismuthinite"
            },

            ["Galena"] = new[]
            {
                "galena"
            },

            ["Cerussite"] = new[]
            {
                "cerussite"
            },

            ["Malachite"] = new[]
            {
                "malachite"
            },

            ["Gold"] = new[]
            {
                "gold"
            },

            ["Silver"] = new[]
            {
                "silver"
            },

            ["Uranium"] = new[]
            {
                "uranium"
            },

            ["Lignite"] = new[]
            {
                "lignite",
                "browncoal"
            },

            ["Quartz"] = new[]
            {
                "quartz"
            },

            ["Coal"] = new[]
            {
                "coal",
                "bituminous"
            },

            ["Sulfur"] = new[]
            {
                "sulfur",
                "sulphur"
            },

            ["Borax"] = new[]
            {
                "borax"
            },

            ["Saltpeter"] = new[]
            {
                "saltpeter",
                "saltpetre"
            }
        };

    public OreDetector(ICoreClientAPI capi)
    {
        this.capi = capi;
    }

    public OreScanResult? FindNearestOre(BlockPos playerPos, int radius)
    {
        OreScanResult? nearest = null;
        int radiusSq = radius * radius;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    int distSq = x * x + y * y + z * z;

                    if (distSq > radiusSq)
                    {
                        continue;
                    }

                    BlockPos checkPos = playerPos.AddCopy(x, y, z);
                    var block = capi.World.BlockAccessor.GetBlock(checkPos);

                    if (block?.Code == null)
                    {
                        continue;
                    }

                    string blockPath = Normalize(block.Code.Path);

                    foreach (var oreEntry in ProspectorsInstinctModSystem.Config.DetectOres)
                    {
                        if (!oreEntry.Value)
                        {
                            continue;
                        }

                        if (!MatchesOre(blockPath, oreEntry.Key))
                        {
                            continue;
                        }

                        double distance = Math.Sqrt(distSq);

                        if (nearest == null || distance < nearest.Distance)
                        {
                            nearest = new OreScanResult(
                                checkPos.Copy(),
                                oreEntry.Key,
                                block.Code.ToString(),
                                distance
                            );
                        }
                    }
                }
            }
        }

        return nearest;
    }

    private static bool MatchesOre(string normalizedBlockPath, string oreName)
    {
        if (OreAliases.TryGetValue(oreName, out string[]? aliases))
        {
            foreach (string alias in aliases)
            {
                if (normalizedBlockPath.Contains(Normalize(alias)))
                {
                    return true;
                }
            }

            return false;
        }

        // Fallback for custom config entries or modded ores.
        return normalizedBlockPath.Contains(Normalize(oreName));
    }

    private static string Normalize(string value)
    {
        return value
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("_", "")
            .ToLowerInvariant();
    }
}