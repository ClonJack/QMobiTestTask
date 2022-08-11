﻿using Code.Pool;
using Code.Ship.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Spawn.Comet
{
    public class SpawnerPlanet : SpawnerBase
    {
        [SerializeField] private int _minSpawnObject;
        [SerializeField] private int _maxSpawnObject;

        [SerializeField] private ObjectPool _poolPlanets;


        [SerializeField] private Vector2 _randPosMin;
        [SerializeField] private Vector2 _randPosMax;

        [SerializeField] private Vector3 _mirrorDirectional;

        public override void Spawn()
        {
            if (_minSpawnObject > _maxSpawnObject)
            {
                _minSpawnObject = _maxSpawnObject;
            }

            var currentSpawnedObject = Random.Range(_minSpawnObject, _maxSpawnObject);

            for (int i = 0; i < currentSpawnedObject; i++)
            {
                var planet = _poolPlanets.GetFreeObject();
                if (planet == null) return;

                planet.SetParent(null);


                planet.position =
                    Camera.main.ViewportToWorldPoint(new Vector3(
                        Random.Range(_randPosMin.x, _randPosMax.x),
                        Random.Range(_randPosMin.y, _randPosMax.y)));

                planet.gameObject.SetActive(true);
            }
        }
    }
}