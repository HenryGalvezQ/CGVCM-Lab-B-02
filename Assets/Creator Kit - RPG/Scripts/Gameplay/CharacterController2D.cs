using System;
using System.Collections;
using System.Collections.Generic;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.U2D;

namespace RPGM.Gameplay
{
    /// <summary>
    /// A controller for animating a 4 directional sprite using Physics,
    /// with support to invert controls, apply slippery and slow movement when passing through trigger zones.
    /// </summary>
    public class CharacterController2D : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float speed = 1f;
        public float acceleration = 2f;

        [Header("Runtime Flags")]
        public Vector3 nextMoveCommand;
        public Animator animator;
        public bool flipX = false;

        // Flags for special movement
        private bool invertControls = false;
        private float slipMultiplier = 1f;
        private float speedMultiplier = 1f;  // New: slow zone multiplier

        new Rigidbody2D rigidbody2D;
        SpriteRenderer spriteRenderer;
        PixelPerfectCamera pixelPerfectCamera;

        enum State { Idle, Moving }
        State state = State.Idle;

        Vector3 start, end;
        Vector2 currentVelocity;
        float distance;
        float velocity;

        void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            pixelPerfectCamera = GameObject.FindObjectOfType<PixelPerfectCamera>();
        }

        void Update()
        {
            switch (state)
            {
                case State.Idle:
                    IdleState();
                    break;
                case State.Moving:
                    MoveState();
                    break;
            }
        }

        void IdleState()
        {
            if (nextMoveCommand != Vector3.zero)
            {
                start = transform.position;
                end = start + ApplyInversion(nextMoveCommand);
                distance = (end - start).magnitude;
                velocity = 0;
                UpdateAnimator(nextMoveCommand);
                nextMoveCommand = Vector3.zero;
                state = State.Moving;
            }
        }

        void MoveState()
        {
            // Smooth velocity build-up
            velocity = Mathf.Clamp01(velocity + Time.deltaTime * acceleration);
            Vector3 command = ApplyInversion(nextMoveCommand);
            UpdateAnimator(command);

            // Compute damping time with slip effect
            float smoothTime = acceleration * slipMultiplier;
            // Apply both slip and slow multipliers to movement speed
            Vector2 targetVelocity = new Vector2(command.x, command.y) * speed * speedMultiplier;

            rigidbody2D.velocity = Vector2.SmoothDamp(
                rigidbody2D.velocity,
                targetVelocity,
                ref currentVelocity,
                smoothTime,
                speed * speedMultiplier
            );

            // Flip sprite based on movement direction
            spriteRenderer.flipX = rigidbody2D.velocity.x >= 0;
        }

        // Applies inversion if the flag is set
        Vector3 ApplyInversion(Vector3 input)
        {
            return invertControls ? -input : input;
        }

        void UpdateAnimator(Vector3 direction)
        {
            if (animator)
            {
                animator.SetInteger("WalkX", direction.x < 0 ? -1 : direction.x > 0 ? 1 : 0);
                animator.SetInteger("WalkY", direction.y < 0 ? 1 : direction.y > 0 ? -1 : 0);
            }
        }

        void LateUpdate()
        {
            if (pixelPerfectCamera != null)
            {
                transform.position = pixelPerfectCamera.RoundToPixel(transform.position);
            }
        }

        // Detect trigger zones for movement effects
        void OnTriggerEnter2D(Collider2D other)
        {
            // Invert controls when entering an InvertZone
            if (other.CompareTag("InvertZone"))
            {
                invertControls = true;
                Debug.Log("Controls inverted");
            }
            // Restore controls when entering a RestoreZone
            else if (other.CompareTag("RestoreZone"))
            {
                invertControls = false;
                Debug.Log("Controls restored");
            }
            // Enable slippery movement when entering a SlipperyZone
            else if (other.CompareTag("SlipperyZone"))
            {
                slipMultiplier = 10f; // slide effect
                Debug.Log("Slippery surface engaged");
            }
            // Disable slippery movement when entering a RestoreSlipperyZone
            else if (other.CompareTag("RestoreSlipperyZone"))
            {
                slipMultiplier = 1f;
                Debug.Log("Normal surface restored");
            }
            // Enable slow movement when entering a SlowZone
            else if (other.CompareTag("SlowZone"))
            {
                speedMultiplier = 0.2f; // half speed
                Debug.Log("Slow surface engaged");
            }
            // Restore normal speed when entering a RestoreSlowZone
            else if (other.CompareTag("RestoreSlowZone"))
            {
                speedMultiplier = 1f;
                Debug.Log("Normal speed restored");
            }
        }
    }
}
