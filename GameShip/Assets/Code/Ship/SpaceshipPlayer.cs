using System.Collections.Generic;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Behavior.Player;
using Code.Ship.Data.Camera;
using Code.Ship.Data.Input;
using Code.Ship.Data.Ship;
using UnityEngine;

namespace Code.Ship
{
    public class SpaceshipPlayer : BaseShip
    {
        [SerializeField] private ShipOptionMove _shipOptionMove;
        [SerializeField] private ShipOptionShot _shipOptionShot;
        [SerializeField] private OptionInput _optionInput;
        [SerializeField] private OptionBoundCamera _optionBoundCamera;

        [Header("Effects ")] [SerializeField] private List<ParticleSystem> _particleGun;
        [Header("Pool")] [SerializeField] private ObjectPool _objectPool;

        private void Awake()
        {
            SetMove(new MovePlayerBehavior(_shipOptionMove, _optionBoundCamera,_optionInput, transform));
            SetAttack(new AttackPlayerBehavior(_shipOptionShot,this,_particleGun,_objectPool));
            
        }


        private void Update()
        {
             Move.Move();
 
             if (Input.GetKeyDown(_optionInput.KeyOnAttack))
             {
                 Attack.Attack();
             }
        }
    }
}