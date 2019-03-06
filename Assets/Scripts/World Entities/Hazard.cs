using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        /// <summary>
        /// Hazards are anything that will kill the player when touched.
        /// </summary>
        public class Hazard : MonoBehaviour
        {

            private void OnCollisionEnter2D(Collision2D col)
            {

                // If we hit the player, they have died and need to lose a life.
                if (col.gameObject.tag == "Player")
                {
                    RSM.Player.PlayerController.Instance.LoseLife();
                }
                
            }

        }
    }
}
