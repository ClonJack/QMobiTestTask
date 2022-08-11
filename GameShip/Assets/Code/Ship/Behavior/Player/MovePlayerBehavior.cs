using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Player
{
    [System.Serializable]
    public class MovePlayerBehavior : IMove
    {
        private readonly float _smothSpeed;
        private readonly float _maxSmothSpeed;
        private readonly float _rotationSpeedShip;
        private readonly Transform _owner;
        private readonly Vector3 _minBoundCamera;
        private readonly float _smothSteepAxis;
        private readonly float _targetMove;
        private readonly Vector3 _maxBoundCamera;

        private Vector2 _currentVelocity;

        private float _axisVertical;


        public MovePlayerBehavior(float smothSpeed, float maxSmothSpeed, float rotationSpeedShip, Transform owner,
            Vector3 minBoundCamera, Vector3 maxBoundCamera, float smothSteepAxis, float targetMove)
        {
            _smothSpeed = smothSpeed;
            _maxSmothSpeed = maxSmothSpeed;
            _rotationSpeedShip = rotationSpeedShip;
            _owner = owner;
            _minBoundCamera = minBoundCamera;
            _smothSteepAxis = smothSteepAxis;
            _targetMove = targetMove;
            _maxBoundCamera = maxBoundCamera;
        }

        public void Move()
        {
            _axisVertical =
                Mathf.SmoothStep(_axisVertical, Input.GetAxis("Vertical"), _smothSteepAxis * Time.deltaTime);
            if (_axisVertical != 0)
            {
                var target = (_owner.up * (_targetMove * _axisVertical));

                var smothDamp = Vector2.SmoothDamp(_owner.transform.position, target, ref
                    _currentVelocity,
                    _smothSpeed, _maxSmothSpeed);

                if (smothDamp.y > _maxBoundCamera.y)
                {
                    smothDamp = -smothDamp + Vector2.up;
                }

                else if (smothDamp.y < _minBoundCamera.y)
                {
                    smothDamp = -smothDamp + Vector2.down;
                }
                else if (smothDamp.x > _maxBoundCamera.x)
                {
                    smothDamp = -smothDamp + Vector2.right;
                }

                else if (smothDamp.x < _minBoundCamera.x)
                {
                    smothDamp = -smothDamp + Vector2.left;
                }

                _owner.transform.position = smothDamp;
            }

            var rotation = -(Input.GetAxis("Horizontal") * _rotationSpeedShip) * Time.deltaTime;
            _owner.Rotate(0, 0, rotation);
        }
    }
}