using Vintagestory.API.Client;
using Vintagestory.API.MathTools;
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
        if (api.Side == EnumAppSide.Client)
        {
            api.Event.RegisterGameTickListener(OnScanTick, 500);
            api.Logger.Notification("[Prospector's Instinct] Client scanner enabled.");
        }
    }

    private void OnScanTick(float deltaTime)
    {
        if (!ProspectorsInstinctModSystem.Config.Enabled)
        {
            return;
        }

        if (api is not ICoreClientAPI capi)
        {
            return;
        }

        var player = capi.World.Player;

        if (player?.Entity == null)
        {
            return;
        }

        BlockPos pos = player.Entity.Pos.AsBlockPos;

        api.Logger.Notification(
            $"[Prospector's Instinct] Player at X:{pos.X} Y:{pos.Y} Z:{pos.Z}"
        );
    }
}