using UnityEngine;

namespace UnityDemoA
{
    public class DebugDamageHandler : EffectHandler<DamageEffect>
    {
        private readonly string _name;
        public DebugDamageHandler(string name) => _name = name;

        protected override void HandleEffect(DamageEffect effect, Transform source)
        {
            Debug.Log($"{_name}: Damage effect of {effect.Damage} damage from {source.name} handled.");
        }
    }
}