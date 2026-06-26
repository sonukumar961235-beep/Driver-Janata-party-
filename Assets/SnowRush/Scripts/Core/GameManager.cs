using System;
using SnowRush.Backend;
using SnowRush.Economy;
using SnowRush.Gameplay;
using SnowRush.UI;
using UnityEngine;

namespace SnowRush.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private RunnerController runner;
        [SerializeField] private HudController hud;
        [SerializeField] private SaveService saveService;
        [SerializeField] private BackendFacade backend;

        public event Action<GameState> StateChanged;
        public GameState State { get; private set; } = GameState.Boot;
        public float Distance { get; private set; }
        public int SessionCoins { get; private set; }
        public int SessionGems { get; private set; }
        public float CurrentSpeed { get; private set; }
        public GameConfig Config => config;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            ServiceLocator.Register(this);
            ServiceLocator.Register(config);
            ServiceLocator.Register(saveService);
            ServiceLocator.Register(backend);
            CurrentSpeed = config.startingSpeed;
        }

        private async void Start()
        {
            await saveService.LoadAsync();
            await backend.InitializeAsync(saveService.Data.PlayerId);
            ChangeState(GameState.MainMenu);
        }

        private void Update()
        {
            if (State != GameState.Running) return;
            Distance += CurrentSpeed * Time.deltaTime;
            CurrentSpeed = Mathf.Min(config.maxSpeed, CurrentSpeed + config.speedIncreasePerMinute / 60f * Time.deltaTime);
            hud.SetCounters(Distance, SessionCoins, saveService.Data.Gems + SessionGems);
        }

        public void StartRun()
        {
            Distance = 0;
            SessionCoins = 0;
            SessionGems = 0;
            CurrentSpeed = config.startingSpeed;
            runner.ResetRunner();
            ChangeState(GameState.Running);
        }

        public void Pause() => ChangeState(GameState.Paused);
        public void Resume() => ChangeState(GameState.Running);

        public void AddCoins(int amount) => SessionCoins += amount;
        public void AddGems(int amount) => SessionGems += amount;

        public async void GameOver()
        {
            ChangeState(GameState.GameOver);
            saveService.Data.Coins += SessionCoins;
            saveService.Data.Gems += SessionGems;
            saveService.Data.HighScore = Mathf.Max(saveService.Data.HighScore, Mathf.FloorToInt(Distance));
            saveService.Data.TotalDistance += Mathf.FloorToInt(Distance);
            await saveService.SaveAsync();
            await backend.PushCloudSaveAsync(saveService.Data);
            backend.TrackRunEnded(Mathf.FloorToInt(Distance), SessionCoins);
        }

        private void ChangeState(GameState next)
        {
            State = next;
            Time.timeScale = next == GameState.Paused ? 0f : 1f;
            StateChanged?.Invoke(next);
        }
    }
}
