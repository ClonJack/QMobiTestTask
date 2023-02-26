using UnityEngine;

namespace ShipGame
{
    public class PoolService : MonoBehaviour
    {
        public ObjectPool PoolLazer;
        public ObjectPool PoolExplosion;
        public ObjectPool PoolPlanets;
        public ObjectPool PoolEnemy;

        public void Init()
        {
            PoolLazer.Init();
            PoolExplosion.Init();
            PoolPlanets.Init();
            PoolEnemy.Init();
        }
    }
}