using System;
using UnityEngine;

namespace UnityDemoA
{
    public abstract class EffectHandler<T> : IEffectHandler where T : IGameplayEffect
    {
        public Type EffectType => typeof(T);

        public virtual void InitializeReferences(GameObject go) { }

        public void HandleEffect(IGameplayEffect effect, Transform source) => HandleEffect((T) effect, source);

        protected abstract void HandleEffect(T effect, Transform source);
    }

    public interface IEffectHandler
    {
        public Type EffectType { get; }
        public void InitializeReferences(GameObject go);
        public void HandleEffect(IGameplayEffect effect, Transform source);
    }
}