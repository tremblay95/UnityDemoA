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
            // This will be refactored later when I move this cast logic into the AbilityContext
            TargetData targetData = null;
            targetingManager.TargetsConfirmed += t => targetData = t;

            Debug.Log($"[Ability] Request: {abilityDefinition.abilityName} (Caster: {targetingManager.name})");
            if (!abilityDefinition.castingCost.CanAfford())
            {
                Debug.Log($"[Ability] Cannot afford cost: {abilityDefinition.abilityName}");
                cancelledCallback?.Invoke();
                yield break;
            }

            Debug.Log($"[Ability] Begin Targeting: {abilityDefinition.abilityName}");
            if (!targetingManager.BeginTargeting(abilityDefinition.targetingStrategy))
            {
                Debug.Log($"[Ability] Targeting failed: {abilityDefinition.abilityName}");
                cancelledCallback?.Invoke();
                yield break;
            }

            yield return new WaitUntil(() => !targetingManager.IsTargeting);

            Debug.Log(
                targetData is { targets: {Count: > 0} }
                    ? $"[Ability] Targeting confirmed: {abilityDefinition.abilityName}"
                    : $"[Ability] Targeting cancelled: {abilityDefinition.abilityName}"
            );

            if (targetData is not { targets: {Count: > 0} })
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

            Debug.Log($"[Ability] Executing {abilityDefinition.abilityName} Targets: {targetData.targets.Count}");
            abilityDefinition.executionStrategy.Execute(abilityDefinition.gameplayEffects, targetingManager.transform, targetData);

            completedCallback?.Invoke();
        }

        public override AbilityContext GetContext()
        {
            return new SingleAbilityContext(this);
        }
    }
}
