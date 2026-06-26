# Snow Rush Architecture

Snow Rush is a mobile-first, offline-capable endless runner built with a clean Unity feature-slice structure under `Assets/SnowRush`.

## Layers

- **Core**: game state, configuration, service registration, session lifecycle, 60 FPS target.
- **Input**: touch swipe handling with editor keyboard fallback.
- **Gameplay**: sled runner lane movement, jump, slide, collision, scoring, and hazards.
- **Procedural**: endless segment generation with density increasing over distance.
- **Pooling**: reusable object pools for lightweight mobile performance.
- **PowerUps**: magnet, shield, double coins, speed boost, and freeze-time extension points.
- **Economy**: local JSON save, player inventory, shops, unlocks, upgrades, missions, achievements.
- **Backend**: Firebase-ready facade for Authentication, Firestore, Analytics, Crashlytics, and cloud save while keeping offline mode functional.
- **UI**: glassmorphism-ready HUD, animated buttons, pause, game-over, rewards, shops, and counters.
- **Audio**: music, SFX, and haptic hooks.

## Production Notes

1. Import Firebase Unity SDK packages and wire `BackendFacade` methods to Auth, Firestore, Analytics, and Crashlytics.
2. Mark prefab variants in `Assets/SnowRush/Prefabs` as Addressables for characters, skins, obstacles, environment segments, and UI panels.
3. Use URP mobile shaders and low-poly meshes for the Unity-style cartoon snow look.
4. Configure Android and iOS player settings for IL2CPP, ARM64, incremental GC, and texture compression.
5. Keep all spawned hazards, collectibles, and environment props pooled to maintain smooth 60 FPS gameplay.
