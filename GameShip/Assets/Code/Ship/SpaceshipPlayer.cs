using System.Collections.Generic;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Player;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipPlayer : BaseShip
    {
        [Header("Option ship")] [SerializeField]
        private float _smothSpeed;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeedShip;
        [SerializeField] private float _durationLazer;
        [SerializeField] private float _targetMove;
        [SerializeField] private LayerMask _layerAttack;
        [SerializeField] private int _distanceMoveShot;
        [SerializeField] private float _timeMoveShot;
        [SerializeField] private float _distanceDetectedShot;

        [Header("Option Bound ")] [SerializeField]
        private Vector3 _minBoundCamera;

        [SerializeField] private Vector3 _maxBoundCamera;

        [Header("Input")] [SerializeField] private KeyCode _keyOnAttack;
        [SerializeField] private float _smothSteepAxis;

        [Header("Effects ")] [SerializeField] private List<ParticleSystem> _particleGun;
        [Header("Pool")] [SerializeField] private ObjectPool _objectPool;

        private void Awake()
        {
            SetMove(new MovePlayerBehavior(_smothSpeed, _maxSpeed,
                _rotationSpeedShip,
                transform, Camera.main.ViewportToWorldPoint(_minBoundCamera),
                Camera.main.ViewportToWorldPoint(_maxBoundCamera), _smothSteepAxis, _targetMove));

            SetAttack(new AttackPlayerBehavior(_particleGun, _objectPool, _durationLazer,
                this, _layerAttack, _distanceMoveShot, _timeMoveShot, _distanceDetectedShot));
        }


        private void Update()
        {
            Move.Move();

            if (Input.GetKeyDown(_keyOnAttack))
            {
                Attack.Attack();
            }
        }
    }
}