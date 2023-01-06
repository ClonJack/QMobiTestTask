using Code.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Enemy
{
    public class MoveEnemyBehavior : IMove
    {
        private readonly SpaceshipEnemy _spaceshipEnemy;

        private float _timeElapsedUpdate;
        private Vector3 _targetMove;
        private Vector3 _velocity;

        public MoveEnemyBehavior(SpaceshipEnemy spaceshipEnemy)
        {
            _spaceshipEnemy = spaceshipEnemy;
            _targetMove = _spaceshipEnemy.transform.position +
                          RandVector(_spaceshipEnemy.RandMove.x, _spaceshipEnemy.RandMove.y);
        }

        private Vector3 RandVector(int x, int y)
        {
            return new Vector3(Random.Range(-x, x), Random.Range(-y, y));
        }

        public void Move()
        {
            Debug.DrawRay(_spaceshipEnemy.transform.position, -_spaceshipEnemy.transform.up * 5, Color.cyan);

            if (_timeElapsedUpdate > _spaceshipEnemy.TimeOnShot)
            {
                _timeElapsedUpdate = 0;
                _targetMove = _spaceshipEnemy.Target.position +
                              RandVector(_spaceshipEnemy.RandMove.x, _spaceshipEnemy.RandMove.y);
            }

            _spaceshipEnemy.transform.position = Vector3.SmoothDamp(_spaceshipEnemy.transform.position, _targetMove,
                ref _velocity, _spaceshipEnemy.SmothMove);

            var direction = _spaceshipEnemy.Target.position - _spaceshipEnemy.transform.position;
            var rotation = Quaternion.LookRotation(direction, _spaceshipEnemy.transform.forward);
            rotation.y = 0;
            rotation.x = 0;
            _spaceshipEnemy.transform.rotation = rotation;

            _timeElapsedUpdate += Time.deltaTime;
        }
    }
}