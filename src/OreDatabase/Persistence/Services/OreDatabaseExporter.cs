using System;
using System.Collections.Generic;
using System.Linq;
using ProspectorsInstinct.OreDatabase.Models;
using Vintagestory.API.Common;

namespace ProspectorsInstinct.OreDatabase.Services;

public sealed class OreDatabaseExporter
{
    private const string OutputFileName = "OreDatabase.json";

    private readonly ICoreAPI api;

    public OreDatabaseExporter(ICoreAPI api)
    {
        this.api = api
            ?? throw new ArgumentNullException(nameof(api));
    }

    public void Export(
        IReadOnlyDictionary<int, OreInfo> database)
    {
        if (database == null)
        {
            throw new ArgumentNullException(nameof(database));
        }

        List<OreDatabaseExportEntry> entries = database.Values
            .OrderBy(ore => ore.BlockId)
            .Select(ore => new OreDatabaseExportEntry
            {
                BlockId = ore.BlockId,
                Code = ore.Code?.ToString() ?? string.Empty,
                Mineral = ore.Mineral,
                Grade = ore.Grade,
                HostRock = ore.HostRock,
                IsOre = ore.IsOre
            })
            .ToList();

        api.StoreModConfig(
            entries,
            OutputFileName
        );

        api.Logger.Notification(
            "[Prospector's Instinct] Exported {0} ore database entries to {1}.",
            entries.Count,
            OutputFileName
        );
    }

    private sealed class OreDatabaseExportEntry
    {
        public int BlockId { get; init; }

        public string Code { get; init; } = string.Empty;

        public string Mineral { get; init; } = string.Empty;

        public string Grade { get; init; } = string.Empty;

        public string HostRock { get; init; } = string.Empty;

        public bool IsOre { get; init; }
    }
}