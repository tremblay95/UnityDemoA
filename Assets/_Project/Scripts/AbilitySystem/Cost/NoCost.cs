using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class NoCost : ICost
    {
        public bool CanAfford()
        {
            Debug.Log("No cost, afforded.");
            return true;
        }

        public bool PayCost()
        {
            Debug.Log("No cost, paid.");
            return true;
        }
    }
}