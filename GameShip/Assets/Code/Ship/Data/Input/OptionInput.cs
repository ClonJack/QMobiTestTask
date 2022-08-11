using UnityEngine;

namespace Code.Ship.Data.Input
{
    [System.Serializable]
    public class OptionInput
    {
        [Header("Input")] [SerializeField] private KeyCode _keyOnAttack;
        [SerializeField] private float _smothStepForward;

        public float SmothStepForwad => _smothStepForward;
        public KeyCode KeyOnAttack => _keyOnAttack;
    }
}