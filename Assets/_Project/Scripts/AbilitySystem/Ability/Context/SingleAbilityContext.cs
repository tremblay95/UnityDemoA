using ImprovedTimers;

namespace UnityDemoA
{
    public class SingleAbilityContext : AbilityContext
    {
        private readonly SingleAbilityDefinition _abilityDefinition;
        //this probably won't stay read only if we have some sort of stat that reduces cooldowns 
        private readonly CountdownTimer _cooldownTimer;

        public SingleAbilityContext(SingleAbilityDefinition abilityDefinition)
        {
            _abilityDefinition = abilityDefinition;
            _cooldownTimer = new CountdownTimer(abilityDefinition.cooldownTime);
            _cooldownTimer.OnTimerStop += ClearCoroutine;
        }

        public override void CastAbility(TargetingManager targetingManager)
        {
            if (IsCasting) { return; }

            PerformCast(targetingManager);
        }

        protected override void OnCastComplete() => _cooldownTimer.Start();
        protected override SingleAbilityDefinition GetAbilityDefinition() => _abilityDefinition;
    }
}