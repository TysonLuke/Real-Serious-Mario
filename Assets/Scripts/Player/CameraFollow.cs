using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace Player
    {

        /// <summary>
        /// Makes the camera follow the player as they progress through the level.
        /// </summary>
        public class CameraFollow : MonoBehaviour
        {
            // The y position we will be sticking to.
            private float yPos;

            // Allows us to move the background, as well as the camera with the one script.
            public float zPos = -10;

            private void Start()
            {
                // Find the position of the player when we start the level.
                yPos = PlayerController.Instance.transform.position.y;
            }

            private void LateUpdate()
            {
                // We want to follow the player on the X axis, but stay in the same positions for both y and z.
                transform.position = new Vector3(PlayerController.Instance.transform.position.x, yPos, zPos);
            }
        }
    }
}
