using System.Threading.Tasks;
using UnityEngine;

namespace ShipGame
{
    public class AttackPlayerBehavior : IAttack
    {
        private readonly SpaceshipPlayer _player;

        public AttackPlayerBehavior(SpaceshipPlayer player)
        {
            _player = player;
        }

        public void Attack()
        {
            var randomShotGun = Random.Range(0, _player.ParticleGun.Count);

            var gun = _player.ParticleGun[randomShotGun];
            gun.Play();

            var lazer = _player.PoolService.PoolLazer.GetFreeObject();
            lazer.SetParent(gun.transform);
            lazer.gameObject.SetActive(true);
            lazer.localPosition = Vector3.zero;
            lazer.SetParent(null);

            var ship = _player.transform;
            lazer.rotation = ship.rotation;

            Shot(lazer, gun.transform.up * _player.ShipOptionShot.DistanceMoveLazer);
        }

        private async void Shot(Transform lazer, Vector3 target)
        {
            var timeElapsed = 0f;
            while (!_player.CancellationToken.IsCancellationRequested)
            {
                var hit =
                    Physics2D.Raycast(lazer.transform.position, lazer.transform.up,
                        _player.ShipOptionShot.DistanceDetectedShot, _player.ShipOptionShot.LayerAttack);

                if (hit.transform != null && hit.transform.TryGetComponent(out IDamage damage))
                {
                    damage.TakeDamage();
                    _player.PoolService.PoolLazer.ReturnToPool(lazer.gameObject);
                    break;
                }

                lazer.Translate(target * (_player.ShipOptionShot.SpeedMoveLazer * Time.deltaTime), Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _player.ShipOptionShot.TimeAliveLazer)
                {
                    _player.PoolService.PoolLazer.ReturnToPool(lazer.gameObject);
                    break;
                }

                await Task.Yield();
            }
        }
    }
}