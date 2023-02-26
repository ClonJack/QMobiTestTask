﻿using UnityEngine;
using Random = UnityEngine.Random;

namespace ShipGame
{
    public class SpawnerPlanet : MonoBehaviour
    {
        [SerializeField] private int _minSpawnObject;
        [SerializeField] private int _maxSpawnObject;

        [SerializeField] private Vector2 _randPosMin;
        [SerializeField] private Vector2 _randPosMax;

        public void Spawn(ObjectPool poolPlanets)
        {
            if (_minSpawnObject > _maxSpawnObject)
            {
                _minSpawnObject = _maxSpawnObject;
            }

            var currentSpawnedObject = Random.Range(_minSpawnObject, _maxSpawnObject);

            for (var i = 0; i < currentSpawnedObject; i++)
            {
                var planet = poolPlanets.GetFreeObject();
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