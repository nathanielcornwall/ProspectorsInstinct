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
        catch
        {
            api.Logger.Warning("[Prospector's Instinct] Failed to load config. Creating a new default config.");
        }

        if (config == null)
        {
            config = new ProspectorsInstinctConfig();
            api.StoreModConfig(config, ConfigFileName);
            api.Logger.Notification("[Prospector's Instinct] Default config created.");
        }
        else
        {
            api.Logger.Notification("[Prospector's Instinct] Config loaded.");
        }

        api.Logger.Notification($"[Prospector's Instinct] Enabled: {config.Enabled}");
        api.Logger.Notification($"[Prospector's Instinct] Scan Radius: {config.ScanRadius}");
        api.Logger.Notification($"[Prospector's Instinct] Particle Density: {config.ParticleDensity}");
        api.Logger.Notification($"[Prospector's Instinct] Detectable Ore Entries: {config.DetectOres.Count}");

        return config;
    }
}