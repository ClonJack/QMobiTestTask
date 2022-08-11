using System;
using Code.Pool;
using Code.Ship.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class SpawnerEnemy : SpawnerBase
    {
        [SerializeField] private int _minSpawnObject;
        [SerializeField] private ObjectPool _poolEnemy;

        [SerializeField] private Vector3Int _randRangeX;
        [SerializeField] private Vector3 _randRangeY;

        private int _randX;
        private float _randY;
        
        private void Start()
        {
            Spawn();
        }
        public override void Spawn()
        {
            if (_minSpawnObject > _poolEnemy.ObjectsPool.Count)
            {
                _minSpawnObject = _poolEnemy.ObjectsPool.Count;
            }

            _minSpawnObject = Random.Range(_minSpawnObject, _poolEnemy.ObjectsPool.Count);
            

            for (int i = 0; i < _minSpawnObject; i++)
            {
                var obj = _poolEnemy.GetFreeObject();
                obj.SetParent(null);
                obj.gameObject.SetActive(true);

                if (_randRangeX != Vector3Int.zero)
                {
                    var randXCurrent = _randX;
                    _randX = Random.Range(_randRangeX.x, _randRangeX.y);
                    if (_randX == randXCurrent)
                    {
                        while (_randX == randXCurrent)
                        {
                            _randX = Random.Range(_randRangeX.x, _randRangeX.y);
                        }
                    }
                }

                _randY = Random.Range(_randRangeY.x, _randRangeY.y);
                
                obj.position = Camera.main.ViewportToWorldPoint(new Vector3(_randX, _randY, 0));
            }
        }
    }
}