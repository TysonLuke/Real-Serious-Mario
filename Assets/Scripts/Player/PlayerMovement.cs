using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace Player
    {

        /// <summary>
        /// PlayerMovement deals with all movement inputs, and turning those inputs into commands.
        /// </summary>
        public class PlayerMovement : MonoBehaviour
        {
            [SerializeField]
            [Tooltip("The lead in movement curve to use, if enabled.")]
            private AnimationCurve leadIn;

            [SerializeField]
            [Tooltip("Whether or not to use the lead in curve for movement.")]
            private bool useLeadIn = false;

            [SerializeField]
            [Tooltip("How long the lead in curve takes to complete.")]
            private float leadInTime = 1f;

            [SerializeField]
            [Tooltip("Scale to multiply any movement commands to the player.")]
            private float movementScale = 1f;

            [SerializeField]
            [Tooltip("The amount of force applied when jumping.")]
            private float jumpForce = 100f;

            private float moveTime = 0f;

            private BoxCollider2D boxCollider;

            private Rigidbody2D rb;

            private bool grounded = false;

            private void Awake()
            {
                boxCollider = GetComponent<BoxCollider2D>();
                rb = GetComponent<Rigidbody2D>();
            }

            private void Update()
            {
                CheckBelow();
                XMovement();
                Jump();
            }

            /// <summary>
            /// All the individual movement logic for moving left and right.
            /// </summary>
            private void XMovement()
            {
                float xMovement = Input.GetAxis("Horizontal");

                // If there's no movement inputs, we don't need to do anything.
                if (xMovement == 0)
                {
                    moveTime = 0f;
                    return;
                }

                // Scale the movement to the correct size.
                xMovement *= Time.deltaTime * movementScale;

                // If we're using the lead in, factor that into the movement.
                if (useLeadIn)
                {
                    float currentLeadIn = leadIn.Evaluate(moveTime / leadInTime);
                    xMovement *= currentLeadIn;

                    // Add this in after calculation so that we start at 0.
                    moveTime += Time.deltaTime;
                }

                // Get the position we want to move to.
                Vector3 newLocation = transform.position + new Vector3(xMovement, 0, 0);
                Vector2 collisionSize = new Vector2(boxCollider.size.x, boxCollider.size.y * 0.9f);

                // And check that that position is clear to move to.
                if (Physics2D.OverlapBox(newLocation, collisionSize, 0))
                {
                    // Check which objects we will be colliding with if we move there.
                    Collider2D[] hits = Physics2D.OverlapBoxAll(newLocation, collisionSize, 0);

                    foreach (Collider2D hit in hits)
                    {
                        // If we hit a wall at that location, we don't move at all.
                        if (hit.tag == "Solid")
                        {
                            return;
                        }
                    }
                }

                // If we made it this far, move to the location.
                transform.position = newLocation;
            }

            private void Jump()
            {
                // If we didn't press jump, we have no reason to continue.
                if (!grounded || !Input.GetButtonDown("Jump"))
                {
                    return;
                }

                // Add the upward jump force, and set us to not grounded so we can't jump again.
                rb.AddForce(Vector2.up * jumpForce);
                grounded = false;
            }

            private void CheckBelow()
            {
                // Check if there's any floors within the area 1/10th of our size below us.
                Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position - new Vector3(0, boxCollider.size.y / 10, 0), boxCollider.size, 0);
                foreach (Collider2D hit in hits)
                {
                    if (hit.tag == "Solid")
                    {
                        // If we hit the ground, we can be grounded and leave.
                        grounded = true;
                        return;
                    }
                }

                // If we didn't hit any ground at all, we're not grounded.
                grounded = false;
            }

        }
    }
}