using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Base
{
    public abstract class BaseShip : MonoBehaviour
    {
        protected IMove Move;
        protected IAttack Attack;

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