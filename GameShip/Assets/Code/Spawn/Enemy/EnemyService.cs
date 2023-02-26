using UnityEngine;
using Random = UnityEngine.Random;

namespace ShipGame
{
    public class EnemyService : MonoBehaviour
    {
        [SerializeField] private float _timeOnSpawn;
        [SerializeField] private int _minSpawnObject;
        [SerializeField] private int _maxSpawnObject;

        [SerializeField] private Vector3Int _randRangeX;
        [SerializeField] private Vector3 _randRangeY;

        private PoolService _poolService;
        private int _randX;
        private float _randY;
        private float _timeElasped;
        
        private void Spawn()
        {
            if (_minSpawnObject > _poolService.PoolEnemy.ObjectsPool.Count)
            {
                _minSpawnObject = _maxSpawnObject;
            }

            _minSpawnObject = Random.Range(_minSpawnObject, _maxSpawnObject);
            for (var i = 0; i < _minSpawnObject; i++)
            {
                var obj = _poolService.PoolEnemy.GetFreeObject();
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
        public void SetPoolService(PoolService poolService)
        {
            _poolService = poolService;
        }
        public void Init(PoolService poolService, Transform target)
        {
            foreach (var pool in _poolService.PoolEnemy.ObjectsPool)
            {
                var enemy = pool.GetComponent<SpaceshipEnemy>();
                enemy.SetTarget(target);
                enemy.SetServicePool(poolService);
                enemy.Init();
            }
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
    }
}