using Vintagestory.API.MathTools;

namespace ProspectorsInstinct.Detection;

public class OreScanResult
{
    public BlockPos Position { get; }
    public string OreName { get; }
    public string BlockCode { get; }
    public double Distance { get; }

    public OreScanResult(BlockPos position, string oreName, string blockCode, double distance)
    {
        Position = position;
        OreName = oreName;
        BlockCode = blockCode;
        Distance = distance;
    }
}