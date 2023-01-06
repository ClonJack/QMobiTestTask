using System.Threading.Tasks;
using Code.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Enemy
{
    public class AttackEnemyBehavior : IAttack
    {
        private readonly SpaceshipEnemy _spaceshipEnemy;
        private Transform _lazer;

        public AttackEnemyBehavior(SpaceshipEnemy spaceshipEnemy)
        {
            _spaceshipEnemy = spaceshipEnemy;
        }

        public void Attack()
        {
            _spaceshipEnemy.ParticleGun.Play();

            _lazer = _spaceshipEnemy.PoolService.PoolLazer.GetFreeObject();
            _lazer.gameObject.SetActive(true);
            _lazer.SetParent(_spaceshipEnemy.ParticleGun.transform);

            _lazer.localRotation = Quaternion.identity;
            _lazer.localPosition = Vector3.zero;

            _lazer.SetParent(null);

            Shot(-_lazer.transform.up * 5);
        }


        private async void Shot(Vector3 target)
        {
            var timeElapsed = 0f;
            while (!_spaceshipEnemy.TokenSource.IsCancellationRequested)
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(_lazer.transform.position, _lazer.transform.up,
                        _spaceshipEnemy.ShipOptionShot.DistanceDetectedShot,
                        _spaceshipEnemy.ShipOptionShot.LayerAttack);

                if (hit.collider)
                {
                    if (hit.transform.TryGetComponent(out IDamage damage))
                    {
                        damage.TakeDamage(_spaceshipEnemy.ShipOptionShot.Damage);
                        _spaceshipEnemy.PoolService.PoolLazer.ReturnToPool(_lazer.gameObject);
                        _lazer = null;
                        break;
                    }
                }

                _lazer.Translate(target * (_spaceshipEnemy.ShipOptionShot.SpeedMoveLazer * Time.deltaTime),
                    Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _spaceshipEnemy.ShipOptionShot.TimeAliveLazer)
                {
                    _spaceshipEnemy.PoolService.PoolLazer.ReturnToPool(_lazer.gameObject);
                    break;
                }

                await Task.Yield();
            }
        }
    }
}