using System;
using UnityEngine;

namespace UnityDemoA
{
    public abstract class EffectHandler<T> : IEffectHandler where T : IGameplayEffect
    {
        public Type EffectType => typeof(T);

        public void HandleEffect(IGameplayEffect effect, Transform source)
        {
            HandleEffect((T) effect, source);
        }

        protected abstract void HandleEffect(T effect, Transform source);
    }

    public interface IEffectHandler
    {
        Type EffectType { get; }
        void HandleEffect(IGameplayEffect effect, Transform source);
    }
}