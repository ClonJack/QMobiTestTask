using Code.Planet.Behavior.Enemy;
using Code.Pool;
using UnityEngine;

namespace Code.Planet
{
    public class PlanetEnemy : PlanetBase
    {
        [SerializeField] private ObjectPool _planetPool;
        [SerializeField] private float _speedMove;

        private void Awake()
        {
            SetMove(new MoveEnemyPlanetBehavior(transform, _planetPool, _speedMove));
        }

        private void Update()
        {
            Move.Move();
        }
    }
}