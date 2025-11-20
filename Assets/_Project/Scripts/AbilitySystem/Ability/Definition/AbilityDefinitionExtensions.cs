using System;
using System.Collections;

namespace UnityDemoA
{
    public static class AbilityDefinitionExtensions
    {
        public static IEnumerator Cast(this SingleAbilityDefinition abilityDefinition, TargetingManager targetingManager,
            Action completedCallback = null, Action cancelledCallback = null)
        {
            return SingleAbilityDefinition.Cast(abilityDefinition, targetingManager, completedCallback, cancelledCallback);
        }
    }
}