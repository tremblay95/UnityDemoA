using UnityEngine;

namespace UnityDemoA
{
    public abstract class State : IState
    {
        protected readonly PlayerController player;
        protected readonly Animator animator;
        
        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int AttackHash = Animator.StringToHash("Attack1");
        
        protected const float crossFadeDuration = 0.1f;
        
        protected State(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }
        
        public virtual void OnEnter()
        {
            // noop
        }

        public virtual void OnExit()
        {
            // noop
        }

        public virtual void OnUpdate()
        {
            // noop
        }

        public virtual void OnFixedUpdate()
        {
            // noop
        }
    }
}