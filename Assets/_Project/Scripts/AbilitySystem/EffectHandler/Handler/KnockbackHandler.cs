using System;
using UnityEngine;
using UnityUtils;

namespace UnityDemoA
{
    [Serializable]
    public class KnockbackHandler : EffectHandler<KnockbackEffect>
    {
        private Rigidbody _rigidbody;
        private Transform _transform;

        public override void InitializeReferences(GameObject go)
        {
            _transform = go.transform;
            _rigidbody = go.GetComponent<Rigidbody>();
        }

        protected override void HandleEffect(KnockbackEffect effect, Transform source)
        {
            var knockbackDirection = source == _transform
                ? -_transform.forward
                : (_transform.position - source.position).With(y:0).normalized;
            
            var rotation = Quaternion.LookRotation(knockbackDirection);
            
            _rigidbody.AddForce(rotation * effect.Knockback,  effect.ForceMode);
        }
    }
}