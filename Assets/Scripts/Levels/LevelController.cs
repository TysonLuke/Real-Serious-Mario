using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSM.WorldEntity;

namespace RSM
{
    namespace Levels
    {
        public class LevelController : MonoBehaviour
        {

            public static LevelController Instance { get; private set; }

            private List<Platform> disabled = new List<Platform>();

            private float timeFading = 0f;

            public float timeToFade = .5f;

            public SpriteRenderer fadeImage;

            /// <summary>
            /// Set up level controller singleton.
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
            /// Destroy the current level in preperation of loading a different level in.
            /// </summary>
            public void UnloadLevel()
            {
                for (int childIndex = transform.childCount - 1; childIndex >= 0; --childIndex)
                {
                    Destroy(transform.GetChild(childIndex).gameObject);
                }
            }

            /// <summary>
            /// Reload all objects to their original locations, ready for the player to play again.
            /// </summary>
            public void ReloadLevel()
            {
                EnableAll();
            }

            public void DisableObject(Platform platform)
            {
                // Add to this list of platforms that have been disabled.
                disabled.Add(platform);
            }

            private void EnableAll()
            {
                foreach (Platform platform in disabled)
                {
                    platform.Enable();
                }
            }

            public void NextLevel()
            {
                StartCoroutine("FadeIntoNewLevel");
            }

#region Different Fade Effects

            public IEnumerator FadeOut()
            {
                Player.PlayerController.Instance.Movement.SetMovement(false);
                while (timeFading < timeToFade)
                {
                    // If we're still fading, find the point of the fade we're at, and add deltaTime.
                    fadeImage.color = new Color(0, 0, 0, timeFading / timeToFade);
                    timeFading += Time.deltaTime;
                    yield return null;
                }
                timeFading = 0f;
                StartCoroutine("FadeIn");
            }

            public IEnumerator FadeOutOnly()
            {
                Player.PlayerController.Instance.Movement.SetMovement(false);
                while (timeFading < timeToFade)
                {
                    // If we're still fading, find the point of the fade we're at, and add deltaTime.
                    fadeImage.color = new Color(0, 0, 0, timeFading / timeToFade);
                    timeFading += Time.deltaTime;
                    yield return null;
                }
                timeFading = 0f;
            }

            public IEnumerator FadeIntoNewLevel()
            {
                Player.PlayerController.Instance.Movement.SetMovement(false);
                while (timeFading < timeToFade)
                {
                    // If we're still fading, find the point of the fade we're at, and add deltaTime.
                    fadeImage.color = new Color(0, 0, 0, timeFading / timeToFade);
                    timeFading += Time.deltaTime;
                    yield return null;
                }
                timeFading = 0f;
                GetComponent<LoadLevelFromFile>().LoadLevel();
            }

            public IEnumerator FadeIn()
            {
                while (timeFading < timeToFade)
                {
                    // If we're still fading, find the point of the fade we're at, and add deltaTime.
                    fadeImage.color = new Color(0, 0, 0, 1 - timeFading / timeToFade);
                    timeFading += Time.deltaTime;
                    yield return null;
                }
                timeFading = 0f;
                Player.PlayerController.Instance.Movement.SetMovement(true);
            }

#endregion

        }
    }
}