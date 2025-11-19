using System;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using Utilities.SubclassSelectorAttribute;

namespace UnityDemoA
{
    [RequireComponent(typeof(Rigidbody))]
    public class EffectHandlerManager : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private new Rigidbody rigidbody;
        private readonly Dictionary<Type, IEffectHandler> _effectHandlerMap = new();

        [SerializeReference, SubclassSelector(typeof(IEffectHandler))]
        private List<IEffectHandler> _effectHandlerList = new();

        private void Awake() => RegisterEffectHandlers(_effectHandlerList);
        private void RegisterEffectHandlers(IEnumerable<IEffectHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                if (_effectHandlerMap.TryAdd(handler.EffectType, handler)) { handler.InitializeReferences(gameObject); }
                else { Debug.LogWarning($"{name} already has a handler for {handler.EffectType.Name}. Ignoring..."); }
            }

            _effectHandlerList = null;
        }

        public void ApplyEffects(List<IGameplayEffect> effects, Transform source) => effects.ForEach(effect => ApplyEffect(effect, source));

        private void ApplyEffect(IGameplayEffect effect, Transform source)
        {
            if (_effectHandlerMap.TryGetValue(effect.GetType(), out var handler)) { handler.HandleEffect(effect, source); }
        }
    }
}