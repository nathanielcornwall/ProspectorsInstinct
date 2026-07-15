using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;

namespace ProspectorsInstinct.OreDatabase.Diagnostics;

public sealed class OreRegistryInspector
{
    private readonly ICoreAPI api;

    public OreRegistryInspector(ICoreAPI api)
    {
        this.api = api ?? throw new ArgumentNullException(nameof(api));
    }

    public void Inspect()
{
    IList<Block>? blocks = api.World.Blocks;

    if (blocks == null || blocks.Count == 0)
    {
        api.Logger.Warning(
            "[Prospector's Instinct] Registry inspection found no registered blocks."
        );

        return;
    }

    int registeredBlockCount = blocks.Count(block => block != null);

    api.Logger.Notification(
        "[Prospector's Instinct] Registry inspection found {0} registered blocks.",
        registeredBlockCount
    );

    int loggedCount = 0;
    const int sampleSize = 20;

    foreach (Block? block in blocks)
    {
        if (block?.Code?.Path == null)
        {
            continue;
        }

        if (!block.Code.Path.StartsWith(
        "ore-",
        StringComparison.OrdinalIgnoreCase))
{
    continue;
}

        api.Logger.Notification(
    "[Prospector's Instinct] Structured ore sample #{0}: ID={1}, Code={2}",
    loggedCount + 1,
    block.BlockId,
    block.Code
);

        loggedCount++;

        if (loggedCount >= sampleSize)
        {
            break;
        }
    }

    api.Logger.Notification(
    "[Prospector's Instinct] Logged {0} structured ore registry sample blocks.",
    loggedCount
);
}
}