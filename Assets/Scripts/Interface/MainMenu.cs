using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RSM
{
    namespace Interface
    {
        public class MainMenu : MonoBehaviour
        {

            public InputField input;

            public void StartGame()
            {
                SceneManager.LoadScene("Game");
            }

            public void SpecialStart()
            {
                PlayerPrefs.SetString("LevelToLoad", input.text);
                SceneManager.LoadScene("Game");
            }

            public void Quit()
            {
                Application.Quit();
            }
        }
    }
}
