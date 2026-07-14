using System;
using System.Collections.Generic;
using ProspectorsInstinct.OreDatabase.Models;
using Vintagestory.API.Client;
using Vintagestory.API.MathTools;

namespace ProspectorsInstinct.Detection;

public class OreDetector
{
    private readonly ICoreClientAPI capi;
    private readonly Func<IReadOnlyDictionary<int, OreInfo>> oreDatabaseProvider;

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

    public OreDetector(
        ICoreClientAPI capi,
        Func<IReadOnlyDictionary<int, OreInfo>> oreDatabaseProvider)
    {
        this.capi = capi
            ?? throw new ArgumentNullException(nameof(capi));

        this.oreDatabaseProvider = oreDatabaseProvider
            ?? throw new ArgumentNullException(nameof(oreDatabaseProvider));
    }

    public OreScanResult? FindNearestOre(BlockPos playerPos, int radius)
    {
        OreScanResult? nearest = null;
        int radiusSq = radius * radius;

        IReadOnlyDictionary<int, OreInfo> oreDatabase =
            oreDatabaseProvider();

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

                    string? oreName = null;

                    // Preferred v0.7.0 path:
                    // perform a fast lookup using the generated Ore Database.
                    if (oreDatabase.TryGetValue(
                            block.BlockId,
                            out OreInfo? oreInfo))
                    {
                        if (!TryResolveConfiguredOre(
                                oreInfo.Mineral,
                                out oreName))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // Temporary compatibility fallback for special blocks
                        // that do not use the structured "ore-" format.
                        string blockPath = Normalize(block.Code.Path);

                        if (!TryResolveConfiguredOre(
                                blockPath,
                                out oreName))
                        {
                            continue;
                        }
                    }

                    double distance = Math.Sqrt(distSq);

                    if (nearest == null || distance < nearest.Distance)
                    {
                        nearest = new OreScanResult(
                            checkPos.Copy(),
                            oreName,
                            block.Code.ToString(),
                            distance
                        );
                    }
                }
            }
        }

        return nearest;
    }

    private static bool TryResolveConfiguredOre(
        string value,
        out string oreName)
    {
        string normalizedValue = Normalize(value);

        foreach (var oreEntry in
                 ProspectorsInstinctModSystem.Config.DetectOres)
        {
            if (!oreEntry.Value)
            {
                continue;
            }

            if (!MatchesOre(normalizedValue, oreEntry.Key))
            {
                continue;
            }

            oreName = oreEntry.Key;
            return true;
        }

        oreName = string.Empty;
        return false;
    }

    private static bool MatchesOre(
        string normalizedValue,
        string oreName)
    {
        if (OreAliases.TryGetValue(
                oreName,
                out string[]? aliases))
        {
            foreach (string alias in aliases)
            {
                if (normalizedValue.Contains(Normalize(alias)))
                {
                    return true;
                }
            }

            return false;
        }

        // Supports custom configuration entries and modded minerals.
        return normalizedValue.Contains(Normalize(oreName));
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