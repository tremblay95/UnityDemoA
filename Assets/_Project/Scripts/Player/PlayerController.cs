using System;
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
        
        [Header( "Movement Settings")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float turnSpeed = 10f;
        [SerializeField] private float smoothTime = 0.2f;

        private const float ZeroF = 0f;
        private Transform mainCamera;
        
        private float currentSpeed;
        private float velocity;
        
        // Animator parameters
        static readonly int SpeedHash = Animator.StringToHash("Speed");

        private void Awake()
        {
            mainCamera = Camera.main.transform;
        }

        private void Start() => input.EnableInputActions();

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(SpeedHash, currentSpeed);
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
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