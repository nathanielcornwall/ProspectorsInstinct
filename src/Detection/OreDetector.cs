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

    private static readonly Dictionary<string, string[]> ConfigMineralAliases =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Native Copper"] =
            [
                "nativecopper",
                "copper"
            ],

            ["Meteoric Iron"] =
            [
                "meteoriciron",
                "meteoriteiron",
                "ironmeteorite",
                "meteorite"
            ],

            ["Bog Iron"] =
            [
                "bogiron",
                "ironbog"
            ],

            ["Lignite"] =
            [
                "lignite",
                "browncoal"
            ],

            ["Coal"] =
            [
                "coal",
                "bituminouscoal",
                "bituminous"
            ],

            ["Sulfur"] =
            [
                "sulfur",
                "sulphur"
            ],

            ["Saltpeter"] =
            [
                "saltpeter",
                "saltpetre"
            ]
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

    public OreScanResult? FindNearestOre(
        BlockPos playerPos,
        int radius)
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

                    BlockPos checkPos =
                        playerPos.AddCopy(x, y, z);

                    var block =
                        capi.World.BlockAccessor.GetBlock(checkPos);

                    if (block?.Code == null)
                    {
                        continue;
                    }

                    string? oreName;

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
                        if (!TryResolveConfiguredOre(
                                block.Code.Path,
                                out oreName))
                        {
                            continue;
                        }
                    }

                    double distance = Math.Sqrt(distSq);

                    if (nearest == null ||
                        distance < nearest.Distance)
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
        string mineralOrBlockPath,
        out string oreName)
    {
        string normalizedValue =
            Normalize(mineralOrBlockPath);

        foreach (KeyValuePair<string, bool> oreEntry in
                 ProspectorsInstinctModSystem.Config.DetectOres)
        {
            if (!oreEntry.Value)
            {
                continue;
            }

            if (!MatchesConfiguredOre(
                    normalizedValue,
                    oreEntry.Key))
            {
                continue;
            }

            oreName = oreEntry.Key;
            return true;
        }

        oreName = string.Empty;
        return false;
    }

    private static bool MatchesConfiguredOre(
        string normalizedValue,
        string configuredOreName)
    {
        if (ConfigMineralAliases.TryGetValue(
                configuredOreName,
                out string[]? aliases))
        {
            foreach (string alias in aliases)
            {
                if (normalizedValue.Contains(
                        Normalize(alias)))
                {
                    return true;
                }
            }

            return false;
        }

        return normalizedValue.Contains(
            Normalize(configuredOreName));
    }

    private static string Normalize(string value)
    {
        return value
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty)
            .ToLowerInvariant();
    }
}