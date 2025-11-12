using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class NoTargetingStrategy : TargetingStrategy
    {
        public override Transform GetTarget(Transform source)
        {
            return null;
        }
    }
}