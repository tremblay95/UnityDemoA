using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityDemoA
{
    public class EffectHandlerManager : MonoBehaviour
    {
        private readonly Dictionary<Type, IEffectHandler> _effectHandlers = new();

        private void Awake() => RegisterEffectHandlers();

        private void RegisterEffectHandlers()
        {
            var handlers = GetComponents<MonoBehaviour>()
                .Where(mb => mb is IEffectHandler).Cast<IEffectHandler>();

            foreach (var handler in handlers)
            {
                var effectType = handler.EffectType;

                if (!_effectHandlers.TryAdd(effectType, handler))
                {
                    Debug.LogWarning($"{gameObject.name} already has a handler for {effectType.Name}. Ignoring...");
                }
            }
        }

        public void ApplyEffects(List<IGameplayEffect> effects, Transform source) => effects.ForEach(effect => ApplyEffect(effect, source));

        private void ApplyEffect(IGameplayEffect effect, Transform source)
        {
            if (_effectHandlers.TryGetValue(effect.GetType(), out var handler)) { handler.HandleEffect(effect, source); }
        }
    }
}