using UnityEngine;

namespace UnityDemoA
{
    public class LocomotionState : State
    {
        public LocomotionState(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            animator.CrossFade(LocomotionHash, crossFadeDuration);
        }

        public override void OnFixedUpdate()
        {
            player.HandleMovement();
        }
    }
}