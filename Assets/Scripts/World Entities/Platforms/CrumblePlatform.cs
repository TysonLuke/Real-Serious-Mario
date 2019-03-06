using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        public class CrumblePlatform : Platform
        {
            [SerializeField]
            [Tooltip("The time taken before this platform is destroyed.")]
            private float timeToFall = 1f;

            [SerializeField]
            [Tooltip("How quickly the platform will fall when it begins to fall.")]
            private float fallSpeed = 10f;

            // Whether or not we're currently falling.
            private bool falling = false;

            private void OnCollisionEnter2D(Collision2D col)
            {
                // If the player collides with this object, destroy it after the timer ends.
                if (col.gameObject.tag == "Player")
                {
                    // Get the direction between the player and the box.
                    Vector2 offset = transform.position - col.transform.position;

                    // Check whether we hit it from the top or not.
                    if (offset.y > 0)
                    {
                        // If we hit the bottom, we have no need to do this.
                        return;
                    }

                    // If we landed on top if it, fall after a delay.
                    Invoke("BeginFalling", timeToFall);
                }
            }

            /// <summary>
            /// Called after hitting a checkpoint. Restores to original location and stops falling.
            /// </summary>
            public override void Enable()
            {
                base.Enable();
                falling = false;
            }

            private void BeginFalling()
            {
                falling = true;

                // Stop falling once it's definitely off the screen to save a bit of performance.
                Invoke("StopFalling", 1);
            }

            private void Update()
            {
                // Once we've begun falling, drop off the screen.
                if (falling)
                {
                    transform.Translate(new Vector3(0, -fallSpeed * Time.deltaTime, 0));
                }
            }

            private void StopFalling()
            {
                Disable();
                falling = false;
            }
        }
    }
}
