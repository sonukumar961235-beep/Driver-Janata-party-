using System.Threading.Tasks;
using SnowRush.Economy;
using UnityEngine;

namespace SnowRush.Backend
{
    public sealed class BackendFacade : MonoBehaviour
    {
        [SerializeField] private bool firebaseEnabled;
        public bool IsOnline { get; private set; }

        public Task InitializeAsync(string playerId)
        {
            IsOnline = Application.internetReachability != NetworkReachability.NotReachable;
            Debug.Log($"Snow Rush backend initialized for {playerId}. Firebase enabled: {firebaseEnabled}, online: {IsOnline}");
            return Task.CompletedTask;
        }

        public Task PushCloudSaveAsync(PlayerData data)
        {
            if (!firebaseEnabled || !IsOnline) return Task.CompletedTask;
            // Production hook: Firebase Auth anonymous sign-in, Firestore player document, Analytics, Crashlytics.
            Debug.Log($"Cloud save queued. highScore={data.HighScore}, coins={data.Coins}");
            return Task.CompletedTask;
        }

        public void TrackRunEnded(int distance, int coins)
        {
            if (!firebaseEnabled || !IsOnline) return;
            Debug.Log($"Analytics run_end distance={distance} coins={coins}");
        }
    }
}
