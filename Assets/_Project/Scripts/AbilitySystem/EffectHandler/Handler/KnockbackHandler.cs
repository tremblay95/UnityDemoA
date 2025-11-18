using UnityEngine;
using UnityUtils;

namespace UnityDemoA
{
    public class KnockbackHandler : EffectHandler<KnockbackEffect>
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        
        public KnockbackHandler(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
        }

        protected override void HandleEffect(KnockbackEffect effect, Transform source)
        {
            var knockbackDirection = source == _transform
                ? -_transform.forward
                : (source.position - _transform.position).With(y:0).normalized;
            
            var rotation = Quaternion.FromToRotation(Vector3.forward, knockbackDirection);
            
            _rigidbody.AddForce(rotation * effect.Knockback,  effect.ForceMode);
        }
    }
}