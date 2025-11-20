using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.SubclassSelectorAttribute;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
    public class SingleAbilityDefinition : AbilityDefinition
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

        public static IEnumerator Cast(SingleAbilityDefinition abilityDefinition, TargetingManager targetingManager, Action completedCallback = null, Action cancelledCallback = null)
        {
            if (!abilityDefinition.castingCost.CanAfford())
            {
                cancelledCallback?.Invoke();
                yield break;
            }
            
            abilityDefinition.targetingStrategy.BeginTargeting(targetingManager);

            yield return new WaitUntil(() => targetingManager.Completed || targetingManager.Cancelled);

            if (targetingManager.Cancelled || !abilityDefinition.castingCost.PayCost())
            {
                cancelledCallback?.Invoke();
                yield break;
            }

            // Todo: yield until execution strategy is complete
            abilityDefinition.executionStrategy.Execute(abilityDefinition.gameplayEffects, targetingManager.transform, targetingManager.Targets);

            completedCallback?.Invoke();
        }

        public override AbilityContext GetContext()
        {
            return new SingleAbilityContext(this);
        }
    }
}
