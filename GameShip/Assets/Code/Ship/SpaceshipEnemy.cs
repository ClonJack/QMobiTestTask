using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Enemy;
using Code.Ship.Data.Ship;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipEnemy : BaseShip
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

        private void Awake()
        {
            SetMove(new MoveEnemyBehavior(transform, _player, _randMove,
                _timeOnUpdatePostionMove, _smothMove));
            SetAttack(new AttackEnemyBehavior(_shipOptionShot, _particleGun,
                _lazerPool, this));
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

        private void Damage()
        {
            var explosion = _effectPool.GetFreeObject().GetComponent<ParticleSystem>();
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();

            _enemyPool.ReturnToPool(gameObject);
        }


        private void OnEnable()
        {
            TakeDamageAction += Damage;
        }

        private void OnDisable()
        {
            TakeDamageAction -= Damage;
        }
    }
}