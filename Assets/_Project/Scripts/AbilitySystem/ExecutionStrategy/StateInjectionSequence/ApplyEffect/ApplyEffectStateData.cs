using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class ApplyEffectStateData : AbilityStateData
    {
        [Tooltip("Effect to apply in this step. Select 'None' apply the ability's effect, otherwise this effect will be applied.")]
        public AbilityEffect effect;
        public ApplyEffectStateData()
        {
            name = "ApplyEffect";
        }
    }
}