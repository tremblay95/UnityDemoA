using System;
using UnityEngine;

namespace UnityDemoA
{
    public class DamageEffectHandler : EffectHandler<DamageEffect>
    {
        private readonly Transform _transform;

        public override Type EffectType => typeof(DamageEffect);

        public DamageEffectHandler(Transform transform) => _transform = transform;

        protected override void HandleEffect(DamageEffect effect, Transform source)
        {
            Debug.Log($"{_transform.name}: Damage effect of {effect.Damage} damage from {source.name} handled.");
        }
    }
}