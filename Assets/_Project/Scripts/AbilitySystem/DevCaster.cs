using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDemoA
{
    [RequireComponent(typeof(TargetingManager))]
    public class DevCaster : ValidatedMonoBehaviour, IEffectHandler
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
                if (_abilityContext == null) { _abilityContext = new AbilityContext(abilityDefinition); }

                _abilityContext.CastAbility(targetingManager);
            }
        }

        public void TakeDamage(int amount)
        {
            Debug.Log($"{gameObject.name} took {amount} damage!");
        }
    }
    
    // Todo: properly design this and probably make it an abstract class named AbilitySystemComponent or something
    public interface IEffectHandler
    {
        void TakeDamage(int amount);
    }
}