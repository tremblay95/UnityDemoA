using UnityEngine;

namespace UnityDemoA
{
    public interface IAbilityEffect {}

    public abstract class AbilityExecutionStrategy : ScriptableObject
    {
        public abstract void Execute(IAbilityEffect effect, Transform source, Transform target);
    }
}
