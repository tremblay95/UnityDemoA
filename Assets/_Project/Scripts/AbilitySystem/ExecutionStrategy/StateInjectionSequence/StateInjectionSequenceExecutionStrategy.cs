using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(menuName = "Abilities/Execution Strategies/State Injection Sequence", fileName = "StateInjectionSequence")]
    public class StateInjectionSequenceExecutionStrategy : AbilityExecutionStrategy
    {
        [SerializeReference] List<AbilityStateData> stateSequence;

        public override void Execute(AbilityEffect effect, Transform source, Transform target)
        {
            // Call into AbilityStateFactory to create a list of states
            var states = AbilityStateFactory.CreateStateSequence(stateSequence, source, target, effect);
            
            // Link states together in sequence
            for (int i = 1; i < states.Count; i++)
            {
                states[i - 1].SetNextState(states[i]);
            }
            
            // Inject states into the target state machine
            // Todo: come back to this after refactoring the state machine, states, and player controller
            
            // Request the target state machine to start executing the first injected state
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
