using System;
using UnityEngine;

namespace UnityDemoA
{
    // Todo: refactor effect handlers to be serializable non-monobehaviours that can be assigned via a List on EffectHandlerManager
    public abstract class EffectHandler<T> : MonoBehaviour, IEffectHandler where T : IGameplayEffect
    {
        public abstract Type EffectType { get; }

        public void HandleEffect(IGameplayEffect effect, Transform source)
        {
            HandleEffect((T) effect, source);
        }
        public abstract void HandleEffect(T effect, Transform source);
    }

    public interface IEffectHandler
    {
        Type EffectType { get; }
        void HandleEffect(IGameplayEffect effect, Transform source);
    }
}