using UnityEngine;

namespace UnityDemoA
{
    public abstract class AbilityDefinition : ScriptableObject
    {
        public abstract AbilityContext GetContext();
    }
}