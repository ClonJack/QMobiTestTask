using Code.Ship.Data.Camera;
using Code.Ship.Data.Input;
using Code.Ship.Data.Ship;
using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Player
{
    [System.Serializable]
    public class MovePlayerBehavior : IMove
    {
        private readonly ShipOptionMove _shipOption;
        private readonly OptionInput _optionInput;
        private readonly Transform _owner;
        private readonly Vector3 _maxBoundCamera;
        private readonly Vector3 _minBoundCamera;


        private Vector2 _currentVelocity;
        private float _axisVertical;


        public MovePlayerBehavior(ShipOptionMove shipOption, OptionBoundCamera optionBoundCamera,
            OptionInput optionInput, Transform owner)
        {
            _shipOption = shipOption;
            _optionInput = optionInput;
            _maxBoundCamera = Camera.main.ViewportToWorldPoint(optionBoundCamera.MaxBoundCamera);
            _minBoundCamera = Camera.main.ViewportToWorldPoint(optionBoundCamera.MinBoundCamera);
            _owner = owner;
        }


        public void Move()
        {
            _axisVertical = Mathf.SmoothStep(_axisVertical, Input.GetAxis("Vertical"),
                _optionInput.SmothStepForwad * Time.deltaTime);
            if (_axisVertical != 0)
            {
                var target = (_owner.up * (_shipOption.TargetMove * _axisVertical));

                var smothDamp = Vector2.SmoothDamp(_owner.transform.position, target, ref
                    _currentVelocity,
                    _shipOption.SmothSpeed, _shipOption.MaxSpeed);

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

            var rotation = -(Input.GetAxis("Horizontal") * _shipOption.RotationSpeedShip) * Time.deltaTime;
            _owner.Rotate(0, 0, rotation);
        }
    }
}