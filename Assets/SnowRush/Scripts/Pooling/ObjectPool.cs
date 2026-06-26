using System.Collections.Generic;
using UnityEngine;

namespace SnowRush.Pooling
{
    public sealed class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int preload = 20;
        private readonly Queue<GameObject> inactive = new();

        private void Awake()
        {
            for (var i = 0; i < preload; i++) Return(Create());
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            var item = inactive.Count > 0 ? inactive.Dequeue() : Create();
            item.transform.SetPositionAndRotation(position, rotation);
            item.SetActive(true);
            return item;
        }

        public void Return(GameObject item)
        {
            item.SetActive(false);
            item.transform.SetParent(transform);
            inactive.Enqueue(item);
        }

        private GameObject Create() => Instantiate(prefab, transform);
    }
}
