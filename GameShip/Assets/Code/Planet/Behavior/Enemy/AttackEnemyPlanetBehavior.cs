using UnityEngine;

namespace ShipGame
{
    public class AttackEnemyPlanetBehavior : IAttack
    {
        private readonly Transform _owner;
        private readonly LayerMask _layerAttack;
        private readonly float _radius;
        private readonly float _damageValue;
        private readonly IDamage _damageOwner;


        private Collider2D[] _colliders = new Collider2D[1];

        public AttackEnemyPlanetBehavior(Transform owner, LayerMask layerAttack, float radius, float damageValue,
            IDamage damageOwner)
        {
            _owner = owner;
            _layerAttack = layerAttack;
            _radius = radius;
            _damageValue = damageValue;
            _damageOwner = damageOwner;
        }

        public void Attack()
        {
            var attackIndex = Physics2D.OverlapCircleNonAlloc(_owner.position, _radius, _colliders, _layerAttack);

            if (attackIndex != 0)
            {
                if (_colliders[attackIndex - 1].TryGetComponent(out IDamage attack))
                {
                    attack.TakeDamage(_damageValue);
                    _damageOwner.TakeDamage();
                }
            }
        }
    }
}