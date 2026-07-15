using System;

namespace ProspectorsInstinct.OreDatabase.Services;

public static class OreCodeParser
{
    public static bool TryParse(
        string codePath,
        out string grade,
        out string mineral,
        out string hostRock)
    {
        grade = string.Empty;
        mineral = string.Empty;
        hostRock = string.Empty;

        if (string.IsNullOrWhiteSpace(codePath))
        {
            return false;
        }

        string[] parts = codePath.Split('-');

        // -------------------------
        // Pattern A
        //
        // ore-low-nativecopper-granite
        // -------------------------
        if (parts.Length == 4 &&
            parts[0].Equals("ore", StringComparison.OrdinalIgnoreCase))
        {
            grade = parts[1];
            mineral = parts[2];
            hostRock = parts[3];

            return true;
        }

        // -------------------------
        // Pattern B
        //
        // ore-quartz-granite
        // ore-lignite-chalk
        // ore-flint-andesite
        // -------------------------
        if (parts.Length == 3 &&
            parts[0].Equals("ore", StringComparison.OrdinalIgnoreCase))
        {
            grade = "none";
            mineral = parts[1];
            hostRock = parts[2];

            return true;
        }

        return false;
    }
}