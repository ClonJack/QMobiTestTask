using UnityEngine;

namespace ShipGame
{
    [System.Serializable]
    public class MovePlayerBehavior : IMove
    {
        private readonly SpaceshipPlayer _player;
        private readonly Vector3 _maxBoundCamera;
        private readonly Vector3 _minBoundCamera;

        private Vector2 _currentVelocity;
        private float _axisVertical;

        public MovePlayerBehavior(SpaceshipPlayer player)
        {
            _player = player;
            _maxBoundCamera = Camera.main.ViewportToWorldPoint(player.ShipOptionBoundCamera.MaxBoundCamera);
            _minBoundCamera = Camera.main.ViewportToWorldPoint(player.ShipOptionBoundCamera.MinBoundCamera);
        }


        public void Move()
        {
            _axisVertical = Mathf.SmoothStep(_axisVertical, Input.GetAxis(_player.ShipOptionInput.KeyOnVerticalAxis),
                _player.ShipOptionInput.SmoothStepForward * Time.deltaTime);
            if (_axisVertical != 0)
            {
                var target = (_player.transform.up * (_player.ShipOptionMove.TargetMove * _axisVertical));

                var smothDamp = Vector2.SmoothDamp(_player.transform.position, target, ref
                    _currentVelocity,
                    _player.ShipOptionMove.SmothSpeed, _player.ShipOptionMove.MaxSpeed);

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

                _player.transform.position = smothDamp;
            }

            var rotation = -(Input.GetAxis(_player.ShipOptionInput.KeyOnHorizontalAxis) * _player.ShipOptionMove.RotationSpeedShip) * Time.deltaTime;
            _player.transform.Rotate(0, 0, rotation);
            _player.transform.Rotate(0, 0, rotation);
        }
    }
}