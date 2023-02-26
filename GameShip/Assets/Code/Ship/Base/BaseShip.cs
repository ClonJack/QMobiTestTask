using UnityEngine;

namespace ShipGame
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

        public virtual void Init()
        {
        }
    }
}