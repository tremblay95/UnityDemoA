using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class BasicExecution : IAbilityExecutionStrategy
    {
        public void Execute(List<IGameplayEffect> effects, Transform source, TargetData targetData)
        {
            foreach (var target in targetData.targets)
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