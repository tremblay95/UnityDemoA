using UnityEngine;

namespace UnityDemoA
{
    public class AttackState : State
    {
        public AttackState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            player.StopMovement();
            animator.CrossFade(AttackHash, 0f);
            player.Attack();
        }
    }
}