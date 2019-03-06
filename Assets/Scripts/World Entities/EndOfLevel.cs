using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSM
{
    namespace WorldEntity
    {
        public class EndOfLevel : MonoBehaviour
        {
            // Name of the next level we're going to load when we hit this object.
            public string nextLevel = "";

            private void OnCollisionEnter2D(Collision2D col)
            {
                if (col.gameObject.tag == "Player")
                {
                    SceneManager.LoadScene(nextLevel);
                }
            }

        }
    }
}