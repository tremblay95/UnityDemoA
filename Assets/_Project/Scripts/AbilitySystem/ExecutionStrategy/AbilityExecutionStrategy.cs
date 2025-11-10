using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityExecutionStrategy : ScriptableObject
    {
        public abstract void Execute(IAbilityEffect effect, Transform source, Transform target);
    }
}
