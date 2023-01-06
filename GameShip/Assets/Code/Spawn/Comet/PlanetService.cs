using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Spawn.Comet
{
    public class PlanetService : MonoBehaviour
    {
        [SerializeField] private List<SpawnerPlanet> _spawnPlanet;

        [SerializeField] private int _minSpawnWave;
        [SerializeField] private int _millisecondsDelayBetweenSpawn;
        [SerializeField] private float _timeUpdatePlanet;

        private PoolService _poolService;

        private float _elaspedTimeUpdate;

        public void SetPoolService(PoolService poolService)
        {
            _poolService = poolService;
        }

        private async void UpdatePlanets()
        {
            var randSpawner = Random.Range(_minSpawnWave, _spawnPlanet.Count);

            for (int i = 0; i < randSpawner; i++)
            {
                _spawnPlanet[i].Spawn(_poolService.PoolPlanets);
                await Task.Delay(_millisecondsDelayBetweenSpawn);
            }
        }

        private void Update()
        {
            if (_elaspedTimeUpdate > _timeUpdatePlanet)
            {
                _elaspedTimeUpdate = 0;
                UpdatePlanets();
            }

            _elaspedTimeUpdate += Time.deltaTime;
        }
    }
}