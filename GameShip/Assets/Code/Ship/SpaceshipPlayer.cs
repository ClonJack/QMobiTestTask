using System.Collections.Generic;
using Code.Interfaces;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Player;
using Code.Ship.Data.Camera;
using Code.Ship.Data.Input;
using Code.Ship.Data.Ship;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Ship
{
    public class SpaceshipPlayer : BaseShip, IDamage
    {
        [SerializeField] private ShipOptionMove _shipOptionMove;
        [SerializeField] private ShipOptionShot _shipOptionShot;
        [SerializeField] private OptionInput _optionInput;
        [SerializeField] private OptionBoundCamera _optionBoundCamera;

        [Header("Effects ")] [SerializeField] private List<ParticleSystem> _particleGun;

        [Header("Pool")] [SerializeField] private ObjectPool _effectPool;

        [SerializeField] private ObjectPool _lazerPool;

        [Header("State Player")] [SerializeField]
        private float _health;

        private float _maxHealth;

        [SerializeField] private Slider _hpBar;
        
        private void Awake()
        {
            SetMove(new MovePlayerBehavior(_shipOptionMove, _optionBoundCamera, _optionInput, transform));
            SetAttack(new AttackPlayerBehavior(_shipOptionShot, this, _particleGun, _lazerPool));
        }
        private void Start()
        {
            _maxHealth = _health;

            _hpBar.value = (1 / _maxHealth) * _health;
        }
        private void Update()
        {
            Move.Move();

            if (Input.GetKeyDown(_optionInput.KeyOnAttack))
            {
                Attack.Attack();
            }
        }
        public void TakeDamage(float damage)
        {
            _health -= damage;
            _hpBar.value = ((1f / _maxHealth) * _health);
            
            if (_health > 0) return;

            var explosion = _effectPool.GetFreeObject().GetComponent<ParticleSystem>();
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();

            gameObject.SetActive(false);

            SceneManager.LoadScene(0);
        }
    }
}