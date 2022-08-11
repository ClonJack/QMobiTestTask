using Code.Pool;
using UnityEngine;

namespace Code
{
    public class StoppedParticle : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private ObjectPool _objectPool;

        private void Awake()
        {
            _objectPool = transform.parent.GetComponent<ObjectPool>();
        }

        private void Start()
        {
            var main = GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Disable;
        }

        private void OnParticleSystemStopped()
        {
            _objectPool.ReturnToPool(gameObject);
        }
    }
}