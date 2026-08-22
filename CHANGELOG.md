# Changelog

All notable changes to Prospector's Instinct will be documented in this file.

---
## v0.9.2

### Improved

- Debug Mode is now restricted to server moderators and administrators.
- Added runtime permission enforcement for Debug Mode in multiplayer.
- Improved multiplayer configuration security by preventing regular players from enabling debug logging through manual config edits.

### Fixed

- Prospecting Pick Heads no longer incorrectly activate ore detection.
- Finished Prospecting Picks continue to activate ore detection normally.

---

## v0.9.1

### Added

- Scrollable Detection Settings panel
- Full dynamic resource list
- Category-based resource grouping
- Category headers for metals, precious metals, industrial minerals, chemical minerals, fuels, gemstones, and other resources
- Enable All and Disable All controls

### Improved

- Detection Settings layout and spacing
- Resource ordering using existing metadata categories
- Detection Settings wording and overall usability
- Support for viewing and configuring all detectable resources in one panel

### Fixed

- Detection Settings no longer expands off-screen when displaying larger resource lists
- Scrollbar interaction now works correctly with the dynamic resource list

---

## v0.7.0

### Added

- Runtime Ore Database
- Automatic ore parsing during startup
- OreDatabase.json diagnostic export
- GitHub Issue Tracker with issue templates
- Improved Runtime compatibility system

### Improved

- Simplified ore detection architecture
- Reduced hardcoded ore mappings
- Better compatibility with Quartz, Sulfur, Lignite, Coal and other ores
- Cleaner scanner implementation
- Improved project documentation

### Fixed

- Multiple ore detection edge cases
- Improved scanner reliability
- Various internal stability improvements

---

## v0.6.0 Beta

- Initial public beta release
- Particle guidance system
- Configurable ore detection
- Prospecting Pick integration
