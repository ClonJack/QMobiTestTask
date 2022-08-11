using Code.Interfaces;
using Code.Pool;
using UnityEngine;


namespace Code.Planet.Behavior.Enemy
{
    public class MoveEnemyPlanetBehavior : IMove
    {
        private readonly Transform _owner;
        private readonly ObjectPool _objectPool;
        private readonly float _speedMove;

        private Vector3 _mirrorPos;
        

        public MoveEnemyPlanetBehavior(Transform owner, ObjectPool objectPool,float speedMove)
        {
            _owner = owner;
            _objectPool = objectPool;
            _speedMove = speedMove;
            _mirrorPos = -(_owner.position + new Vector3(Random.Range(1, 4), Random.Range(1, 4)));
        }


        public void Move()
        {
            var time = _speedMove * Time.deltaTime;

            _owner.position = Vector3.MoveTowards(_owner.position, _mirrorPos, time);
            
            var distance = (_mirrorPos - _owner.position).sqrMagnitude;
            if (distance < 0.25f)
            {
                _objectPool.ReturnToPool(_owner.gameObject);
            }
        }
    }
}