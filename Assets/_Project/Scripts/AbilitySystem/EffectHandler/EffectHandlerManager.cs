using System;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace UnityDemoA
{
    [RequireComponent(typeof(Rigidbody))]
    public class EffectHandlerManager : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private new Rigidbody rigidbody;
        private readonly Dictionary<Type, IEffectHandler> _effectHandlers = new();

        // Todo: List of Effect Handler Factory
        private List<IEffectHandler> _effectHandlerList;

        private void Awake()
        {
            _effectHandlerList = new(2){ new DamageEffectHandler(transform), new KnockbackEffectHandler(rigidbody) };
            RegisterEffectHandlers(_effectHandlerList);
        }

        private void RegisterEffectHandlers(IEnumerable<IEffectHandler> handlers)
        {
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