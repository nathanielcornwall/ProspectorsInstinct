using System;
using System.Collections.Generic;
using ProspectorsInstinct.Config;
using ProspectorsInstinct.Detection;
using ProspectorsInstinct.OreDatabase.Diagnostics;
using ProspectorsInstinct.OreDatabase.Models;
using ProspectorsInstinct.OreDatabase.Services;
using Vintagestory.API.Common;
using ProspectorsInstinct.Gui;
using Vintagestory.API.Client;


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
    private GuiDialogProspectorsInstinct? configDialog;

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

    public override void StartClientSide(
    ICoreClientAPI capi)
{
    base.StartClientSide(capi);

    capi.Input.RegisterHotKey(
        "prospectorsinstinct-config",
        "Open Prospector's Instinct configuration",
        GlKeys.P,
        HotkeyType.GUIOrOtherControls
    );

    capi.Input.SetHotKeyHandler(
        "prospectorsinstinct-config",
        _ =>
        {
            configDialog ??=
                new GuiDialogProspectorsInstinct(capi);

            configDialog.Toggle();
            return true;
        }
    );

    capi.Logger.Notification(
        "[Prospector's Instinct] Configuration hotkey registered."
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

            Metadata.MetadataValidator.Validate(
    api,
    oreDatabase);

            api.Logger.Notification(
                "[Prospector's Instinct] Runtime Ore Database ready " +
                "with {0} entries.",
                oreDatabase.Count
            );

            if (api.Side == EnumAppSide.Client)
            {
                var exporter = new OreDatabaseExporter(api);
                exporter.Export(oreDatabase);
            }
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
