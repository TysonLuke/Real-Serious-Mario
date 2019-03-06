using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        public class CrumblePlatform : MonoBehaviour
        {
            [SerializeField]
            [Tooltip("The time taken before this platform is destroyed.")]
            private float timeToFall = 1f;

            [SerializeField]
            [Tooltip("How quickly the platform will fall when it begins to fall.")]
            private float fallSpeed = 10f;

            // Whether or not we're currently falling.
            private bool falling = false;

            // Where we started from.
            private Vector3 origin;

            private void Awake()
            {
                origin = transform.position;
            }

            private void OnCollisionEnter2D(Collision2D col)
            {
                // If the player collides with this object, destroy it after the timer ends.
                if (col.gameObject.tag == "Player")
                {
                    Invoke("BeginFalling", timeToFall);
                }
            }

            /// <summary>
            /// Called after hitting a checkpoint. Restores to original location and stops falling.
            /// </summary>
            private void Restore()
            {
                transform.position = origin;
                falling = false;
            }

            private void BeginFalling()
            {
                falling = true;

                // Stop falling once it's definitely off the screen to save a bit of performance.
                Invoke("StopFalling", 10);
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
                falling = false;
            }
        }
    }
}
