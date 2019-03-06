using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        /// <summary>
        /// The coins, which give score to the player when collected.
        /// </summary>
        public class Coin : Collectable
        {
            [SerializeField]
            [Tooltip("How much score is obtained by collecting this object.")]
            private int score;

            public override void OnTriggerEnter2D(Collider2D col)
            {
                if (col.gameObject.tag == "Player")
                {
                    RSM.Player.PlayerController.Instance.AddScore(score);
                    Destroy(gameObject);
                }
            }
        }

    }
}
