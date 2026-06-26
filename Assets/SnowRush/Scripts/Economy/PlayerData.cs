using System;
using System.Collections.Generic;

namespace SnowRush.Economy
{
    [Serializable]
    public sealed class PlayerData
    {
        public string PlayerId = Guid.NewGuid().ToString("N");
        public int Coins;
        public int Gems;
        public int HighScore;
        public int TotalDistance;
        public string EquippedCharacter = "penguin";
        public string EquippedSkin = "default";
        public long LastDailyRewardUtcTicks;
        public List<string> OwnedCharacters = new() { "penguin" };
        public List<string> OwnedSkins = new() { "default" };
        public Dictionary<string, int> UpgradeLevels = new();
        public Dictionary<string, int> MissionProgress = new();
        public HashSet<string> Achievements = new();
    }
}
