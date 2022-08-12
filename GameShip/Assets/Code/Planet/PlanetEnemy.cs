using Code.Interfaces;
using Code.Planet.Behavior.Enemy;
using Code.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Planet
{
    public class PlanetEnemy : PlanetBase, IDamage
    {
        [SerializeField] private ObjectPool _planetPool;
        [SerializeField] private ObjectPool _poolEffect;

        [SerializeField] private float _speedMove;
        [SerializeField] private int _maxDebris = 2;


        private void Awake()
        {
            SetMove(new MoveEnemyPlanetBehavior(transform, _planetPool, _speedMove));
        }

        private void Update()
        {
            Move.Move();
        }

        private void DoDebris()
        {
            for (int i = 0; i < _maxDebris; i++)
            {
                var planet = _planetPool.GetFreeObject();
                planet.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                planet.gameObject.SetActive(true);
                planet.SetParent(null);
                _planetPool.GetFreeObject().position = transform.position + new Vector3(Random.Range(1, 2), 0);
            }

            _planetPool.ReturnToPool(gameObject);
        }

        public void TakeDamage()
        {
            var explosion = _poolEffect.GetFreeObject().GetComponent<ParticleSystem>();
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();

            if (gameObject.transform.localScale.x > 0.09f)
            {
                var rnd = Random.Range(0, _maxDebris);
                if (rnd != 0)
                {
                    DoDebris();
                    return;
                }
            }

            _planetPool.ReturnToPool(gameObject);
        }

        private void OnDisable()
        {
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}