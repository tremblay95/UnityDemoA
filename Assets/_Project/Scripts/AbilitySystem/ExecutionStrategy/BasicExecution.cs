using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class BasicExecution : IAbilityExecutionStrategy
    {
        public void Execute(List<IGameplayEffect> effects, Transform source, IReadOnlyList<Transform> targets)
        {
            foreach (var target in targets)
            {
                var handlerManager = target.GetComponent<EffectHandlerManager>();
                if (handlerManager != null)
                {
                    handlerManager.ApplyEffects(effects, source);
                }
            }
        }
    }
}