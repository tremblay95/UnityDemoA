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
            _castCoroutine = targetingManager.StartCoroutine(AbilityDefinition.Cast(GetAbilityDefinition(), targetingManager, 
                OnAbilityCastComplete, OnAbilityCastCancelled));
        }
        
        protected virtual void OnAbilityCastComplete() { }
        
        protected virtual void OnAbilityCastCancelled() => _castCoroutine = null;
    }
}