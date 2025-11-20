using System;
using System.Collections;

namespace UnityDemoA
{
    public static class AbilityDefinitionExtensions
    {
        public static IEnumerator Cast(this AbilityDefinition abilityDefinition, TargetingManager targetingManager,
            Action completedCallback = null, Action cancelledCallback = null)
        {
            return AbilityDefinition.Cast(abilityDefinition, targetingManager, completedCallback, cancelledCallback);
        }
    }
}