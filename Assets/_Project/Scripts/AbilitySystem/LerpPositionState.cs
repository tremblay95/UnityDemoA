using UnityEngine;
using Utilities;

namespace UnityDemoA
{
    public class LerpPositionState : AbilityExecutionState
    {
        private CountdownTimer timer;
        private readonly Vector3 initialPosition;
        private readonly Vector3 targetPosition;
        
        public LerpPositionState(PlayerController player, Animator animator, Vector3 initialPosition, Vector3 targetPosition, float stateDuration) : base(player, animator)
        {
            timer = new CountdownTimer(stateDuration);
            timer.OnStart += () => player.transform.position = initialPosition;
            timer.OnStop += () => player.transform.position = targetPosition;
            
            this.initialPosition = initialPosition;
            this.targetPosition = targetPosition;
        }
        
        public override void OnEnter() => timer.Start();

        public override void OnUpdate()
        {
            player.transform.position = Vector3.Lerp(initialPosition, targetPosition, timer.Progress);
            timer.Tick(Time.deltaTime);
        }
        
        public override bool IsFinished() => timer.IsFinished;
    }
}