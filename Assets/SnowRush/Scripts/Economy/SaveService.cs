using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace SnowRush.Economy
{
    public sealed class SaveService : MonoBehaviour
    {
        public PlayerData Data { get; private set; } = new();
        private string Path => System.IO.Path.Combine(Application.persistentDataPath, "snow-rush-save.json");

        public async Task LoadAsync()
        {
            if (!File.Exists(Path)) return;
            var json = await File.ReadAllTextAsync(Path);
            Data = JsonUtility.FromJson<PlayerData>(json) ?? new PlayerData();
        }

        public Task SaveAsync()
        {
            var json = JsonUtility.ToJson(Data, true);
            return File.WriteAllTextAsync(Path, json);
        }
    }
}
