using UnityEngine;
using Utilities;

namespace UnityDemoA
{
    public class DelayState : AbilityState
    {
        // Todo: consider using a coroutine instead of a timer
        private CountdownTimer timer;
        
        public DelayState(PlayerController player, Animator animator, DelayStateData data) : base(player, animator)
        {
            timer = new CountdownTimer(data.duration);
        }
        
        public override void OnEnter() => timer.Start();
        public override void OnUpdate() => timer.Tick(Time.deltaTime);
        public override bool IsFinished() => timer.IsFinished;
    }
}