using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityEffect : ScriptableObject
    {
        // Todo: Handle longer effects
        public abstract void Apply(Transform target);
    }
}