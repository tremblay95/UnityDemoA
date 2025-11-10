using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(menuName = "Abilities/Execution Strategy Config/Void Rift Config", fileName = "VoidRiftAbilityContext")]
    public class VoidRiftConfig : ScriptableObject
    {
        public string suspendAnimationStateName = "Suspend";
        
        public float liftOffDuration = 0.5f;
        public float liftYOffset = 1f;
        
        public float raisedDuration = 1f;
        
        public float voidDragDuration = 0.5f;
        public float voidDragYOffset = -3f;
        
        public float voidEmergeDuration = 0.5f;
        [ Tooltip( "The target position of the void emerge state relative to the player" )]
        public Vector3 voidEmergeRelativePosition = new Vector3(0f, 1.5f, 1f);
        
        public float vulnerableDuration = 4f;
        
        public float minLaunchForce = 100f;
        public float maxLaunchForce = 500f;
        public float launchAngle = 45f;
    }
}