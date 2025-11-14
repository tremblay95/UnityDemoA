using System;
using System.Collections;
using System.Collections.Generic;
using Utilities.SubclassSelectorAttribute;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
    public class Ability : ScriptableObject
    {
        [Header("Settings")] 
        [SerializeField] private string abilityName = "New Ability";
        [SerializeField] private float cooldownTime = 0f;

        [SerializeReference, SubclassSelector(typeof(TargetingStrategy))]
        private TargetingStrategy targetingStrategy;

        [SerializeReference, SubclassSelector(typeof(ICost))]
        private ICost castingCost;

        [SerializeReference, SubclassSelector(typeof(IGameplayEffect))] 
        private List<IGameplayEffect> gameplayEffects;
        
        [SerializeReference, SubclassSelector(typeof(IAbilityExecutionStrategy))]
        private IAbilityExecutionStrategy executionStrategy;

        public IEnumerator Cast(TargetingManager targetingManager, Action OnFinish = null)
        {
            if (castingCost.CanAfford())
            {
                targetingStrategy.BeginTargeting(targetingManager);
            }
            
            yield return new WaitUntil(() => targetingManager.Completed || targetingManager.Cancelled);

            if (targetingManager.Cancelled)
            {
                OnFinish?.Invoke();
                yield break;
            }

            if (castingCost.PayCost())
            {
                executionStrategy.Execute(gameplayEffects, targetingManager.transform, targetingManager.Targets);
            }
            
            OnFinish?.Invoke();
        }
    }
}
