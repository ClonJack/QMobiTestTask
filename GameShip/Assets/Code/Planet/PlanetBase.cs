using Code.Interfaces;
using UnityEngine;

namespace Code.Planet
{
    public abstract class PlanetBase : MonoBehaviour
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