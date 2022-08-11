using System;
using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Base
{
    public abstract class BaseShip : MonoBehaviour
    {
        protected IMove Move;
        protected IAttack Attack;

        protected Action TakeDamageAction;

        public void TakeDamage() => TakeDamageAction?.Invoke();

        protected void SetMove(IMove move)
        {
            Move = move;
        }

        protected void SetAttack(IAttack attack)
        {
            Attack = attack;
        }
    }
}