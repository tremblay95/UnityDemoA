using UnityEngine;

namespace UnityDemoA
{
    public abstract class TargetingStrategy
    {
        public string targetingStrategyName = "";
        
        public abstract Transform GetTarget(Transform source);
    }
}