using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ShipGame
{
    public class SpaceshipPlayer : BaseShip, IDamage, ICancellationToken
    {
        [SerializeField] private ShipOptionMove _shipOptionMove;
        [SerializeField] private ShipOptionShot _shipOptionShot;
        [SerializeField] private ShipOptionInput _shipOptionInput;
        [SerializeField] private ShipOptionBoundCamera _shipOptionBoundCamera;

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
        public ShipOptionInput ShipOptionInput => _shipOptionInput;
        public ShipOptionBoundCamera ShipOptionBoundCamera => _shipOptionBoundCamera;

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

            if (Input.GetButtonDown(_shipOptionInput.KeyOnAttack))
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
        
        public void OnDestroy()
        {
            OnDeath = null;
            OnChangeHp = null;
            CancellationToken.Cancel();
        }
    }
}