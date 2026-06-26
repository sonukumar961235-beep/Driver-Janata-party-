using UnityEngine;
using UnityEngine.EventSystems;

namespace SnowRush.UI
{
    public sealed class AnimatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float pressedScale = .94f;
        [SerializeField] private float speed = 16f;
        private Vector3 target = Vector3.one;
        private void Update() => transform.localScale = Vector3.Lerp(transform.localScale, target, Time.unscaledDeltaTime * speed);
        public void OnPointerDown(PointerEventData eventData) => target = Vector3.one * pressedScale;
        public void OnPointerUp(PointerEventData eventData) => target = Vector3.one;
    }
}
