using UnityEngine;

namespace UnityDemoA
{
    public class AttackState : State
    {
        public AttackState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            animator.CrossFade(AttackHash, 0f);
            player.Attack();
        }

        public override void OnFixedUpdate()
        {
            player.HandleMovement();
        }
    }
}