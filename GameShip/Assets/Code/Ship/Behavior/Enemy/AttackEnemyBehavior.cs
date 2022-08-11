using System.Collections;
using Code.Pool;
using Code.Ship.Base;
using Code.Ship.Interfaces;
using UnityEngine;

namespace Code.Ship.Behavior.Enemy
{
    public class AttackEnemyBehavior : IAttack
    {
        private readonly ParticleSystem _particleGun;
        private readonly ObjectPool _objectPool;
        private readonly float _speedMoveLazer;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly float _timeUpdateMove;
        private readonly LayerMask _maskAttack;
        private readonly float _distanceAttack;


        public AttackEnemyBehavior(ParticleSystem particleGun, ObjectPool objectPool, float speedMoveLazer,
            MonoBehaviour monoBehaviour, float timeUpdateMove, LayerMask maskAttack, float distanceAttack)
        {
            _particleGun = particleGun;
            _objectPool = objectPool;
            _speedMoveLazer = speedMoveLazer;
            _monoBehaviour = monoBehaviour;
            _timeUpdateMove = timeUpdateMove;
            _maskAttack = maskAttack;
            _distanceAttack = distanceAttack;
        }

        public void Attack()
        {
            _particleGun.Play();

            var lazer = _objectPool.GetFreeObject();
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
                    Physics2D.Raycast(lazer.transform.position, lazer.transform.up, _distanceAttack, _maskAttack);

                if (hit.collider)
                {
                    if (hit.transform.TryGetComponent(out BaseShip baseShip))
                    {
                        Debug.Log(hit.collider.name);
                        _objectPool.ReturnToPool(lazer.gameObject);
                        yield break;
                    }
                }

                lazer.Translate(target * (_speedMoveLazer * Time.deltaTime), Space.World);
                timeElapsed += Time.deltaTime;

                if (timeElapsed > _timeUpdateMove)
                {
                    _objectPool.ReturnToPool(lazer.gameObject);
                    yield break;
                }

                yield return null;
            }
        }
    }
}