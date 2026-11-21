using System;
using System.Collections.Generic;
using ProspectorsInstinct.OreDatabase.Models;
using ProspectorsInstinct.Rendering;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace ProspectorsInstinct.Detection;

public class OreScanner : IDisposable
{
    private readonly ICoreClientAPI capi;
    private readonly Func<IReadOnlyDictionary<int, OreInfo>>
        oreDatabaseProvider;

    private OreDetector? detector;
    private ParticleGuide? particleGuide;
    private bool guidanceActive;
    private long? tickListenerId;
    private int? lastDimension;

    public OreScanner(
        ICoreClientAPI capi,
        Func<IReadOnlyDictionary<int, OreInfo>> oreDatabaseProvider)
    {
        this.capi = capi
            ?? throw new ArgumentNullException(nameof(capi));

        this.oreDatabaseProvider = oreDatabaseProvider
            ?? throw new ArgumentNullException(
                nameof(oreDatabaseProvider));

        capi.Logger.Notification(
            "[Prospector's Instinct] OreScanner initialized."
        );
    }

    public void Start()
    {
        if (detector != null)
        {
            return;
        }

        detector = new OreDetector(
            capi,
            oreDatabaseProvider
        );

        particleGuide = new ParticleGuide(capi);

        capi.Event.LevelFinalize += StartScanning;
        capi.Event.LeaveWorld += StopScanning;
    }

    private void StartScanning()
    {
        if (tickListenerId.HasValue)
        {
            return;
        }

        tickListenerId = capi.Event.RegisterGameTickListener(
            OnScanTick,
            ProspectorsInstinctModSystem.Config.ScanIntervalMs
        );

        capi.Logger.Notification(
            "[Prospector's Instinct] Client scanner enabled."
        );
    }

    private void StopScanning()
    {
        if (tickListenerId is long listenerId)
        {
            capi.Event.UnregisterGameTickListener(listenerId);
            tickListenerId = null;
        }

        guidanceActive = false;
        lastDimension = null;
    }

    public void Dispose()
    {
        StopScanning();
        capi.Event.LevelFinalize -= StartScanning;
        capi.Event.LeaveWorld -= StopScanning;
        detector = null;
        particleGuide = null;
    }

    internal static bool CanUseDebugMode(IPlayer? player) =>
        player?.HasPrivilege(Privilege.kick) == true;

    private void OnScanTick(float deltaTime)
    {
        if (!ProspectorsInstinctModSystem.Config.Enabled)
        {
            guidanceActive = false;
            return;
        }

        var player = capi.World.Player;

        if (player?.Entity is not { Alive: true } entity)
        {
            guidanceActive = false;
            return;
        }

        if (ProspectorsInstinctModSystem.Config.RequireProspectingPick &&
            !HasProspectingPick(player))
        {
            guidanceActive = false;
            return;
        }

        BlockPos pos = entity.Pos.AsBlockPos;
        if (lastDimension != pos.dimension)
        {
            guidanceActive = false;
            lastDimension = pos.dimension;
        }

        if (capi.World.BlockAccessor.GetChunkAtBlockPos(pos) == null)
        {
            guidanceActive = false;
            return;
        }

        OreScanResult? result = detector?.FindNearestOre(
            pos,
            ProspectorsInstinctModSystem.Config.ScanRadius
        );

        if (result == null)
        {
            guidanceActive = false;
            return;
        }

        bool pulse = !guidanceActive;
        guidanceActive = true;

        if (ProspectorsInstinctModSystem.Config.DebugMode &&
            CanUseDebugMode(player))
        {
            capi.Logger.Notification(
                $"[Prospector's Instinct] Found {result.OreName} " +
                $"({result.Distance:F1} blocks away)");
        }

        Vec3d playerPos =
            entity.Pos.XYZ.Add(0, 1.5, 0);

        BlockPos oreBlock = result.Position;
        Vec3d orePos = new(oreBlock.X + 0.5, oreBlock.InternalY + 0.5, oreBlock.Z + 0.5);

        double horizontalDistance = Math.Sqrt(
            Math.Pow(orePos.X - playerPos.X, 2) +
            Math.Pow(orePos.Z - playerPos.Z, 2)
        );

        bool oreIsBelowPlayer =
            orePos.Y < playerPos.Y - 1.0;

        bool playerIsAboveOre =
            horizontalDistance < 2.0 && oreIsBelowPlayer;

        if (playerIsAboveOre)
        {
            particleGuide?.SpawnLocated(
                entity.Pos.XYZ, result.OreName, pulse
            );

            return;
        }

        Vec3d direction =
            orePos.SubCopy(playerPos).Normalize();

        Vec3d particlePos = playerPos.AddCopy(
            direction.X * 2,
            direction.Y * 2,
            direction.Z * 2
        );

        particleGuide?.Spawn(particlePos, result.Distance, result.OreName, pulse);
    }

    private static bool HasProspectingPick(
        IPlayer player)
    {
        var activeItem = player.InventoryManager?
            .ActiveHotbarSlot?
            .Itemstack;

        return activeItem?.Collectible?.Code?.Path.StartsWith(
            "prospectingpick-", StringComparison.OrdinalIgnoreCase) == true;
    }
}