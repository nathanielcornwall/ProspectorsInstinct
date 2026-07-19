using System.Collections.Generic;
using System.Linq;
using ProspectorsInstinct.OreDatabase.Models;
using Vintagestory.API.Common;

namespace ProspectorsInstinct.Metadata;

public static class MetadataValidator
{
    public static void Validate(
        ICoreAPI api,
        IReadOnlyDictionary<int, OreInfo> runtimeDatabase)
    {
        var runtimeNames = runtimeDatabase.Values
            .Select(o => o.Mineral.ToLowerInvariant())
            .Distinct()
            .OrderBy(x => x)
            .ToHashSet();

        var allMetadata = OreMetadataProvider.GetAll()
            .ToList();

        var metadataNames = allMetadata
            .Select(m => m.RuntimeName.ToLowerInvariant())
            .ToHashSet();

        var runtimeMetadataNames = allMetadata
            .Where(m => !m.IsAlias)
            .Select(m => m.RuntimeName.ToLowerInvariant())
            .ToHashSet();

        var missing = runtimeNames
            .Except(metadataNames)
            .OrderBy(x => x)
            .ToList();

        var unused = runtimeMetadataNames
            .Except(runtimeNames)
            .OrderBy(x => x)
            .ToList();

        api.Logger.Notification(
            "[Prospector's Instinct] Runtime minerals: {0}",
            runtimeNames.Count);

        api.Logger.Notification(
            "[Prospector's Instinct] Metadata entries: {0}",
            metadataNames.Count);

        api.Logger.Notification(
            "[Prospector's Instinct] Alias metadata entries: {0}",
            allMetadata.Count(m => m.IsAlias));

        if (missing.Count == 0)
        {
            api.Logger.Notification(
                "[Prospector's Instinct] ✓ Metadata coverage complete.");
        }
        else
        {
            api.Logger.Warning(
                "[Prospector's Instinct] Missing metadata for:");

            foreach (var ore in missing)
            {
                api.Logger.Warning(" - {0}", ore);
            }
        }

        if (unused.Count == 0)
        {
            api.Logger.Notification(
                "[Prospector's Instinct] ✓ No unused runtime metadata.");
        }
        else
        {
            api.Logger.Warning(
                "[Prospector's Instinct] Unused runtime metadata entries:");

            foreach (var ore in unused)
            {
                api.Logger.Warning(" - {0}", ore);
            }
        }
    }
}