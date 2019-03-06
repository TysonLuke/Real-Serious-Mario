using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        /// <summary>
        /// Base class for collectable objets in the world.
        /// </summary>
        public abstract class Collectable : MonoBehaviour
        {
            private Vector3 position;

            // How often bounces happen.
            private float bounceSpeed = 4f;

            // How high the bounces go.
            private float bounceHeight = 0.15f;

            // For making things looks less synched up.
            private float offsetTime;

            private void Start()
            {
                // Remember where we started, so we can bounce relative to it.
                position = transform.position;

                // Offset so they don't all bounce the same as eachother.
                offsetTime = Random.Range(0, Mathf.PI);
            }

            private void Update()
            {
                // Bounce on the spot.
                transform.position = position + new Vector3(0, (Mathf.Sin(((Time.timeSinceLevelLoad) * bounceSpeed) + offsetTime) * bounceHeight), 0);
            }

            public abstract void OnTriggerEnter2D(Collider2D col);
        }

    }
}
