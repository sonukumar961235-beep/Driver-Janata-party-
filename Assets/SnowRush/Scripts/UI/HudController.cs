using SnowRush.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnowRush.UI
{
    public sealed class HudController : MonoBehaviour
    {
        [SerializeField] private TMP_Text distanceText;
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text gemText;
        [SerializeField] private CanvasGroup pauseMenu;
        [SerializeField] private CanvasGroup gameOverPanel;
        [SerializeField] private Button pauseButton;
        private GameManager game;

        private void Start()
        {
            game = ServiceLocator.Get<GameManager>();
            game.StateChanged += OnStateChanged;
            pauseButton.onClick.AddListener(game.Pause);
        }

        public void SetCounters(float distance, int coins, int gems)
        {
            distanceText.text = $"{Mathf.FloorToInt(distance)} m";
            coinText.text = coins.ToString();
            gemText.text = gems.ToString();
        }

        private void OnStateChanged(GameState state)
        {
            SetVisible(pauseMenu, state == GameState.Paused);
            SetVisible(gameOverPanel, state == GameState.GameOver);
        }

        private static void SetVisible(CanvasGroup group, bool visible)
        {
            group.alpha = visible ? 1 : 0;
            group.interactable = visible;
            group.blocksRaycasts = visible;
        }
    }
}
