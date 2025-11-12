using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class NoExecutionStrategy : IAbilityExecutionStrategy
    {
        public string executionStrategyName = "No Execution Strategy";
        
        public void Execute(AbilityEffect effect, Transform source, Transform target)
        {
            // noop
        }   
    }
}