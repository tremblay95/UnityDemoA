using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityContext
    {
        private Coroutine _castCoroutine;
        
        public bool IsCasting => _castCoroutine != null;
        
        public abstract void CastAbility(TargetingManager targetingManager);
        
        protected abstract AbilityDefinition GetAbilityDefinition();
        
        protected void PerformCast(TargetingManager targetingManager)
        {
            var abilityDefinition = GetAbilityDefinition();
            if (abilityDefinition == null) { return; }

            _castCoroutine = targetingManager.StartCoroutine(abilityDefinition.Cast(targetingManager,
                OnCastComplete, OnCastCancelled));
        }
        
        protected virtual void OnCastComplete() { }
        
        protected virtual void OnCastCancelled() => ClearCoroutine();
        protected void ClearCoroutine() => _castCoroutine = null;
    }
}