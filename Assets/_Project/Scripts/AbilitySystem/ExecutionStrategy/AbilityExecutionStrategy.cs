using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityExecutionStrategy : ScriptableObject
    {
        public abstract void Execute(AbilityEffect effect, Transform source, Transform target);
    }
}
