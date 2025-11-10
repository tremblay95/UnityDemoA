using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(menuName = "Abilities/Execution Strategies/Transition Sequence", fileName = "TransitionSequenceStrategy")]
    public class TransitionSequenceStrategy : AbilityExecutionStrategy
    {
        [SerializeReference] List<TransitionStep> transitionSteps;
        
        public override void Execute(IAbilityEffect effect, Transform source, Transform target) { }

        
#if UNITY_EDITOR
        public void AddStep(object step)
        {
            transitionSteps ??= new List<TransitionStep>();
            transitionSteps.Add((TransitionStep)step);
        }
#endif
    }
}
