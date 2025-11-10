using UnityEngine;

namespace UnityDemoA
{
    public class ApplyEffectState : AbilityState
    {
        private readonly AbilityEffect effectToApply;
        
        public ApplyEffectState(PlayerController player, Animator animator, ApplyEffectStateData data, AbilityEffect abilityEffect) : base(player, animator)
        {
            effectToApply = data.effect != null ? data.effect : abilityEffect; 
        }

        public override void OnEnter()
        {
            if (effectToApply != null) return;
            
            effectToApply.Apply(player.transform);
        }

        public override bool IsFinished() => true;
    }
}