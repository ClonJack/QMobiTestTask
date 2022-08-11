using Code.Interfaces;
using UnityEngine;

namespace Code.Planet
{
    public abstract class  PlanetBase : MonoBehaviour
    {
        protected IMove Move;

        protected void SetMove(IMove move)
        {
            Move = move;
        }
    }
}
