using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _objectOnSpawn;

        [SerializeField] private List<GameObject> _objectsInPool;

        public List<GameObject> ObjectsPool => _objectsInPool;

        [SerializeField] private bool _isSpawnNew;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _objectsInPool.Add(transform.GetChild(i).gameObject);
                _objectsInPool[i].gameObject.SetActive(false);
            }
        }

        public void ReturnAllToPool()
        {
            foreach (var objectPool in _objectsInPool)
            {
                objectPool.transform.SetParent(transform);
                objectPool.SetActive(false);
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

            if (_isSpawnNew)
            {
                var spawnObject = Instantiate(_objectOnSpawn, Vector3.zero, Quaternion.identity);
                _objectsInPool.Add(spawnObject);

                return spawnObject.transform;
            }

            return null;
        }

        public void ReturnToPool(GameObject objectTooPool)
        {
            objectTooPool.transform.SetParent(transform);
            objectTooPool.transform.position = Vector3.zero;
            objectTooPool.SetActive(false);
            
        }
    }
}