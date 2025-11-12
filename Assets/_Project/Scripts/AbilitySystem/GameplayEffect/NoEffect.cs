using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "No Effect", menuName = "Abilities/Effects/No Effect")]
    public class NoEffect : GameplayEffect
    {
        public override void Apply(Transform target)
        {
            // noop
            Debug.Log("No Effect applied.");
        }
    }
}