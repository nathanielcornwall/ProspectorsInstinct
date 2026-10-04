# Prospector's Instinct

> **Current Version:** v0.9.4
> **Vintage Story:** 1.22.3  
> **License:** MIT

A lightweight client-side quality-of-life mod for Vintage Story.

Prospector's Instinct enhances ore hunting by providing subtle particle guidance while using a Prospecting Pick. Instead of revealing exact ore locations or replacing prospecting mechanics, the particles gently encourage exploration, helping players develop a natural sense for nearby mineral deposits.

This mod is designed to complement the vanilla prospecting experience, not replace it.

---

## Features

- Particle guidance toward nearby ore
- Runtime Ore Database
- Automatic ore discovery and parsing
- Configurable ore detection
- In-game settings menu (default hotkey: **P**) with Save and Cancel
- Particle density, size, and High Visibility options
- Scrollable resource categories with individual and bulk controls
- Reset to Defaults and automatic configuration migration
- English localization assets for community translations
- JSON database export for diagnostics
- Multiplayer compatible
- Lightweight client-side design
- GitHub Issue Tracker for bug reports and feature requests

---

## Requirements

- Vintage Story **1.22.3**
- Client-side installation

---

## Installation

1. Download the latest release from **VSModDB**.
2. Copy the downloaded `.zip` file into your:

```
VintagestoryData/Mods
```

3. Launch Vintage Story.
4. Press **P** to configure the mod in-game. Click **Save** to apply changes or **Cancel** to discard them. The configuration file is stored at:

```
VintagestoryData/ModConfig/ProspectorsInstinctConfig.json
```

---

## Configuration

The configuration file allows you to customize:

- Enable or disable the mod
- Scan radius
- Particle density
- Particle size and High Visibility
- Debug mode (moderators and administrators only)
- Prospecting Pick requirement
- Individual ore detection

No code changes are required to customize the mod.

**Reset to Defaults** resets the editable settings and known resource choices. Changes only take effect after **Save**. Enable/Disable buttons beside a category affect that category; **Enable All / Disable All** affect the full displayed list.

Older configuration files migrate automatically while retaining your choices. Invalid numeric values are repaired within the supported ranges. Resource display translations do not change saved configuration keys.

English UI strings are in `assets/prospectorsinstinct/lang/en.json`. To contribute a translation, add a language JSON with the same keys and preserve formatting placeholders such as `{0}` and `{0:0.0}`.

---

## Compatibility

Prospector's Instinct supports:

- Vanilla Vintage Story
- Multiplayer
- Existing Prospecting Pick mechanics

Version **0.7** introduces a Runtime Ore Database that automatically discovers and classifies registered ore blocks during startup. This greatly improves compatibility while reducing reliance on large hardcoded ore lists.

---

## Diagnostics

Version 0.7 automatically generates:

```
OreDatabase.json
```

inside:

```
VintagestoryData/ModConfig/
```

This file contains every ore discovered during startup and is primarily intended for diagnostics, troubleshooting, and compatibility testing.

---

## Reporting Issues

If you discover a bug or compatibility issue, please use the GitHub Issue Tracker.

Helpful files to include with your report:

- OreDatabase.json
- client-main.txt
- server-main.txt

Providing these files makes diagnosing issues significantly faster.

---

## Roadmap

See [ROADMAP.md](ROADMAP.md) for completed updates and remaining goals toward v1.0.

## Development packaging

Run `./package-test.ps1 -TestVersion "0.9.4-dev.4"` for an incremental test package. It replaces `release/ProspectorsInstinct-latest-test.zip` after verification and removes staging files.

Run `./package.ps1` for the version declared in `modinfo.json`. Packaging builds and verifies the ZIP locally. Install only one Prospector's Instinct ZIP in the game's Mods folder at a time. Public release follows the final in-game test and approval.

---

## License

Released under the **MIT License**.

---

## Thank You

Thank you for downloading Prospector's Instinct.

Feedback, bug reports, and feature suggestions are always welcome through the GitHub Issue Tracker.

Happy prospecting!
