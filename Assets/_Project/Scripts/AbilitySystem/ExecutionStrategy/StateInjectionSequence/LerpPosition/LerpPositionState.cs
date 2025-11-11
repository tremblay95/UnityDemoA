using ImprovedTimers;
using UnityEngine;

namespace UnityDemoA
{
    public class LerpPositionState : AbilityState
    {
        private CountdownTimer timer;
        private readonly Vector3 initialPosition;
        private readonly Vector3 targetPosition;
        
        public LerpPositionState(PlayerController player, Animator animator, LerpPositionStateData data, Transform source, Transform target) : base(player, animator)
        {
            initialPosition = data.initialPositionRelativeTo.GetPosition(source.position, target.position, data.initialPosition);
            targetPosition = data.targetPositionRelativeTo.GetPosition(source.position, target.position, data.targetPosition);
            
            timer = new CountdownTimer(data.duration);
            timer.OnTimerStart += () => player.transform.position = data.initialPosition;
            timer.OnTimerStop += () => player.transform.position = data.targetPosition;
        }
        
        public override void OnEnter() => timer.Start();

        public override void OnUpdate()
        {
            player.transform.position = Vector3.Lerp(initialPosition, targetPosition, timer.Progress);
        }
        
        public override bool IsFinished() => timer.IsFinished;
        
        ~LerpPositionState() => timer.Dispose();
    }
}