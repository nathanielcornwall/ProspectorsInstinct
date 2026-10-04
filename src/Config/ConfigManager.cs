using Vintagestory.API.Common;

namespace ProspectorsInstinct.Config;

public static class ConfigManager
{
    private const string ConfigFileName = "ProspectorsInstinctConfig.json";

    public static ProspectorsInstinctConfig Load(ICoreAPI api)
    {
        ProspectorsInstinctConfig? config = null;

        try
        {
            config = api.LoadModConfig<ProspectorsInstinctConfig>(ConfigFileName);
        }
        catch (System.Exception exception)
        {
            api.Logger.Warning("[Prospector's Instinct] Failed to load config; leaving the original file unchanged: {0}", exception.Message);
            var defaults = new ProspectorsInstinctConfig();
            defaults.Normalize();
            return defaults;
        }

        if (config == null)
        {
            config = new ProspectorsInstinctConfig();
            config.Normalize();
            api.StoreModConfig(config, ConfigFileName);
            api.Logger.Notification("[Prospector's Instinct] Default config created.");
        }
        else
        {
            if (config.ConfigVersion > ProspectorsInstinctConfig.CurrentConfigVersion)
            {
                api.Logger.Warning("[Prospector's Instinct] Config was created by a newer version; refusing to rewrite it.");
                config.Normalize();
            }
            else if (config.Normalize())
            {
                api.StoreModConfig(config, ConfigFileName);
                api.Logger.Notification("[Prospector's Instinct] Config migrated and validated.");
            }
            api.Logger.Notification("[Prospector's Instinct] Config loaded.");
        }

        api.Logger.Notification($"[Prospector's Instinct] Enabled: {config.Enabled}");
        api.Logger.Notification($"[Prospector's Instinct] Scan Radius: {config.ScanRadius}");
        api.Logger.Notification($"[Prospector's Instinct] Particle Density: {config.ParticleDensity}");
        api.Logger.Notification($"[Prospector's Instinct] Detectable Ore Entries: {config.DetectOres.Count}");

        return config;
    }
    public static void Save(
    ICoreAPI api,
    ProspectorsInstinctConfig config)
{
    if (config.ConfigVersion > ProspectorsInstinctConfig.CurrentConfigVersion)
    {
        api.Logger.Warning("[Prospector's Instinct] Cannot save a config from a newer version.");
        return;
    }
    config.Normalize();
    api.StoreModConfig(
        config,
        ConfigFileName);

    api.Logger.Notification(
        "[Prospector's Instinct] Config saved."
    );
}
}
