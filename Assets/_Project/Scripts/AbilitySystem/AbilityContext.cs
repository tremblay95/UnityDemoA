using ImprovedTimers;
using UnityEngine;

namespace UnityDemoA
{
    public class AbilityContext
    {
        private readonly AbilityDefinition _abilityDefinition;
        //this probably won't stay read only if we have some sort of stat that reduces cooldowns 
        private readonly CountdownTimer _cooldownTimer; 
        private Coroutine _castCoroutine;
        
        public bool IsCasting => _castCoroutine != null;

        public AbilityContext(AbilityDefinition abilityDefinition)
        {
            _abilityDefinition = abilityDefinition;
            _cooldownTimer = new CountdownTimer(abilityDefinition.cooldownTime);
            _cooldownTimer.OnTimerStart += () => Debug.Log("Timer started!");
            _cooldownTimer.OnTimerStop += ClearCoroutine;
        }

        public void CastAbility(TargetingManager targetingManager)
        {
            if (IsCasting) { return; }
            
            _castCoroutine = targetingManager.StartCoroutine(AbilityDefinition.Cast(_abilityDefinition, targetingManager, 
                _cooldownTimer.Start, ClearCoroutine));
        }

        private void ClearCoroutine() => _castCoroutine = null;
    }
}