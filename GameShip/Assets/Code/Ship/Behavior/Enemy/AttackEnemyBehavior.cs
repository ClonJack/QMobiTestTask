using System.Collections;
using Code.Interfaces;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Data.Ship;
using UnityEngine;

namespace Code.Ship.Behavior.Enemy
{
    public class AttackEnemyBehavior : IAttack
    {
        private readonly ShipOptionShot _shipOptionShot;
        private readonly ParticleSystem _particleGun;
        private readonly ObjectPool _lazerPool;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly IDamage _damage;

        public AttackEnemyBehavior(ShipOptionShot shipOptionShot,
            ParticleSystem particleGun, ObjectPool lazerPool
            , MonoBehaviour monoBehaviour, IDamage damage)
        {
            _shipOptionShot = shipOptionShot;
            _particleGun = particleGun;
            _lazerPool = lazerPool;
            _monoBehaviour = monoBehaviour;
            _damage = damage;
        }

        public void Attack()
        {
            _particleGun.Play();

            var lazer = _lazerPool.GetFreeObject();
            lazer.gameObject.SetActive(true);
            lazer.SetParent(_particleGun.transform);

            lazer.localRotation = Quaternion.identity;
            lazer.localPosition = Vector3.zero;

            lazer.SetParent(null);

            _monoBehaviour.StartCoroutine(Shot(lazer, -lazer.transform.up * 5));
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
                    if (hit.transform.TryGetComponent(out IDamage damage))
                    {
                        damage.TakeDamage();
                        _lazerPool.ReturnToPool(lazer.gameObject);
                        yield break;
                    }
                }

                lazer.Translate(target * (_shipOptionShot.SpeedMoveLazer * Time.deltaTime), Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _shipOptionShot.TimeAliveLazer)
                {
                    _lazerPool.ReturnToPool(lazer.gameObject);
                    yield break;
                }

                yield return null;
            }
        }
    }
}