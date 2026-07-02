using Vintagestory.API.Common;

namespace ProspectorsInstinct.Detection;

public class OreScanner
{
    private readonly ICoreAPI api;

    public OreScanner(ICoreAPI api)
    {
        this.api = api;

        api.Logger.Notification("[Prospector's Instinct] OreScanner initialized.");
    }

    public void Start()
    {
        api.Event.RegisterGameTickListener(OnScanTick, 500);
        api.Logger.Notification("[Prospector's Instinct] OreScanner tick listener registered.");
    }

    private void OnScanTick(float deltaTime)
    {
        api.Logger.Notification("[Prospector's Instinct] Scanning tick...");
    }
}