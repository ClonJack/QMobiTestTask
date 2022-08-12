using Code.Pool;
using Code.Ship.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Spawn.Enemy
{
    public class SpawnerEnemy : SpawnerBase
    {
        [SerializeField] private float _timeOnSpawn;
        [SerializeField] private int _minSpawnObject;
        [SerializeField] private int _maxSpawnObject;
        [SerializeField] private ObjectPool _poolEnemy;

        [SerializeField] private Vector3Int _randRangeX;
        [SerializeField] private Vector3 _randRangeY;

        private int _randX;
        private float _randY;
        private float _timeElasped;

        private void Start()
        {
            Spawn();
        }

        private void Update()
        {
            if (_timeElasped > _timeOnSpawn)
            {
                Spawn();
                _timeElasped = 0;
            }

            _timeElasped += Time.deltaTime;
        }
        public override void Spawn()
        {
            if (_minSpawnObject > _poolEnemy.ObjectsPool.Count)
            {
                _minSpawnObject = _maxSpawnObject;
            }

            _minSpawnObject = Random.Range(_minSpawnObject, _maxSpawnObject);


            for (var i = 0; i < _minSpawnObject; i++)
            {
                var obj = _poolEnemy.GetFreeObject();
                if (obj == null) return;
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