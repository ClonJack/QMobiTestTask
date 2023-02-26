using UnityEngine;
using UnityEngine.UI;

namespace ShipGame
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;
        public void Init(SpaceshipPlayer player)
        {
            _healthBar.maxValue = player.Health;
            _healthBar.value = _healthBar.maxValue;
            
            player.OnChangeHp += RepaintHpBar;
        }
        private void RepaintHpBar(float value)
        {
            _healthBar.value = value;
        }
    }
}