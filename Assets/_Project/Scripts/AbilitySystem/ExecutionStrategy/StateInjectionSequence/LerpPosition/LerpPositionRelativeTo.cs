using System;
using UnityEngine;

namespace UnityDemoA
{
    public enum LerpPositionRelativeTo
    {
        WorldSpace,
        SourceInitial,
        TargetInitial
    }
    
    public static class LerpPositionRelativeToExtensions
    {
        public static Vector3 GetPosition(this LerpPositionRelativeTo relativeTo, Vector3 source, Vector3 target, Vector3 positionOffset)
        {
            return relativeTo switch
            {
                LerpPositionRelativeTo.WorldSpace => positionOffset,
                LerpPositionRelativeTo.SourceInitial => source + positionOffset,
                LerpPositionRelativeTo.TargetInitial => target + positionOffset,
                _ => throw new ArgumentOutOfRangeException(nameof(relativeTo), relativeTo, null)
            };
        }
    }
}