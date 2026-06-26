using System;
using UnityEngine;

namespace SnowRush.Input
{
    public enum SwipeDirection { Left, Right, Up, Down }

    public sealed class SwipeInput : MonoBehaviour
    {
        [SerializeField] private float minSwipePixels = 55f;
        public event Action<SwipeDirection> Swiped;
        private Vector2 start;
        private bool tracking;

        private void Update()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) { start = touch.position; tracking = true; }
                if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && tracking)
                    Detect(touch.position - start);
            }
#if UNITY_EDITOR
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow)) Swiped?.Invoke(SwipeDirection.Left);
            if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow)) Swiped?.Invoke(SwipeDirection.Right);
            if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow)) Swiped?.Invoke(SwipeDirection.Up);
            if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow)) Swiped?.Invoke(SwipeDirection.Down);
#endif
        }

        private void Detect(Vector2 delta)
        {
            tracking = false;
            if (delta.magnitude < minSwipePixels) return;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) Swiped?.Invoke(delta.x < 0 ? SwipeDirection.Left : SwipeDirection.Right);
            else Swiped?.Invoke(delta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
        }
    }
}
