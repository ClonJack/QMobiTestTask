using UnityEngine;

namespace Code.Ship.Data.Ship
{
    [System.Serializable]
    public class ShipOptionShot
    {
        [Header("Option Shot")] [SerializeField]
        private float _timeAliveLazer;

        [SerializeField] private float _speedMoveLazer;
        [SerializeField] private float _distanceDetectedShot;
        [SerializeField] private int _distanceMoveLazer;
        [SerializeField] private float _damage;

        [Header("Layer Attack")] [SerializeField]
        private LayerMask _layerAttack;

        public float TimeAliveLazer => _timeAliveLazer;
        public float SpeedMoveLazer => _speedMoveLazer;
        public float DistanceDetectedShot => _distanceDetectedShot;
        public float DistanceMoveLazer => _distanceMoveLazer;

        public float Damage => _damage;

        public LayerMask LayerAttack => _layerAttack;
    }
}