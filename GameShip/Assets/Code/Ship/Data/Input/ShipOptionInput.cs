using UnityEngine;

namespace ShipGame
{
    [System.Serializable]
    public class ShipOptionInput
    {
        [Header("Input")] [SerializeField] private string _keyOnAttack = "Attack";
        [SerializeField] private string _keyOnVerticalAxis = "Vertical";
        [SerializeField] private string _keyOnHorizontalAxis = "Horizontal";
        
        [SerializeField] private float _smoothStepForward;

        public float SmoothStepForward => _smoothStepForward;
        public string KeyOnVerticalAxis => _keyOnVerticalAxis;
        public string KeyOnHorizontalAxis => _keyOnHorizontalAxis;
        public string KeyOnAttack => _keyOnAttack;
    }
}