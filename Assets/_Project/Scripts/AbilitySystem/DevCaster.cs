using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDemoA
{
    [RequireComponent(typeof(TargetingManager))]
    public class DevCaster : ValidatedMonoBehaviour, IEffectHandler
    {
        [SerializeField, Self] private TargetingManager targetingManager;
        [SerializeField] private Ability ability;
        [SerializeField] private InputReader input;

        public Coroutine castCoroutine = null;

        private void OnEnable() => input.EnableInputActions();
        private void OnDisable() => input.DisableInputActions();

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame && castCoroutine == null)
            {
                castCoroutine = StartCoroutine(ability.Cast(targetingManager, () => castCoroutine = null));
            }
        }

        public void TakeDamage(int amount)
        {
            Debug.Log($"{gameObject.name} took {amount} damage!");
        }
    }
    
    public interface IEffectHandler
    {
        void TakeDamage(int amount);
    }
}