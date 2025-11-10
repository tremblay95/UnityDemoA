using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(menuName = "Abilities/Execution Strategies/State Injection Sequence", fileName = "StateInjectionSequence")]
    public class StateInjectionSequenceExecutionStrategy : AbilityExecutionStrategy
    {
        [SerializeReference] List<AbilityStateData> stateSequence;

        public override void Execute(IAbilityEffect effect, Transform source, Transform target)
        {
            
        }

        
#if UNITY_EDITOR
        public void AddStep(object step)
        {
            stateSequence ??= new List<AbilityStateData>();
            stateSequence.Add((AbilityStateData)step);
        }
#endif
    }
}
