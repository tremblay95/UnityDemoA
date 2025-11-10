using System;

namespace UnityDemoA
{
    [Serializable]
    public class DelayStateData : AbilityStateData
    {
        public float duration;
        
        public DelayStateData() 
        {
            name = "Delay";
        }
    }
}