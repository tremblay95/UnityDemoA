namespace UnityDemoA
{
    // Consider moving this out of the AbilitySystem namespace
    public interface ICost
    {
        bool CanAfford();
        bool PayCost();
    }
}