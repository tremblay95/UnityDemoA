using System;
using UnityEngine;

namespace UnityDemoA
{
    public class DamageEffectHandler : EffectHandler<DamageEffect>
    {
        public override Type EffectType => typeof(DamageEffect);

        public override void HandleEffect(DamageEffect effect, Transform source)
        {
            Debug.Log($"{name}: Damage effect of {effect.Damage} damage from {source.name} handled.");
        }
    }
}