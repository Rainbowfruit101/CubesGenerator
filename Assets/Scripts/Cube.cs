using System.Collections;
using UnityEngine;

namespace CubesGenerator
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        private ConfigData _configData;
        private Coroutine _moveCoroutine;
        private CubesPool _cubesPool;

        private void Awake()
        {
            renderer.sharedMaterial = new Material(renderer.material);
        }

        public void Init(ConfigData configData)
        {
            _configData = configData;

            renderer.sharedMaterial.color = GetRandomColor();
        }

        public void Move()
        {
            if (_moveCoroutine != null)
            {
                return;
            }
            _moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        public void Stop()
        {
            if (_moveCoroutine == null)
            {
                return;
            }

            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        private IEnumerator MoveCoroutine()
        {
            var timer = _configData.Distance / _configData.Speed;
            while (timer > 0f)
            {
                transform.position += Vector3.forward * (_configData.Speed * Time.deltaTime);
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _moveCoroutine = null;
            _cubesPool.Put(this);
        }

        private Color GetRandomColor()
        {
            var h = Random.value;
            return Color.HSVToRGB(h, 1f, 1f);
        }

        public void SetPool(CubesPool cubesPool)
        {
            _cubesPool = cubesPool;
        }
    }
}