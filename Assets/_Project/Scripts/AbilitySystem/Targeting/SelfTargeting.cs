using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class SelfTargeting : TargetingStrategy
    {
        protected override void Start() => _targetingManager.CompleteTargeting(new List<Transform> { _targetingManager.transform });
    }
}