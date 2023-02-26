using System.Threading;
using UnityEngine;

namespace ShipGame
{
    public class SpaceshipEnemy : BaseShip, IDamage
    {
        [SerializeField] private ShipOptionShot _shipOptionShot;
        [SerializeField] private float _timeOnShot;

        [Header("Option Move Ship")]
        [SerializeField] private float _smothMove;
        [SerializeField] private Vector3Int _randMove;

        [Header("Pool")] [SerializeField] private PoolService _poolService;

        [Header("Target Attack")] [SerializeField]
        private Transform _target;

        [Header("Effect")] [SerializeField] private ParticleSystem _particleGun;

        private CancellationTokenSource _tokenSource;

        private float _timeElapsedShot;
        public CancellationTokenSource TokenSource => _tokenSource;
        public ShipOptionShot ShipOptionShot => _shipOptionShot;
        public PoolService PoolService => _poolService;
        public ParticleSystem ParticleGun => _particleGun;
        public float SmothMove => _smothMove;
        public float TimeOnShot => _timeOnShot;
        public Transform Target => _target;
        public Vector3Int RandMove => _randMove;

        public override void Init()
        {
            _tokenSource = new CancellationTokenSource();

            SetMove(new MoveEnemyBehavior(this));
            SetAttack(new AttackEnemyBehavior(this));
        }

        public void TakeDamage(float damage)
        {
            var explosion = _poolService.PoolExplosion.GetFreeObject().GetComponent<ParticleSystem>();
            explosion.gameObject.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.Play();

            _poolService.PoolEnemy.ReturnToPool(gameObject);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetServicePool(PoolService poolService)
        {
            _poolService = poolService;
        }

        private void Update()
        {
            Move.Move();

            _timeElapsedShot += Time.deltaTime;
            if (_timeElapsedShot > _timeOnShot)
            {
                _timeElapsedShot = 0;
                Attack.Attack();
            }
        }

        private void OnDestroy()
        {
            _tokenSource.Cancel();
        }
    }
}