using System;
using System.Collections.Generic;
using ProspectorsInstinct.OreDatabase.Models;
using Vintagestory.API.Common;

namespace ProspectorsInstinct.OreDatabase.Services;

public sealed class OreDatabaseBuilder
{
    private readonly ICoreAPI api;

    public OreDatabaseBuilder(ICoreAPI api)
    {
        this.api = api ?? throw new ArgumentNullException(nameof(api));
    }

    public Dictionary<int, OreInfo> Build()
    {
        Dictionary<int, OreInfo> database = new();

        IList<Block>? blocks = api.World.Blocks;

        if (blocks == null || blocks.Count == 0)
        {
            api.Logger.Warning(
                "[Prospector's Instinct] Ore database could not be built because the block registry was empty."
            );

            return database;
        }

        int structuredOreCount = 0;
        int failedParseCount = 0;
        int loggedFailureCount = 0;

        const int failureLogLimit = 50;

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

            structuredOreCount++;

            if (!OreCodeParser.TryParse(
                    block.Code.Path,
                    out string grade,
                    out string mineral,
                    out string hostRock))
            {
                failedParseCount++;

                if (loggedFailureCount < failureLogLimit)
                {
                    api.Logger.Notification(
                        "[Prospector's Instinct] Unparsed ore block #{0}: ID={1}, Code={2}",
                        loggedFailureCount + 1,
                        block.BlockId,
                        block.Code
                    );

                    loggedFailureCount++;
                }

                continue;
            }

            database[block.BlockId] = new OreInfo
            {
                BlockId = block.BlockId,
                Code = block.Code,
                Grade = grade,
                Mineral = mineral,
                HostRock = hostRock,
                IsOre = true
            };
        }

        api.Logger.Notification(
            "[Prospector's Instinct] Structured ore blocks found: {0}. Parsed: {1}. Unparsed: {2}.",
            structuredOreCount,
            database.Count,
            failedParseCount
        );

        if (failedParseCount > failureLogLimit)
        {
            api.Logger.Notification(
                "[Prospector's Instinct] Only the first {0} unparsed ore blocks were logged.",
                failureLogLimit
            );
        }

        return database;
    }
}