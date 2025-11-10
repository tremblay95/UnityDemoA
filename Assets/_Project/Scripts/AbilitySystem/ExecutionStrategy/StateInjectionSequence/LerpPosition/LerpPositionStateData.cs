using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class LerpPositionStateData : AbilityStateData
    {
        public Vector3 startPosition;
        public Vector3 targetPosition;
        public float duration;
        
        public LerpPositionStateData() 
        {
            name = "LerpPosition";
        }
    }
}