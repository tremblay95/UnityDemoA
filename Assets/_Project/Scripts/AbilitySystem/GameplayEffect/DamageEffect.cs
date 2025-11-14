using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class DamageEffect : IGameplayEffect
    {
        [SerializeField] private int _damage = 10;
        
        public void Apply(Transform source, Transform target)
        {
            var handler = target.GetComponent<IEffectHandler>();
            if (handler == null) { return; }
            
            Debug.Log($"{_damage} damage applied to {target.name}");
            handler.TakeDamage(_damage);
        }
    }
}