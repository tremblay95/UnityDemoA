using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    public interface IAbilityExecutionStrategy
    {
        void Execute(List<IGameplayEffect> effects, Transform source, IReadOnlyList<Transform> targets);
    }
}