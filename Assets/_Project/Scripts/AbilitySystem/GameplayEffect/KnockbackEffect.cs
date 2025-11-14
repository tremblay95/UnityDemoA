using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class KnockbackEffect : IGameplayEffect
    {
        [SerializeField] private float _knockbackForce = 10f;
        [SerializeField] private float _knockbackLift = 1f;
        [SerializeField] private ForceMode _forceMode = ForceMode.VelocityChange;
        
        public void Apply(Transform source, Transform target)
        {
            var rb = target.GetComponent<Rigidbody>();
            if (rb == null) { return; }
            
            Vector3 knockbackDirection = target == source ? -target.forward : target.position - source.position;
            
            Debug.Log($"{_knockbackForce} knockback applied to {target.name}");
            rb.AddForce(knockbackDirection * _knockbackForce + Vector3.up * _knockbackLift, _forceMode);
        }
    }
}