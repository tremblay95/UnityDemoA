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
        [SerializeField] private string _abilityName = "New Ability";
        [SerializeField] private float _cooldownTime = 0f;

        [SerializeReference, SubclassSelector(typeof(TargetingStrategy))]
        private TargetingStrategy _targetingStrategy;

        [SerializeReference, SubclassSelector(typeof(ICost))]
        private ICost _castingCost;

        [SerializeReference, SubclassSelector(typeof(IGameplayEffect))] 
        private List<IGameplayEffect> _gameplayEffects;
        
        [SerializeReference, SubclassSelector(typeof(IAbilityExecutionStrategy))]
        private IAbilityExecutionStrategy _executionStrategy;

        public IEnumerator Cast(TargetingManager targetingManager, Action OnFinish = null)
        {
            if (_castingCost.CanAfford())
            {
                _targetingStrategy.BeginTargeting(targetingManager);
            }
            
            yield return new WaitUntil(() => targetingManager.Completed || targetingManager.Cancelled);

            if (targetingManager.Cancelled)
            {
                OnFinish?.Invoke();
                yield break;
            }

            if (_castingCost.PayCost())
            {
                _executionStrategy.Execute(_gameplayEffects, targetingManager.transform, targetingManager.Targets);
            }
            
            OnFinish?.Invoke();
        }
    }
}
