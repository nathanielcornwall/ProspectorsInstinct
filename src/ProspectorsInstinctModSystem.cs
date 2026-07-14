using System;
using System.Collections.Generic;
using ProspectorsInstinct.Config;
using ProspectorsInstinct.Detection;
using ProspectorsInstinct.OreDatabase.Diagnostics;
using ProspectorsInstinct.OreDatabase.Models;
using ProspectorsInstinct.OreDatabase.Services;
using Vintagestory.API.Common;

namespace ProspectorsInstinct;

public class ProspectorsInstinctModSystem : ModSystem
{
    public static ProspectorsInstinctConfig Config
    {
        get;
        private set;
    } = null!;

    private IReadOnlyDictionary<int, OreInfo> oreDatabase =
        new Dictionary<int, OreInfo>();

    private OreScanner? scanner;

    public override void Start(ICoreAPI api)
    {
        Config = ConfigManager.Load(api);

        scanner = new OreScanner(
            api,
            () => oreDatabase
        );

        scanner.Start();

        api.Logger.Notification(
            "[Prospector's Instinct] Loaded successfully!"
        );
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        base.AssetsFinalize(api);

        try
        {
            var inspector = new OreRegistryInspector(api);
            inspector.Inspect();

            var builder = new OreDatabaseBuilder(api);
            oreDatabase = builder.Build();

            api.Logger.Notification(
                "[Prospector's Instinct] Runtime Ore Database ready " +
                "with {0} entries.",
                oreDatabase.Count
            );
        }
        catch (Exception exception)
        {
            api.Logger.Error(
                "[Prospector's Instinct] Ore Database initialization " +
                "failed: {0}",
                exception
            );
        }
    }
}
