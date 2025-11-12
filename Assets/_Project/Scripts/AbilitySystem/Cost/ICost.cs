using UnityEngine;

namespace UnityDemoA
{
    // Consider moving this out of the AbilitySystem namespace
    public interface ICost
    {
        bool CanAfford(Transform source);
        bool PayCost(Transform source);
    }
}