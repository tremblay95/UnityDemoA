using System;
using ImprovedTimers;
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
        private CountdownTimer _cooldownTimer;
        private Coroutine _abilityCoroutine;

        private void OnEnable() => input.EnableInputActions();
        private void OnDisable() => input.DisableInputActions();

        private void Awake()
        {
            _cooldownTimer = new CountdownTimer(ability.cooldownTime);
            _cooldownTimer.OnTimerStop += () => _abilityCoroutine = null;
        }

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame && _abilityCoroutine == null)
            {
                var abilityCast = Ability.Cast(ability, targetingManager,
                    () => { _cooldownTimer.Start(); }, () => { _abilityCoroutine = null; });

                _abilityCoroutine = StartCoroutine(abilityCast);
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