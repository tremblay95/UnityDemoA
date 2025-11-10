using UnityEngine;

namespace UnityDemoA
{
    public class SuspendState : AbilityExecutionState
    {
        private readonly int? animationHash = null;
        public SuspendState(PlayerController player, Animator animator, int? animationHash = null) : base(player, animator)
        {
            this.animationHash = animationHash;
        }
        
        public override void OnEnter()
        {
            if (animationHash.HasValue)
            {
                animator.CrossFade(animationHash.Value, crossFadeDuration);
            }
            
            // todo: player.DisablePhysics() (rigidbody, collider, etc)
        }
        
        public override bool IsFinished() => true;
    }
}