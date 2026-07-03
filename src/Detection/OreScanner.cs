using Vintagestory.API.Client;
using Vintagestory.API.MathTools;
using Vintagestory.API.Common;

namespace ProspectorsInstinct.Detection;

public class OreScanner
{
    private readonly ICoreAPI api;
    private OreDetector? detector;

    public OreScanner(ICoreAPI api)
    {
        this.api = api;

        api.Logger.Notification("[Prospector's Instinct] OreScanner initialized.");
    }

    public void Start()
    {
        if (api.Side == EnumAppSide.Client)
        {
            detector = new OreDetector((ICoreClientAPI)api);

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

        var result = detector?.FindNearestOre(
            pos,
            ProspectorsInstinctModSystem.Config.ScanRadius
        );

        if (result != null)
        {
            api.Logger.Notification(
                $"[Prospector's Instinct] Found {result.OreName} " +
                $"({result.Distance:F1} blocks away)"
            );
        }
    }
}