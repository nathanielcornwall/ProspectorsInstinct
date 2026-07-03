using System;
using Vintagestory.API.Client;
using Vintagestory.API.MathTools;

namespace ProspectorsInstinct.Detection;

public class OreDetector
{
    private readonly ICoreClientAPI capi;

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

                    string blockPath = block.Code.Path.ToLowerInvariant();

                    foreach (var oreEntry in ProspectorsInstinctModSystem.Config.DetectOres)
                    {
                        if (!oreEntry.Value)
                        {
                            continue;
                        }

                        string oreKey = oreEntry.Key
                            .Replace(" ", "")
                            .ToLowerInvariant();

                        if (!blockPath.Contains(oreKey))
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
}