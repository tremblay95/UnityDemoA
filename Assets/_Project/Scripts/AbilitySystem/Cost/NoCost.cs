using System;

namespace UnityDemoA
{
    [Serializable]
    public class NoCost : ICost
    {
        public bool CanAfford() => true;
        public bool PayCost() => true;
    }
}