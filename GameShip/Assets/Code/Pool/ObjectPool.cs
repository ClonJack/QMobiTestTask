using System.Collections.Generic;
using UnityEngine;

namespace Code.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _objectOnSpawn;

        [SerializeField] private List<GameObject> _objectsInPool;

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