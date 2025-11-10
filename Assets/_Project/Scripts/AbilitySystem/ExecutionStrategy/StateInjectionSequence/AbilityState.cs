using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityState : State
    {
        private AbilityState nextState;
        // Todo: SetNextState(...)?
        // Todo: Builder?
        
        protected AbilityState(PlayerController player, Animator animator, AbilityState nextState = null) : base(player, animator) { }

        public abstract bool IsFinished();
    }
}