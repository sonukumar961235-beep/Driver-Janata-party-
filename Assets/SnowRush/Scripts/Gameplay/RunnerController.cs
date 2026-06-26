using SnowRush.Core;
using SnowRush.Input;
using SnowRush.PowerUps;
using UnityEngine;

namespace SnowRush.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class RunnerController : MonoBehaviour
    {
        [SerializeField] private SwipeInput input;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform sledVisual;
        [SerializeField] private LayerMask hazardMask;
        private CharacterController controller;
        private GameManager game;
        private int lane;
        private float verticalVelocity;
        private float slideTimer;
        private bool shielded;

        private void Awake() => controller = GetComponent<CharacterController>();
        private void OnEnable() => input.Swiped += OnSwipe;
        private void OnDisable() => input.Swiped -= OnSwipe;
        private void Start() => game = ServiceLocator.Get<GameManager>();

        private void Update()
        {
            if (game == null || game.State != GameState.Running) return;
            var cfg = game.Config;
            var targetX = lane * cfg.laneWidth;
            var pos = transform.position;
            var move = new Vector3((targetX - pos.x) * 12f, verticalVelocity, (PowerUpController.SpeedBoostActive ? game.CurrentSpeed * 1.35f : game.CurrentSpeed)) * Time.deltaTime;
            if (controller.isGrounded && verticalVelocity < 0) verticalVelocity = -1f;
            verticalVelocity += Physics.gravity.y * 2.2f * Time.deltaTime;
            controller.Move(move);
            slideTimer -= Time.deltaTime;
            animator.SetBool("Sliding", slideTimer > 0);
            sledVisual.localRotation = Quaternion.Euler(0, 0, (pos.x - targetX) * 7f);
        }

        public void ResetRunner()
        {
            lane = 0;
            verticalVelocity = 0;
            slideTimer = 0;
            transform.position = Vector3.up;
            shielded = false;
        }

        public void SetShield(bool enabled) => shielded = enabled;

        private void OnSwipe(SwipeDirection direction)
        {
            if (game.State != GameState.Running) return;
            if (direction == SwipeDirection.Left) lane = Mathf.Max(lane - 1, -1);
            if (direction == SwipeDirection.Right) lane = Mathf.Min(lane + 1, 1);
            if (direction == SwipeDirection.Up && controller.isGrounded) { verticalVelocity = game.Config.jumpForce; animator.SetTrigger("Jump"); }
            if (direction == SwipeDirection.Down) slideTimer = game.Config.slideDuration;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin")) { game.AddCoins(PowerUpController.DoubleCoinsActive ? 2 : 1); other.gameObject.SetActive(false); }
            else if (other.CompareTag("Gem")) { game.AddGems(1); other.gameObject.SetActive(false); }
            else if (other.CompareTag("PowerUp")) other.GetComponent<PowerUpPickup>()?.Collect(this);
            else if (((1 << other.gameObject.layer) & hazardMask) != 0)
            {
                if (shielded) { shielded = false; other.gameObject.SetActive(false); return; }
                game.GameOver();
            }
        }
    }
}
