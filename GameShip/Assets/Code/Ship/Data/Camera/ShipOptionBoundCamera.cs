using UnityEngine;

namespace ShipGame
{
    [System.Serializable]
    public class ShipOptionBoundCamera
    {
        [Header("Option Bound ")] 
        [SerializeField] private Vector3 _minBoundCamera;
        [SerializeField] private Vector3 _maxBoundCamera;

        public Vector3 MinBoundCamera => _minBoundCamera;
        public Vector3 MaxBoundCamera => _maxBoundCamera;
    }
}