using System;
using System.Collections.Generic;
using ImprovedTimers;
using KBCore.Refs;
using UnityEngine;

namespace UnityDemoA
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header( "References" )]
        [SerializeField, Self] private Rigidbody rb;
        [SerializeField, Child] private Animator animator;
        [SerializeField] private Transform avatar;
        [SerializeField, Anywhere] private InputReader input;
        
        [Header( "Movement Settings" )]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float turnSpeed = 10f;
        [SerializeField] private float smoothTime = 0.2f;

        [Header( "Attack Settings" )]
        [SerializeField] private float attackDuration = 0.5f;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private int attackDamage = 10;
        
        private const float ZeroF = 0f;
        private Transform mainCamera;
        
        private float currentSpeed;
        private float velocity;

        // Timers
        private List<Timer> timers;
        private CountdownTimer attackTimer;
        
        private StateMachine stateMachine;
        
        // Animator parameters
        static readonly int SpeedHash = Animator.StringToHash("Speed");

        private void Awake()
        {
            mainCamera = Camera.main.transform;

            SetupTimers();
            SetupStateMachine();
        }

        private void SetupTimers()
        {
            // Setup timers
            attackTimer = new CountdownTimer(attackDuration);

            timers = new(1) { attackTimer };
        }

        private void OnDestroy() => timers.ForEach(timer => timer.Dispose());

        private void SetupStateMachine()
        {
            // State Machine
            stateMachine = new StateMachine();
            
            // Declare states
            var locomotionState = new LocomotionState(this, animator);
            var attackState = new AttackState(this, animator);
            
            // Define transitions
            stateMachine.AddTransition(locomotionState, attackState, new FuncPredicate(() => attackTimer.IsRunning));
            stateMachine.AddTransition(attackState, locomotionState, new FuncPredicate(() => !attackTimer.IsRunning));
            
            // Set initial state
            stateMachine.SetState(locomotionState);
        }

        private void OnEnable()
        {
            input.Attack += OnAttack;
        }
        
        private void OnDisable()
        {
            input.Attack -= OnAttack;
        }

        private void Start() => input.EnableInputActions();

        private void OnAttack()
        {
            if (!attackTimer.IsRunning)
            {
                attackTimer.Start();
            }
        }
        
        private void Update()
        {
            stateMachine.Update();
            
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(SpeedHash, currentSpeed);
        }

        private void FixedUpdate() => stateMachine.FixedUpdate();


        public void Attack()
        {
            Debug.Log("Attack executed!");
            // Attack logic here (e.g., raycast to detect hit, apply damage, etc
        }
        
        public void HandleMovement()
        {
            var moveDirection = new Vector3(input.Direction.x, 0, input.Direction.y);
            var adjustedDirection = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) * moveDirection;

            
            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);
                
                rb.linearVelocity = new Vector3(ZeroF, rb.linearVelocity.y, ZeroF);
            }
        }

        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            avatar.rotation = Quaternion.RotateTowards(avatar.rotation, targetRotation, turnSpeed);
        }

        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            var velocity = adjustedDirection * moveSpeed;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }

        private void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}