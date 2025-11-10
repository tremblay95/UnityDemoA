using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(menuName = "Abilities/Execution Strategies/State Injection", fileName = "StateInjectionAbilityExecutionStrategy")]
    public abstract class StateInjectionAbilityExecutionStrategy : AbilityExecutionStrategy
    {
        protected PlayerController targetController;
        protected Animator targetAnimator;
        protected StateMachine targetFSM;
        
        public override void Execute(IAbilityEffect effect, Transform source, Transform target)
        {
            if (!target.TryGetComponent(out targetController)) return;
            if (!target.TryGetComponent(out targetAnimator)) return;
            
            // targetFSM = targetController.GetStateMachine();
        }
    }

    public class VoidRiftExecutionStrategy : StateInjectionAbilityExecutionStrategy
    {
        [SerializeField] VoidRiftConfig config;
        
        public override void Execute(IAbilityEffect effect, Transform source, Transform target)
        {
            base.Execute(effect, source, target);
            
            if (targetController == null || targetFSM == null) return;
            
            Vector3 initialPosition = target.position;
            Vector3 liftTargetPosition = initialPosition + new Vector3(0f, config.liftYOffset, 0f);
            Vector3 voidDragTargetPosition = initialPosition + new Vector3(0f, config.voidDragYOffset, 0f);
            
            Vector3 voidEmergeTargetPosition = source.position + config.voidEmergeRelativePosition;
            Vector3 voidEmergeInitialPosition = voidEmergeTargetPosition;
            voidEmergeInitialPosition.y = source.position.y + config.voidDragYOffset;
            
            // Define states
            SuspendState suspendState = new SuspendState(targetController, targetAnimator,
                Animator.StringToHash(config.suspendAnimationStateName));
            
            LerpPositionState liftState = new LerpPositionState(targetController, targetAnimator, 
                initialPosition, liftTargetPosition, config.liftOffDuration);
            
            DelayState raisedState = new DelayState(targetController, targetAnimator, config.raisedDuration);
            
            LerpPositionState voidDragState = new LerpPositionState(targetController, targetAnimator,
                liftTargetPosition, voidDragTargetPosition, config.voidDragDuration);
            
            LerpPositionState voidEmergeState = new LerpPositionState(targetController, targetAnimator,
                voidEmergeInitialPosition, voidEmergeTargetPosition, config.voidEmergeDuration);
            
            DelayState vulnerableState = new DelayState(targetController, targetAnimator, config.vulnerableDuration);
            
            // Todo: LaunchState
            
            // targetFSM.InjectState(suspendedState);
        }
    }
}