using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    public static class AbilityStateFactory
    {
        public static List<AbilityState> CreateStateSequence(List<AbilityStateData> stateDataSequence, Transform source, Transform target, AbilityEffect effect)
        {
            // Todo: replace PlayerController with an abstraction that works for both the player and enemies
            var player = target.GetComponent<PlayerController>();
            var animator = target.GetComponent<Animator>();
            
            var states = new List<AbilityState>();
            
            foreach (var stateData in stateDataSequence)
            {
                switch (stateData)
                {
                    case DelayStateData delayStateData:
                        states.Add(new DelayState(player, animator, delayStateData));
                        break;
                    
                    case LerpPositionStateData lerpPositionStateData:
                        states.Add(new LerpPositionState(player, animator, lerpPositionStateData, source, target));
                        break;
                    
                    case ApplyEffectStateData applyEffectStateData:
                        states.Add(new ApplyEffectState(player, animator, applyEffectStateData, effect));
                        break;
                    default:
                        throw new System.NotImplementedException();
                }
            }
            
            return states;
        }
    }
}