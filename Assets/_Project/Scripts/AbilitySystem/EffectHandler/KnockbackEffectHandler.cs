using System;
using KBCore.Refs;
using UnityEngine;
using UnityUtils;

namespace UnityDemoA
{
    [RequireComponent(typeof(Rigidbody))]
    public class KnockbackEffectHandler : EffectHandler<KnockbackEffect>
    {
        [SerializeField, Self] private Rigidbody _rigidbody;
        public override Type EffectType =>  typeof(KnockbackEffect);
        
        public override void HandleEffect(KnockbackEffect effect, Transform source)
        {
            var knockbackDirection = source == transform 
                ? -transform.forward 
                : (source.position - transform.position).With(y:0).normalized;
            
            var rotation = Quaternion.FromToRotation(Vector3.forward, knockbackDirection);
            
            _rigidbody.AddForce(rotation * effect.Knockback,  effect.ForceMode);
        }
    }
}