using UnityEngine;

namespace UnityDemoA
{
    public abstract class GameplayEffect : ScriptableObject
    {
        public abstract void Apply(Transform target);
    }
}