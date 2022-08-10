using System.Collections.Generic;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Player;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipPlayer : BaseShip
    {
        [SerializeField] private float _speedShip;
        [SerializeField] private float _rotationSpeedShip;

        [SerializeField] private float _durationLazer;


        [SerializeField] private List<ParticleSystem> _particleGun;

        [SerializeField] private KeyCode _keyOnAttack;

        [SerializeField] private ObjectPool _objectPool;


        private void Awake()
        {
            SetMove(new MovePlayerBehavior(_speedShip, _rotationSpeedShip, transform));
            SetAttack(new AttackPlayerBehavior(_particleGun, _objectPool, _durationLazer, this));
        }


        private void Update()
        {
            Move.Move();

            if (Input.GetKeyDown(_keyOnAttack))
            {
                Attack.Attack();
            }

            var gg = _particleGun[0].transform.up * 15 - _particleGun[0].transform.position;
            Debug.DrawLine(_particleGun[0].transform.position, gg, Color.red);
        }
    }
}