using Vintagestory.API.Common;
using ProspectorsInstinct.Config;
using ProspectorsInstinct.Detection;

namespace ProspectorsInstinct;

public class ProspectorsInstinctModSystem : ModSystem
{
    public static ProspectorsInstinctConfig Config { get; private set; } = null!;

    private OreScanner? scanner;

    public override void Start(ICoreAPI api)
    {
        Config = ConfigManager.Load(api);

        scanner = new OreScanner(api);
scanner.Start();

        api.Logger.Notification("[Prospector's Instinct] Loaded successfully!");
    }
}
