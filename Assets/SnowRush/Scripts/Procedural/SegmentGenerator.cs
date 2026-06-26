using System.Collections.Generic;
using SnowRush.Core;
using SnowRush.Pooling;
using UnityEngine;

namespace SnowRush.Procedural
{
    public sealed class SegmentGenerator : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private ObjectPool[] segmentPools;
        [SerializeField] private GameObject[] obstaclePrefabs;
        [SerializeField] private GameObject[] collectiblePrefabs;
        [SerializeField] private Transform spawnedRoot;
        private readonly Queue<GameObject> activeSegments = new();
        private GameConfig config;
        private float nextZ;

        private void Start()
        {
            config = ServiceLocator.Get<GameConfig>();
            for (var i = 0; i < config.initialSegments; i++) SpawnSegment();
        }

        private void Update()
        {
            if (player.position.z + config.segmentLength * config.initialSegments > nextZ) SpawnSegment();
            while (activeSegments.Count > config.pooledSegments) Destroy(activeSegments.Dequeue());
        }

        private void SpawnSegment()
        {
            var pool = segmentPools[Random.Range(0, segmentPools.Length)];
            var segment = pool.Get(new Vector3(0, 0, nextZ), Quaternion.identity);
            segment.transform.SetParent(spawnedRoot);
            activeSegments.Enqueue(segment);
            Populate(segment.transform, nextZ);
            nextZ += config.segmentLength;
        }

        private void Populate(Transform parent, float zStart)
        {
            var density = config.obstacleDensity.Evaluate(ServiceLocator.Get<GameManager>().Distance);
            for (var z = zStart + 5f; z < zStart + config.segmentLength; z += 3f)
            {
                if (Random.value < density)
                {
                    var lane = Random.Range(-1, 2);
                    Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], new Vector3(lane * config.laneWidth, .1f, z), Quaternion.Euler(0, Random.Range(0, 360), 0), parent);
                }
                else if (Random.value < .55f)
                {
                    var lane = Random.Range(-1, 2);
                    Instantiate(collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)], new Vector3(lane * config.laneWidth, 1f, z), Quaternion.identity, parent);
                }
            }
        }
    }
}
