using UnityEngine;

namespace ShipGame
{
    public class StoppedParticle : MonoBehaviour
    {
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