using System;
using System.Collections;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Enemy;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipEnemy : BaseShip
    {
        [Header("Option ship")] 
        [SerializeField] private float _timeOnShot;
        [SerializeField] private float _speedMoveLazer;
        [SerializeField] private float _timeOnUpdatePostionMove;
        [SerializeField] private float _smothMove;
        [SerializeField] private float _timeUpdateMove;
        [SerializeField] private float _distanceDetectedAttack;
        [SerializeField] private Vector3Int _randMove;
        [SerializeField] private LayerMask _layerAttack;


        [Header("Pool")] [SerializeField] private ObjectPool _lazerPool;
        [SerializeField] private ObjectPool _effectPool;
        [SerializeField] private ObjectPool _enemyPool;

        [Header("Target Attack")]
        [SerializeField] private Transform _player;

        [Header("Effect")]
        [SerializeField] private ParticleSystem _particleGun;

        private float _timeElapsedShot;

        private void Awake()
        {
            SetMove(new MoveEnemyBehavior(transform, _player, _randMove, _timeOnUpdatePostionMove, _smothMove));
            SetAttack(new AttackEnemyBehavior(_particleGun, _lazerPool, _speedMoveLazer, this,
                _timeUpdateMove, _layerAttack, _distanceDetectedAttack));
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