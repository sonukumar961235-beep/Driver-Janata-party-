using System.Collections;
using SnowRush.Core;
using SnowRush.Gameplay;
using UnityEngine;

namespace SnowRush.PowerUps
{
    public enum PowerUpType { Magnet, Shield, DoubleCoins, SpeedBoost, FreezeTime }

    public sealed class PowerUpController : MonoBehaviour
    {
        public static bool DoubleCoinsActive { get; private set; }
        public static bool MagnetActive { get; private set; }
        public static bool SpeedBoostActive { get; private set; }
        private GameConfig config;
        private void Start() => config = ServiceLocator.Get<GameConfig>();

        public void Activate(PowerUpType type, RunnerController runner)
        {
            StopCoroutine(nameof(RunPowerUp));
            StartCoroutine(RunPowerUp(type, runner));
        }

        private IEnumerator RunPowerUp(PowerUpType type, RunnerController runner)
        {
            if (type == PowerUpType.Shield) runner.SetShield(true);
            if (type == PowerUpType.DoubleCoins) DoubleCoinsActive = true;
            if (type == PowerUpType.Magnet) MagnetActive = true;
            if (type == PowerUpType.SpeedBoost) SpeedBoostActive = true;
            if (type == PowerUpType.FreezeTime) Time.timeScale = .55f;
            yield return new WaitForSecondsRealtime(config.powerUpDuration);
            if (type == PowerUpType.Shield) runner.SetShield(false);
            if (type == PowerUpType.DoubleCoins) DoubleCoinsActive = false;
            if (type == PowerUpType.Magnet) MagnetActive = false;
            if (type == PowerUpType.SpeedBoost) SpeedBoostActive = false;
            if (type == PowerUpType.FreezeTime) Time.timeScale = 1f;
        }
    }

    public sealed class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] private PowerUpType type;
        public void Collect(RunnerController runner)
        {
            FindObjectOfType<PowerUpController>().Activate(type, runner);
            gameObject.SetActive(false);
        }
    }
}
