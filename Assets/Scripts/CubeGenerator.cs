using System.Collections;
using UnityEngine;

namespace CubesGenerator
{
    public class CubeGenerator : MonoBehaviour
    {
        [SerializeField] private CubesPool cubesPool;
        [SerializeField] private Transform cubesContainer;
        [SerializeField] private Transform[] spawnPoints;

        private bool _isEnabled;
        private ConfigData _configData;
        private Coroutine _spawnCoroutine;
        private int _nextSpawnPointIndex;

        public bool IsEnabled => _isEnabled;

        public void StartGenerate(ConfigData configData)
        {
            if (_isEnabled)
            {
                return;
            }

            _configData = configData;
            _isEnabled = true;
            _nextSpawnPointIndex = 0;
            _spawnCoroutine = StartCoroutine(GenerateCoroutine());
        }

        public void StopGenerate()
        {
            if (!_isEnabled)
            {
                return;
            }

            _configData = null;
            _isEnabled = false;
            StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator GenerateCoroutine()
        {
            while (_isEnabled)
            {
                var spawnPoint = GetNextSpawnPoint();
                var cube = cubesPool.Take();
                cube.transform.position = spawnPoint.position;
                cube.transform.SetParent(cubesContainer);
                cube.Init(_configData);
                cube.Move();

                yield return new WaitForSeconds(_configData.SpawnDelay);
            }
        }

        private Transform GetNextSpawnPoint()
        {
            var result = spawnPoints[_nextSpawnPointIndex];
            _nextSpawnPointIndex += 1;
            if (_nextSpawnPointIndex >= spawnPoints.Length)
            {
                _nextSpawnPointIndex = 0;
            }

            return result;
        }
    }
}