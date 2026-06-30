using Vintagestory.API.Common;

namespace ProspectorsInstinct;

public class ProspectorsInstinctModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        api.Logger.Notification("[Prospector's Instinct] Loaded successfully!");
    }
}
