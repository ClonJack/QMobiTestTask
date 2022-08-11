using System;
using Code.Pool;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _valueSpawned;
        [SerializeField] private ObjectPool _poolEnemy;

        private int _randX;
        private float _randY;

        private void Create()
        {
            for (int i = 0; i < _valueSpawned; i++)
            {
                var obj = _poolEnemy.GetFreeObject();
                obj.SetParent(null);
                obj.gameObject.SetActive(true);

                var randXCurrent = _randX;
                _randX = Random.Range(0, 1);
                if (_randX == randXCurrent)
                {
                    while (_randX == randXCurrent)
                    {
                        _randX = Random.Range(0, 2);
                    }
                }

                _randY = Random.Range(0, 1f);
                obj.position = Camera.main.ViewportToWorldPoint(new Vector3(_randX, _randY, 0));
            }
        }

        private void Start()
        {
            Create();
        }
    }
}