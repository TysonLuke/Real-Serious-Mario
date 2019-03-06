using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        public class EndOfLevel : MonoBehaviour
        {

            private void OnCollisionEnter2D(Collision2D col)
            {
                if (col.gameObject.tag == "Player")
                {
                    RSM.Levels.LevelController.Instance.NextLevel();
                }
            }

        }
    }
}