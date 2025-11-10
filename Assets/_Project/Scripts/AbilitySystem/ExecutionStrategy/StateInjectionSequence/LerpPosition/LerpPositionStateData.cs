using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public class LerpPositionStateData : AbilityStateData
    {
        public LerpPositionRelativeTo initialPositionRelativeTo;
        public Vector3 initialPosition;
        public LerpPositionRelativeTo targetPositionRelativeTo;
        public Vector3 targetPosition;
        public float duration;
        
        public LerpPositionStateData() 
        {
            name = "LerpPosition";
        }
    }
}