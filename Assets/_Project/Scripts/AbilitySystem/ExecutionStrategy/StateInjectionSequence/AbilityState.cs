using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityState : State
    {
        private AbilityState nextState;
        // Todo: Builder?
        
        protected AbilityState(PlayerController player, Animator animator) : base(player, animator) { }

        public abstract bool IsFinished();

        public void SetNextState(AbilityState state)
        {
            nextState = state;
        }
    }
}