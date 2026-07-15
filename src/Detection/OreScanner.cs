using System;
using System.Collections.Generic;
using ProspectorsInstinct.OreDatabase.Models;
using ProspectorsInstinct.Rendering;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace ProspectorsInstinct.Detection;

public class OreScanner
{
    private readonly ICoreAPI api;
    private readonly Func<IReadOnlyDictionary<int, OreInfo>>
        oreDatabaseProvider;

    private OreDetector? detector;
    private ParticleGuide? particleGuide;

    public OreScanner(
        ICoreAPI api,
        Func<IReadOnlyDictionary<int, OreInfo>> oreDatabaseProvider)
    {
        this.api = api
            ?? throw new ArgumentNullException(nameof(api));

        this.oreDatabaseProvider = oreDatabaseProvider
            ?? throw new ArgumentNullException(
                nameof(oreDatabaseProvider));

        api.Logger.Notification(
            "[Prospector's Instinct] OreScanner initialized."
        );
    }

    public void Start()
    {
        if (api.Side != EnumAppSide.Client)
        {
            return;
        }

        ICoreClientAPI capi = (ICoreClientAPI)api;

        detector = new OreDetector(
            capi,
            oreDatabaseProvider
        );

        particleGuide = new ParticleGuide(capi);

        api.Event.RegisterGameTickListener(
            OnScanTick,
            ProspectorsInstinctModSystem.Config.ScanIntervalMs
        );

        api.Logger.Notification(
            "[Prospector's Instinct] Client scanner enabled."
        );
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

        if (ProspectorsInstinctModSystem.Config.RequireProspectingPick &&
            !HasProspectingPick(capi))
        {
            return;
        }

        BlockPos pos = player.Entity.Pos.AsBlockPos;

        OreScanResult? result = detector?.FindNearestOre(
            pos,
            ProspectorsInstinctModSystem.Config.ScanRadius
        );

        if (result == null)
        {
            return;
        }

        if (ProspectorsInstinctModSystem.Config.DebugMode)
        {
            api.Logger.Notification(
                $"[Prospector's Instinct] Found {result.OreName} " +
                $"({result.Distance:F1} blocks away)"
            );
        }

        Vec3d playerPos =
            player.Entity.Pos.XYZ.Add(0, 1.5, 0);

        Vec3d orePos =
            result.Position.ToVec3d().Add(0.5, 0.5, 0.5);

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
                player.Entity.Pos.XYZ
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

        particleGuide?.Spawn(particlePos);
    }

    private static bool HasProspectingPick(
        ICoreClientAPI capi)
    {
        var activeItem = capi.World.Player
            .InventoryManager
            .ActiveHotbarSlot?
            .Itemstack;

        if (activeItem?.Collectible?.Code == null)
        {
            return false;
        }

        string itemCode = activeItem
            .Collectible
            .Code
            .ToString()
            .ToLowerInvariant();

        return itemCode.Contains("prospectingpick");
    }
}