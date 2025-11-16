using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.SubclassSelectorAttribute;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
    public class AbilityDefinition : ScriptableObject
    {
        public string abilityName = "New Ability";
        public float cooldownTime = 0f;

        [SerializeReference, SubclassSelector(typeof(TargetingStrategy))]
        public TargetingStrategy targetingStrategy;

        [SerializeReference, SubclassSelector(typeof(ICost))]
        public ICost castingCost;

        [SerializeReference, SubclassSelector(typeof(IGameplayEffect))]
        public List<IGameplayEffect> gameplayEffects;

        [SerializeReference, SubclassSelector(typeof(IAbilityExecutionStrategy))]
        public IAbilityExecutionStrategy executionStrategy;

        public static IEnumerator Cast(AbilityDefinition abilityDefinition, TargetingManager targetingManager, Action completedCallback = null, Action cancelledCallback = null)
        {
            if (!abilityDefinition.castingCost.CanAfford())
            {
                if (cancelledCallback != null) { cancelledCallback(); }
                yield break;
            }
            
            abilityDefinition.targetingStrategy.BeginTargeting(targetingManager);

            yield return new WaitUntil(() => targetingManager.Completed || targetingManager.Cancelled);

            if (targetingManager.Cancelled)
            {
                if (cancelledCallback != null) { cancelledCallback(); }
                yield break;
            }

            if (!abilityDefinition.castingCost.PayCost())
            {
                if (cancelledCallback != null) { cancelledCallback(); }
                yield break;
            }
            
            abilityDefinition.executionStrategy.Execute(abilityDefinition.gameplayEffects, targetingManager.transform, targetingManager.Targets);

            if (completedCallback != null) { completedCallback(); }
        }
    }
}
