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
        
        // Todo: refactor ability def and combo def to have a common base class
        [SerializeField] private AbilityDefinition abilityDefinition;
        [SerializeField] private ComboDefinition comboDefinition;
        private AbilityContext _abilityContext;

        private void OnEnable() => input.EnableInputActions();
        private void OnDisable() => input.DisableInputActions();

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame && (!_abilityContext?.IsCasting ?? true))
            {
                // Todo: decide on a way to get the correct context based on the type of ability definition
                // _abilityContext ??= new SingleAbilityContext(abilityDefinition);
                _abilityContext ??= new ComboAbilityContext(comboDefinition);

                _abilityContext.CastAbility(targetingManager);
            }
        }
    }
}