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
            foreach (var effect in effects)
            {
                foreach (var target in targets)
                {
                    effect.Apply(source, target);
                }
            }
        }
    }
}