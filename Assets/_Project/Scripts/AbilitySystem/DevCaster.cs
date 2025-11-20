using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDemoA
{
    [RequireComponent(typeof(TargetingManager))]
    [RequireComponent(typeof(EffectHandlerManager))]
    public class DevCaster : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private TargetingManager targetingManager;
        [SerializeField] private InputReader input;
        
        [SerializeField] private AbilityDefinition abilityDefinition;
        private AbilityContext _abilityContext;

        private void OnEnable() => input.EnableInputActions();
        private void OnDisable() => input.DisableInputActions();

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame && (!_abilityContext?.IsCasting ?? true))
            {
                _abilityContext ??= abilityDefinition.GetContext();

                _abilityContext.CastAbility(targetingManager);
            }
        }
    }
}