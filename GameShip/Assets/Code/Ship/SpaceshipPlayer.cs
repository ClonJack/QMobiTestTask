using System;
using System.Collections.Generic;
using System.Threading;
using Code.Interfaces;
using Code.Level;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Player;
using Code.Ship.Data.Camera;
using Code.Ship.Data.Input;
using Code.Ship.Data.Ship;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipPlayer : BaseShip, IDamage, ICancellationToken
    {
        [SerializeField] private ShipOptionMove _shipOptionMove;
        [SerializeField] private ShipOptionShot _shipOptionShot;
        [SerializeField] private OptionInput _optionInput;
        [SerializeField] private OptionBoundCamera _optionBoundCamera;

        [Header("Effects ")] [SerializeField] private List<ParticleSystem> _particleGun;
        [SerializeField] private PoolService _poolService;

        [Header("State Player")] [SerializeField]
        private float _health;

        public Action<float> OnChangeHp;
        public Action OnDeath;
        public CancellationTokenSource CancellationToken { get; set; }
        public float Health => _health;
        public List<ParticleSystem> ParticleGun => _particleGun;
        public PoolService PoolService => _poolService;
        public ShipOptionMove ShipOptionMove => _shipOptionMove;
        public ShipOptionShot ShipOptionShot => _shipOptionShot;
        public OptionInput OptionInput => _optionInput;
        public OptionBoundCamera OptionBoundCamera => _optionBoundCamera;

        public override void Init()
        {
            CancellationToken = new CancellationTokenSource();

            SetMove(new MovePlayerBehavior(this));
            SetAttack(new AttackPlayerBehavior(this));
        }

        public void SetPoolService(PoolService poolService)
        {
            _poolService = poolService;
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

            OnChangeHp?.Invoke(_health);
            if (_health > 0) return;

            var explosion = _poolService.PoolExplosion.GetFreeObject().GetComponent<ParticleSystem>();
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();
            
            gameObject.SetActive(false);
            OnDeath.Invoke();
        }

        private void OnChangeLevel()
        {
            throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            OnDeath = null;
            OnChangeHp = null;
            CancellationToken.Cancel();
        }
    }
}