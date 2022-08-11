using System.Collections;
using System.Collections.Generic;
using Code.Interfaces;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Data.Ship;
using UnityEngine;

namespace Code.Ship.Behavior.Player
{
    public class AttackPlayerBehavior : IAttack
    {
        private readonly ShipOptionShot _shipOptionShot;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly List<ParticleSystem> _particleGun;
        private readonly ObjectPool _objectPool;

        public AttackPlayerBehavior(ShipOptionShot shipOptionShot,MonoBehaviour monoBehaviour,List<ParticleSystem> particleGun,
            ObjectPool objectPool)
        {
            _shipOptionShot = shipOptionShot;
            _monoBehaviour = monoBehaviour;
            _particleGun = particleGun;
            _objectPool = objectPool;
        }

        public void Attack()
        {
            var randomShotGun = Random.Range(0, _particleGun.Count);

            var gun = _particleGun[randomShotGun];
            gun.Play();

            var lazer = _objectPool.GetFreeObject();
            lazer.SetParent(gun.transform);
            lazer.gameObject.SetActive(true);
            lazer.localPosition = Vector3.zero;
            lazer.SetParent(null);

            var ship = _monoBehaviour.transform;
            lazer.rotation = ship.rotation;

            _monoBehaviour.StartCoroutine(Shot(lazer, gun.transform.up * _shipOptionShot.DistanceMoveLazer));
        }


        private IEnumerator Shot(Transform lazer, Vector3 target)
        {
            var timeElapsed = 0f;
            while (true)
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(lazer.transform.position, lazer.transform.up,
                        _shipOptionShot.DistanceDetectedShot, _shipOptionShot.LayerAttack);

                if (hit.collider)
                {
                    if (hit.transform.TryGetComponent(out BaseShip baseShip))
                    {
                        baseShip.TakeDamage();
                        _objectPool.ReturnToPool(lazer.gameObject);
                        yield break;
                    }
                }

                lazer.Translate(target * (_shipOptionShot.SpeedMoveLazer * Time.deltaTime), Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _shipOptionShot.TimeAliveLazer)
                {
                    _objectPool.ReturnToPool(lazer.gameObject);
                    yield break;
                }

                yield return null;
            }
        }
    }
}