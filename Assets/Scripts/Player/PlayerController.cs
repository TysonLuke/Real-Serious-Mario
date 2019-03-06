using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSM.WorldEntity;

namespace RSM
{
    namespace Player
    {
        /// <summary>
        /// Player controller keeps track of general player information such as score, lives, etc.
        /// </summary>
        public class PlayerController : MonoBehaviour
        {

            /// <summary>
            /// Used to get access to the player controller singleton.
            /// </summary>
            public static PlayerController Instance { get; private set; }

            /// <summary>
            /// Used to access the player's current score. Cannot be changed directly.
            /// </summary>
            public int Score { get; private set; }

            /// <summary>
            /// Used to get access to the player's life count. Cannot be changed directly.
            /// </summary>
            public int Lives { get; private set; }

            // Keeps track of the last checkpoint we reached, so we know where to go back to.
            public Checkpoint currentCheckpoint;

            // Whether or not we currently have the powerup.
            public bool poweredUp = false;

            /// <summary>
            /// Set up a basic Player Controller with the default settings.
            /// </summary>
            public PlayerController()
            {
                Score = 0;
                Lives = 2;
                currentCheckpoint = null;
                poweredUp = false;
            }

            /// <summary>
            /// Set up player controller singleton.
            /// </summary>
            private void Awake()
            {
                if (Instance != null)
                {
                    DestroyImmediate(gameObject);
                }
                else
                {
                    Instance = this;
                }
            }

            /// <summary>
            /// Called to update the player's current checkpoint information.
            /// <param name="reached">The checkpoint which was aquired.</param>
            /// </summary>
            public void CheckpointReched(Checkpoint reached)
            {
                // Update current checkpoint, and get the checkpoint to read the world status.
                currentCheckpoint = reached;
                currentCheckpoint.Activate();
            }

            /// <summary>
            /// Adds points to the player's total score.
            /// <param name="score">The number of points to be added.</param>
            /// </summary>
            public void AddScore(int score)
            {
                // Make sure we're not adding a negative number to the points.
                score = System.Math.Abs(score);

                Score += score;
            }

            /// <summary>
            /// Called to remove a life from the player when they are killed.
            /// </summary>
            public void LoseLife()
            {
                Lives -= 1;

                if (Lives < 0)
                {
                    GameOver();
                }
            }

            /// <summary>
            /// Called when the player runs out of lives to end the game.
            /// </summary>
            private void GameOver()
            {
                // Disable player movement.
                // Display game over info.
                // Display high scores.
                // Save high scores.
            }

        }
    }
}
