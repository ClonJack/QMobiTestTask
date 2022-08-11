using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Enemy
{
    public class MoveEnemyBehavior : IMove
    {
        private readonly Transform _owner;
        private readonly Transform _pointToMove;
        private readonly Vector3Int _randomMove;
        private readonly float _timeOnUpdate;
        private readonly float _smothMove;

        private float _timeElapsedUpdate;
        private Vector3 _targetMove;


        private Vector3 _velocity;

        public MoveEnemyBehavior(Transform owner, Transform pointToMove, Vector3Int randomMove, float timeOnUpdate,
            float smothMove)
        {
            _owner = owner;
            _pointToMove = pointToMove;
            _randomMove = randomMove;
            _timeOnUpdate = timeOnUpdate;
            _smothMove = smothMove;

            _targetMove = pointToMove.position + _randomMove;
        }


        private Vector3 RandVector(int x, int y)
        {
            return new Vector3(Random.Range(-x, x), Random.Range(-y, y));
        }

        public void Move()
        {
            Debug.DrawRay(_owner.position, -_owner.up * 5, Color.cyan);

            if (_timeElapsedUpdate > _timeOnUpdate)
            {
                _timeElapsedUpdate = 0;
                _targetMove = _pointToMove.position + RandVector(_randomMove.x, _randomMove.y);
            }

            _owner.position = Vector3.SmoothDamp(_owner.position, _targetMove, ref _velocity, _smothMove);

            var direction = _pointToMove.position - _owner.position;
            var rotation = Quaternion.LookRotation(direction, _owner.forward);
            rotation.y = 0;
            rotation.x = 0;
            _owner.rotation = rotation;

            _timeElapsedUpdate += Time.deltaTime;
        }
    }
}