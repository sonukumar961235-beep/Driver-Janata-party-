using SnowRush.PowerUps;
using UnityEngine;

namespace SnowRush.Gameplay
{
    public sealed class CollectibleMagnetTarget : MonoBehaviour
    {
        [SerializeField] private float attractionSpeed = 18f;
        private Transform runner;

        private void Start()
        {
            var controller = FindObjectOfType<RunnerController>();
            if (controller != null) runner = controller.transform;
        }

        private void Update()
        {
            if (!PowerUpController.MagnetActive || runner == null) return;
            if (Vector3.Distance(transform.position, runner.position) > 9f) return;
            transform.position = Vector3.MoveTowards(transform.position, runner.position + Vector3.up, attractionSpeed * Time.deltaTime);
        }
    }
}
