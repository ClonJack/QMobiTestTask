using System.Collections;
using System.Collections.Generic;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Player
{
    public class AttackPlayerBehavior : IAttack
    {
        private readonly List<ParticleSystem> _particleGun;
        private readonly ObjectPool _objectPool;
        private readonly float _speedMoveLazer;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly LayerMask _layerAttack;
        private readonly int _distanceMove;
        private readonly float _timeMove;
        private readonly float _distanceDetectedShot;


        public AttackPlayerBehavior(List<ParticleSystem> particleGun, ObjectPool objectPool, float speedMoveLazer,
            MonoBehaviour monoBehaviour, LayerMask layerAttack,int distanceMove,float timeMove,float distanceDetectedShot)
        {
            _particleGun = particleGun;
            _objectPool = objectPool;
            _speedMoveLazer = speedMoveLazer;
            _monoBehaviour = monoBehaviour;
            _layerAttack = layerAttack;
            _distanceMove = distanceMove;
            _timeMove = timeMove;
            _distanceDetectedShot = distanceDetectedShot;
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

            _monoBehaviour.StartCoroutine(Shot(lazer, gun.transform.up * _distanceMove));
        }


        private IEnumerator Shot(Transform lazer, Vector3 target)
        {
            var timeElapsed = 0f;
            while (true)
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(lazer.transform.position, lazer.transform.up,
                        _distanceDetectedShot, _layerAttack);

                if (hit.collider)
                {
                    if (hit.transform.TryGetComponent(out BaseShip baseShip))
                    {
                        baseShip.TakeDamage();
                        _objectPool.ReturnToPool(lazer.gameObject);
                        yield break;
                    }
                }

                lazer.Translate(target * (_speedMoveLazer * Time.deltaTime), Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _timeMove)
                {
                    _objectPool.ReturnToPool(lazer.gameObject);
                    yield break;
                }

                yield return null;
            }
        }
    }
}