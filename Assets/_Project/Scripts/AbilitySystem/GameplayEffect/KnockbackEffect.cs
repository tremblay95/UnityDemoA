using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class KnockbackEffect : IGameplayEffect
    {
        [SerializeField, Range(0f, 50f)] private float _knockbackForce = 10f;
        [SerializeField, Range(0f, 20f)] private float _knockbackLift = 1f;
        [SerializeField] private ForceMode _forceMode = ForceMode.VelocityChange;
        
        public Vector3 Knockback => new(0f, _knockbackLift, _knockbackForce);
        public ForceMode ForceMode => _forceMode;
    }
}