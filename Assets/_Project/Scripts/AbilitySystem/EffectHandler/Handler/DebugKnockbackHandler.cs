using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class DebugKnockbackHandler : EffectHandler<KnockbackEffect>
    {
        private string _name;

        public override void InitializeReferences(GameObject go) => _name =  go.name;
        protected override void HandleEffect(KnockbackEffect effect, Transform source) =>
            Debug.Log($"{_name}: Knockback effect with force {effect.Knockback} from {source.name} handled.");
    }
}