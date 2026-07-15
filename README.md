# Prospector's Instinct

> **Current Version:** v0.7.0  
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
4. Configure the mod (optional) using:

```
VintagestoryData/ModConfig/ProspectorsInstinctConfig.json
```

---

## Configuration

The configuration file allows you to customize:

- Enable or disable the mod
- Scan radius
- Particle density
- Debug mode
- Prospecting Pick requirement
- Individual ore detection

No code changes are required to customize the mod.

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

### v0.7.0

- ✅ Runtime Ore Database
- ✅ Automatic ore parsing
- ✅ JSON database export
- ✅ Improved ore compatibility
- ✅ GitHub Issue Tracker

### v0.8.0

- Ore categories (Metal, Gem, Fuel, Salt, etc.)
- Enhanced particle guidance
- Expanded mod compatibility
- Additional configuration options
- Performance optimizations

### Future

- Localization support
- Developer diagnostics
- Optional particle styles
- Community compatibility packs

---

## License

Released under the **MIT License**.

---

## Thank You

Thank you for downloading Prospector's Instinct.

Feedback, bug reports, and feature suggestions are always welcome through the GitHub Issue Tracker.

Happy prospecting!