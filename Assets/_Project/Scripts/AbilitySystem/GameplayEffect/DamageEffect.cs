using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class DamageEffect : IGameplayEffect
    {
        [SerializeField] private int _damage = 10;
        
        public int Damage => _damage;
    }
}