using System.Collections.Generic;
using ScratchPad.SubclassSelectorAttribute;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
    public class Ability : ScriptableObject
    {
        [Header("Settings")] [SerializeField] private string abilityName = "New Ability";
        [SerializeField] private float cooldownTime = 0f;


        [SerializeReference, SubclassSelector(typeof(TargetingStrategy))]
        private TargetingStrategy targetingStrategy;

        [SerializeReference, SubclassSelector(typeof(ICost))]
        private ICost castingCost;

        [SerializeField] private List<GameplayEffect> gameplayEffects;


        [SerializeReference, SubclassSelector(typeof(IAbilityExecutionStrategy))]
        private IAbilityExecutionStrategy executionStrategy;
    }
}
