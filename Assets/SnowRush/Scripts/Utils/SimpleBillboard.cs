using UnityEngine;

namespace SnowRush.Utils
{
    public sealed class SimpleBillboard : MonoBehaviour
    {
        private Camera cam;
        private void Awake() => cam = Camera.main;
        private void LateUpdate()
        {
            if (cam != null) transform.forward = cam.transform.forward;
        }
    }
}
