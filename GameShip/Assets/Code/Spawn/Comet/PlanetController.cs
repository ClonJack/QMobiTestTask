using System;
using System.Collections;
using System.Collections.Generic;
using Code.Pool;
using Code.Ship.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Spawn.Comet
{
    public class PlanetController : MonoBehaviour
    {
        [SerializeField] private List<SpawnerBase> _spawnPlanet;

        [SerializeField] private float _timeUpdatePlanet;


        private float _elaspedTimeUpdate;

        [SerializeField] private int _minSpawnWave;
        [SerializeField] private float _timeBetweenSpawn;

        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_timeBetweenSpawn);
        }

        private void Start()
        {
            StartCoroutine(UpdatePlanets());
        }

        private IEnumerator UpdatePlanets()
        {
            var randSpawner = Random.Range(_minSpawnWave, _spawnPlanet.Count);

            for (int i = 0; i < randSpawner; i++)
            {
                _spawnPlanet[i].Spawn();

                yield return _waitForSeconds;
            }
        }

        private void Update()
        {
            if (_elaspedTimeUpdate > _timeUpdatePlanet)
            {
                _elaspedTimeUpdate = 0;
                StartCoroutine(UpdatePlanets());
            }

            _elaspedTimeUpdate += Time.deltaTime;
        }
    }
}