using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RSM.Player;

namespace RSM
{
    namespace Interface
    {
        /// <summary>
        /// Used to update the UI and show information to the player.
        /// </summary>
        public class UIUpdate : MonoBehaviour
        {
            // The text boxes we're using to display this information.
            public Text scoreDisplay, livesDisplay;

            private void LateUpdate()
            {
                scoreDisplay.text = "SCORE: " + PlayerController.Instance.Score;
                livesDisplay.text = "LIVES: " + PlayerController.Instance.Lives;
            }
        }
    }
}
