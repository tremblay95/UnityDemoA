using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class NoCost : ICost
    {
        public string costName = "No Cost";
        
        public bool CanAfford(Transform source) => true;
        public bool PayCost(Transform source) => true;
    }
}