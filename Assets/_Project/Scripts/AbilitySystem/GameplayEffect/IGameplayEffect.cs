using UnityEngine;

namespace UnityDemoA
{
    public interface IGameplayEffect
    {
        public void Apply(Transform source, Transform target);
    }
}