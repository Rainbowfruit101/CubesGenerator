using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CubesGenerator
{
    public class CubesPool : MonoBehaviour
    {
        [SerializeField] private int amountOfPreparedCubes = 10;
        [SerializeField] private Cube cubePrefab;

        private List<Cube> _cubes;

        private void Awake()
        {
            _cubes = new List<Cube>();

            for (var i = 0; i < amountOfPreparedCubes; i++)
            {
                var cube = CreateCube();
                _cubes.Add(cube);
            }
        }

        public Cube Take()
        {
            var result = _cubes.Count > 0 ? _cubes.Last() : CreateCube();
            _cubes.Remove(result);
            result.transform.SetParent(null);
            result.gameObject.SetActive(true);
            return result;
        }

        public void Put(Cube cube)
        {
            if (_cubes.Contains(cube))
            {
                return;
            }
            
            cube.transform.SetParent(transform);
            cube.gameObject.SetActive(false);
            _cubes.Add(cube);
        }

        private Cube CreateCube()
        {
            var cube = Instantiate(cubePrefab.gameObject, transform).GetComponent<Cube>();
            cube.SetPool(this);
            cube.gameObject.SetActive(false);
            return cube;
        }
    }
}