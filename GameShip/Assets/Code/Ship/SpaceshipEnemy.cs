using Code.Interfaces;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Enemy;
using Code.Ship.Data.Ship;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipEnemy : BaseShip, IDamage
    {
        [SerializeField] private ShipOptionShot _shipOptionShot;
        [SerializeField] private float _timeOnShot;

        [Header("Option Move Ship")] [SerializeField]
        private float _timeOnUpdatePostionMove;

        [SerializeField] private float _smothMove;
        [SerializeField] private Vector3Int _randMove;

        [Header("Pool")] [SerializeField] private ObjectPool _lazerPool;
        [SerializeField] private ObjectPool _effectPool;
        [SerializeField] private ObjectPool _enemyPool;

        [Header("Target Attack")] [SerializeField]
        private Transform _player;

        [Header("Effect")] [SerializeField] private ParticleSystem _particleGun;

        private float _timeElapsedShot;

        private AttackEnemyBehavior _attackEnemyBehavior;

        private void Awake()
        {
            SetMove(new MoveEnemyBehavior(transform, _player, _randMove,
                _timeOnUpdatePostionMove, _smothMove));

            _attackEnemyBehavior = new AttackEnemyBehavior(_shipOptionShot, _particleGun,
                _lazerPool, this);
            SetAttack(_attackEnemyBehavior);
        }

        private void Update()
        {
            Move.Move();

            _timeElapsedShot += Time.deltaTime;
            if (_timeElapsedShot > _timeOnShot)
            {
                _timeElapsedShot = 0;
                Attack.Attack();
            }
        }

        public void TakeDamage(float damage)
        {
            var explosion = _effectPool.GetFreeObject().GetComponent<ParticleSystem>();
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();

            if (_attackEnemyBehavior.Lazer != null)
                _lazerPool.ReturnToPool(_attackEnemyBehavior.Lazer.gameObject);

            _enemyPool.ReturnToPool(gameObject);
        }
    }
}