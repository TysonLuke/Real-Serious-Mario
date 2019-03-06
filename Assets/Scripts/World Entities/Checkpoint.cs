using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        public class Checkpoint : MonoBehaviour
        {
            [SerializeField]
            [Tooltip("The offset from the object's origin location to resapwn at.")]
            private Vector3 offset;

            /// <summary>
            /// Used for the location to return to.
            /// <returns>The position off this object, plus the offset it uses.</returns>
            /// </summary>
            public Vector3 Location()
            {
                return transform.position + offset;
            }

            private void OnTriggerEnter2D(Collider2D col)
            {
                // If we hit the player, send it a reference to this checkpoint.
                if (col.gameObject.tag == "Player")
                {
                    RSM.Player.PlayerController.Instance.CheckpointReched(this);
                }
            }
        }

    }
}