using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class DamageEffect : IGameplayEffect
    {
        public int damage = 10;
        
        public void Apply(Transform source, Transform target)
        {
            var handler = target.GetComponent<IEffectHandler<DamageEffect>>();
            if (handler == null) { return; }
            
            handler.HandleEffect(this);
        }
    }
}