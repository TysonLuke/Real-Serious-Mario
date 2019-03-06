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

            public PlayerMovement Movement { get; private set; }

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

            public float minY = -15;

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

                // Get movement component.
                Movement = GetComponent<PlayerMovement>();
            }

            /// <summary>
            /// Called to update the player's current checkpoint information.
            /// <param name="reached">The checkpoint which was aquired.</param>
            /// </summary>
            public void CheckpointReched(Checkpoint reached)
            {
                // Update current checkpoint.
                currentCheckpoint = reached;
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
                // Lose a life, and check that we still have any after this.
                --Lives;

                if (Lives < 0)
                {
                    RSM.Levels.LevelController.Instance.StartCoroutine("FadeOutOnly");
                    GameOver();
                }
                else
                {            
                    RSM.Levels.LevelController.Instance.StartCoroutine("FadeOut");
                    Invoke("ResetLocation", RSM.Levels.LevelController.Instance.timeToFade);
                }
            }

            private void ResetLocation()
            {                
                try
                {
                    // If we still have lives, return to the location of the checkpoint.
                    transform.position = currentCheckpoint.Location();
                    RSM.Levels.LevelController.Instance.ReloadLevel();
                }
                catch (System.NullReferenceException)
                {
                    throw new System.NullReferenceException("No current checkpoint found, make sure a checkpoint is assigned at the start of the level for the player to obtain.");
                }
            }

            public void LevelLoaded(Vector2 location)
            {
                // Move to the location we want to be at.
                transform.position = location;

                // Set the location where we die below, based on where we spawned.
                minY = location.y - 15f;

                // Make sure the camera and background are lined up properly.
                Camera.main.GetComponent<CameraFollow>().Start();
                GameObject.Find("Background").GetComponent<CameraFollow>().Start();
            }

            /// <summary>
            /// Called when the player runs out of lives to end the game.
            /// </summary>
            private void GameOver()
            {
                GameObject.Find("GameOverCanvas").GetComponent<Interface.GameOver>().Ended();
            }

        }
    }
}
