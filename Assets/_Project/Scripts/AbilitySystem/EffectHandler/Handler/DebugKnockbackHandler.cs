using UnityEngine;

namespace UnityDemoA
{
    public class DebugKnockbackHandler : EffectHandler<KnockbackEffect>
    {
        private readonly string _name;
        public DebugKnockbackHandler(string name)
        {
            _name = name;
        }
        protected override void HandleEffect(KnockbackEffect effect, Transform source)
        {
            Debug.Log($"{_name}: Knockback effect with force {effect.Knockback} from {source.name} handled.");
        }
    }
}