using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _objectOnSpawn;

        [SerializeField] private List<GameObject> _objectsInPool;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _objectsInPool.Add(transform.GetChild(i).gameObject);
                _objectsInPool[i].gameObject.SetActive(false);
            }
        }

        public Transform GetFreeObject()
        {
            foreach (var objTransform in _objectsInPool)
            {
                if (!objTransform.activeSelf)
                {
                    return objTransform.transform;
                }
            }

            var spawnObject = Instantiate(_objectOnSpawn, Vector3.zero, Quaternion.identity);
            _objectsInPool.Add(spawnObject);

            return spawnObject.transform;
        }

        public void ReturnToPool(GameObject objectTooPool)
        {
            objectTooPool.transform.SetParent(transform);
            objectTooPool.SetActive(false);
        }
    }
}