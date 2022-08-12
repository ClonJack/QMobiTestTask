using System.Collections;
using Code.Interfaces;
using Code.Pool;
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

        private Transform _lazer;

        public Transform Lazer => _lazer;

        public AttackEnemyBehavior(ShipOptionShot shipOptionShot,
            ParticleSystem particleGun, ObjectPool lazerPool
            , MonoBehaviour monoBehaviour)
        {
            _shipOptionShot = shipOptionShot;
            _particleGun = particleGun;
            _lazerPool = lazerPool;
            _monoBehaviour = monoBehaviour;
        }

        public void Attack()
        {
            _particleGun.Play();

            _lazer = _lazerPool.GetFreeObject();
            _lazer.gameObject.SetActive(true);
            _lazer.SetParent(_particleGun.transform);

            _lazer.localRotation = Quaternion.identity;
            _lazer.localPosition = Vector3.zero;

            _lazer.SetParent(null);

            _monoBehaviour.StartCoroutine(Shot(-_lazer.transform.up * 5));
        }


        private IEnumerator Shot(Vector3 target)
        {
            var timeElapsed = 0f;
            while (true)
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(_lazer.transform.position, _lazer.transform.up,
                        _shipOptionShot.DistanceDetectedShot, _shipOptionShot.LayerAttack);

                if (hit.collider)
                {
                    if (hit.transform.TryGetComponent(out IDamage damage))
                    {
                        damage.TakeDamage(_shipOptionShot.Damage);
                        _lazerPool.ReturnToPool(_lazer.gameObject);
                        _lazer = null;
                        yield break;
                    }
                }

                _lazer.Translate(target * (_shipOptionShot.SpeedMoveLazer * Time.deltaTime), Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _shipOptionShot.TimeAliveLazer)
                {
                    _lazerPool.ReturnToPool(_lazer.gameObject);
                    yield break;
                }

                yield return null;
            }
        }
    }
}