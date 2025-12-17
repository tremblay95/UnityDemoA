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
            Debug.Log($"[Ability] Request: {abilityDefinition.abilityName} (Caster: {targetingManager.name})");
            if (!abilityDefinition.castingCost.CanAfford())
            {
                Debug.Log($"[Ability] Cannot afford cost: {abilityDefinition.abilityName}");
                cancelledCallback?.Invoke();
                yield break;
            }
            
            Debug.Log($"[Ability] Begin Targeting: {abilityDefinition.abilityName}");
            abilityDefinition.targetingStrategy.BeginTargeting(targetingManager);

            yield return new WaitUntil(() => targetingManager.Completed || targetingManager.Cancelled);

            Debug.Log(
                targetingManager.Completed
                    ? $"[Ability] Targeting confirmed: {abilityDefinition.abilityName}"
                    : $"[Ability] Targeting cancelled: {abilityDefinition.abilityName}"
            );

            if (targetingManager.Cancelled)
            {
                cancelledCallback?.Invoke();
                yield break;
            }

            if (!abilityDefinition.castingCost.PayCost())
            {
                Debug.Log($"[Ability] Cannot afford cost: {abilityDefinition.abilityName}");
                cancelledCallback?.Invoke();
                yield break;
            }

            Debug.Log($"[Ability] Executing {abilityDefinition.abilityName} Targets: {targetingManager.Targets.Count}");
            abilityDefinition.executionStrategy.Execute(abilityDefinition.gameplayEffects, targetingManager.transform, targetingManager.Targets);

            completedCallback?.Invoke();
        }

        public override AbilityContext GetContext()
        {
            return new SingleAbilityContext(this);
        }
    }
}
