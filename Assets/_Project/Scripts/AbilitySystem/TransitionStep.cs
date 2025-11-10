using System;
using UnityEngine;

namespace UnityDemoA
{
    [Serializable]
    public abstract class TransitionStep
    {
        public string name;
    }
    
    [Serializable]
    public class LerpPositionStep : TransitionStep
    {
        public Vector3 startPosition;
        public Vector3 targetPosition;
        public float duration;
        
        public LerpPositionStep() 
        {
            name = "Lerp Position Step";
        }
    }
    
    [Serializable]
    public class DelayStep : TransitionStep
    {
        public float duration;
        
        public DelayStep() 
        {
            name = "Delay Step";
        }
    }
}