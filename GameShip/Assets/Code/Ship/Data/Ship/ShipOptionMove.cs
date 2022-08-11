using UnityEngine;

namespace Code.Ship.Data.Ship
{
    [System.Serializable]
    public class ShipOptionMove
    {
        [Header("Move Ship")] 
        [SerializeField] private float _smothSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _targetMove;

        [Header("Rotate Ship")] 
        [SerializeField] private float _rotationSpeedShip;

        public float SmothSpeed => _smothSpeed;
        public float MaxSpeed => _maxSpeed;
        public float TargetMove => _targetMove;
        public float RotationSpeedShip => _rotationSpeedShip;
    }
}