# Build Guide

## Requirements

- Unity 2022.3 LTS
- Android Build Support and/or iOS Build Support
- Firebase Unity SDK for production backend integration

## Scenes

Create these build scenes in `Assets/SnowRush/Scenes`:

1. `Boot.unity` - initializes services and loads player save data.
2. `MainMenu.unity` - character shop, lucky spin, daily rewards, missions, achievements, leaderboard, settings.
3. `Game.unity` - endless runner gameplay.

## Mobile Build Settings

- Target frame rate: 60 FPS via `GameManager`.
- Orientation: Portrait.
- Scripting Backend: IL2CPP.
- Architectures: ARM64.
- Graphics: URP mobile renderer.
- Compression: ASTC for Android, PVRTC/ASTC for iOS.

## Monetization Hooks

Rewarded ads and IAP buttons should call into a platform-specific monetization service. The UI and economy layers are separated so ad providers can be swapped without touching gameplay.
