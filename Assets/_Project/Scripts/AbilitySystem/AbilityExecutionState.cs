using UnityEngine;
using Utilities;

namespace UnityDemoA
{
    public abstract class AbilityExecutionState : State
    {
        protected AbilityExecutionState(PlayerController player, Animator animator) : base(player, animator) { }

        public abstract bool IsFinished();
    }
    
    public class VoidDragState : AbilityExecutionState
    {
        private CountdownTimer timer;

        public VoidDragState(PlayerController player, Animator animator, Vector3 targetPosition, float stateDuration) : base(player, animator)
        {
            
        }
        public override bool IsFinished() => timer.IsFinished; 
    }
}