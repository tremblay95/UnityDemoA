using UnityEngine;

namespace UnityDemoA
{
    public interface ITargetable
    {
        Transform Transform { get; }
        
        void Highlight();
        void Unhighlight();
    }
}