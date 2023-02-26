using UnityEngine;
using Random = UnityEngine.Random;

namespace ShipGame
{
    public class PlanetEnemy : PlanetBase, IDamage
    {
        [SerializeField] private ObjectPool _planetPool;
        [SerializeField] private ObjectPool _poolEffect;

        [SerializeField] private float _speedMove;
        [SerializeField] private int _maxDebris = 2;
        [SerializeField] private float _radiusAttack;

        [SerializeField] private float _damage;

        [SerializeField] private LayerMask _LayerAttack;


        private void Awake()
        {
            SetMove(new MoveEnemyPlanetBehavior(transform, _planetPool, _speedMove));
            SetAttack(new AttackEnemyPlanetBehavior(transform, _LayerAttack, _radiusAttack,
                _damage, this));
        }

        private void Update()
        {
            Move.Move();
            Attack.Attack();
        }

        private void DoDebris(int debris)
        {
            for (var i = 0; i < _maxDebris; i++)
            {
                var planet = _planetPool.GetFreeObject();
                planet.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                planet.gameObject.SetActive(true);
                planet.SetParent(null);
                _planetPool.GetFreeObject().position = transform.position + new Vector3(Random.Range(1, 2), 0);
            }

            _planetPool.ReturnToPool(gameObject);
        }

        public void TakeDamage(float damage)
        {
            var explosion = _poolEffect.GetFreeObject().GetComponent<ParticleSystem>();
            if (explosion == null) return;
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();

            if (gameObject.transform.localScale.x > 0.09f)
            {
                var rnd = Random.Range(0, _maxDebris);
                DoDebris(rnd);
                return;
            }

            _planetPool.ReturnToPool(gameObject);
        }

        private void OnDisable()
        {
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}