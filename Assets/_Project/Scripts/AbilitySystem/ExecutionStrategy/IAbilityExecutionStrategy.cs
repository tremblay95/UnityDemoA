using UnityEngine;

namespace UnityDemoA
{
    public interface IAbilityExecutionStrategy
    {
        void Execute(AbilityEffect effect, Transform source, Transform target);
    }
}