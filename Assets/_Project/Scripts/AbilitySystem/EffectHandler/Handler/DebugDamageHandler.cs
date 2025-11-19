using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class DebugDamageHandler : EffectHandler<DamageEffect>
    {
        private string _name;

        public override void InitializeReferences(GameObject go) => _name =  go.name;
        protected override void HandleEffect(DamageEffect effect, Transform source) =>
            Debug.Log($"{_name}: Damage effect of {effect.Damage} damage from {source.name} handled.");
    }
}