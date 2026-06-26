using UnityEngine;

namespace SnowRush.Core
{
    [CreateAssetMenu(menuName = "Snow Rush/Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        [Header("Runner")]
        public float startingSpeed = 10f;
        public float maxSpeed = 32f;
        public float speedIncreasePerMinute = 3.25f;
        public float laneWidth = 2.4f;
        public int laneCount = 3;
        public float jumpForce = 8f;
        public float slideDuration = .75f;

        [Header("Generation")]
        public int initialSegments = 8;
        public float segmentLength = 18f;
        public int pooledSegments = 14;
        public AnimationCurve obstacleDensity = AnimationCurve.Linear(0, .18f, 900, .72f);

        [Header("Economy")]
        public int dailyRewardCoins = 250;
        public int reviveGemCost = 5;
        public float powerUpDuration = 8f;
    }
}
