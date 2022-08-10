using System.Collections;
using System.Collections.Generic;
using Code.Pool;
using Code.Ship.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Ship.Behavior.Player
{
    public class AttackPlayerBehavior : IAttack
    {
        private readonly List<ParticleSystem> _particleGun;
        private readonly ObjectPool _objectPool;
        private readonly float _speedMoveLazer;
        private readonly MonoBehaviour _monoBehaviour;


        private WaitForSeconds _waitForSeconds;

        private Transform _gun;

        private const int _distanceMove = 10;
        private const float _timeMove = 1f;

        public AttackPlayerBehavior(List<ParticleSystem> particleGun, ObjectPool objectPool, float speedMoveLazer,
            MonoBehaviour monoBehaviour)
        {
            _particleGun = particleGun;
            _objectPool = objectPool;
            _speedMoveLazer = speedMoveLazer;
            _monoBehaviour = monoBehaviour;
        }

        public void Attack()
        {
            var randomShotGun = Random.Range(0, _particleGun.Count - 1);

            var gun = _particleGun[randomShotGun];
            gun.Play();

            _gun = gun.transform;

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