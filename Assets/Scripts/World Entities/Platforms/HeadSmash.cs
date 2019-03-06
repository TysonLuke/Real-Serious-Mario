using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        public class HeadSmash : Platform
        {

            [SerializeField]
            [Tooltip("The amount of score given for destroying this object.")]
            private int scoreOnDestroy = 100;

            private void OnCollisionEnter2D(Collision2D col)
            {
                if (col.gameObject.tag == "Player")
                {
                    // Get the direction between the player and the box.
                    Vector2 offset = transform.position - col.transform.position;
                    
                    // Normalize the vector, and see which axis is stronger.
                    offset.Normalize();

                    // If the y is over 0, we're not below the box.
                    if (offset.y < 0)
                    {
                        // Do the less expensive check first, before we do anything more difficult.
                        return;
                    }

                    // If the x axis is smaller than the y axis, we've hit the bottom, and should be destroyed.
                    if (Mathf.Abs(offset.x) < Mathf.Abs(offset.y))
                    {
                        RSM.Player.PlayerController.Instance.AddScore(scoreOnDestroy);
                        Disable();
                    }
                }
            }
        }
    }
}
