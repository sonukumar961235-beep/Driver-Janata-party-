using System.Collections.Generic;
using UnityEngine;

namespace SnowRush.Economy
{
    [CreateAssetMenu(menuName = "Snow Rush/Shop Catalog")]
    public sealed class ShopCatalog : ScriptableObject
    {
        public List<ShopItem> Characters = new()
        {
            new ShopItem("penguin", "Penguin", 0, Currency.Coins),
            new ShopItem("polar_bear", "Polar Bear", 2500, Currency.Coins),
            new ShopItem("snow_kid", "Snow Kid", 4000, Currency.Coins),
            new ShopItem("santa", "Santa", 75, Currency.Gems)
        };
        public List<ShopItem> Skins = new();
        public List<ShopItem> Upgrades = new();
    }

    public enum Currency { Coins, Gems }
    [System.Serializable]
    public sealed class ShopItem
    {
        public string Id;
        public string DisplayName;
        public int Price;
        public Currency Currency;
        public ShopItem(string id, string displayName, int price, Currency currency) { Id = id; DisplayName = displayName; Price = price; Currency = currency; }
    }
}
