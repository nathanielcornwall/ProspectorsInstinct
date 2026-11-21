using Vintagestory.API.Config;
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

    public override void StartClientSide(ICoreClientAPI capi)
    {
        base.StartClientSide(capi);
        Config = ConfigManager.Load(capi);
        scanner = new OreScanner(capi, () => oreDatabase);
        scanner.Start();

        capi.Input.RegisterHotKey(
            "prospectorsinstinct-config",
            Lang.Get("prospectorsinstinct:config-hotkey"),
            GlKeys.P,
            HotkeyType.GUIOrOtherControls);

        capi.Input.SetHotKeyHandler("prospectorsinstinct-config", _ =>
        {
            if (capi.World.Player?.Entity == null)
            {
                return false;
            }

            configDialog ??= new GuiDialogProspectorsInstinct(capi);
            configDialog.Toggle();
            return true;
        });
    }

    public override void Dispose()
    {
        scanner?.Dispose();
        configDialog?.Dispose();
        base.Dispose();
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
