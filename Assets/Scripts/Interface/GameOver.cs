using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RSM
{
    namespace Interface
    {
        public class GameOver : MonoBehaviour
        {
            private class HiScore : IComparable<HiScore>
            {
                public string playerName;
                public int score;

                // Constructor for newing this class easier.
                public HiScore(string n, int s)
                {
                    playerName = n;
                    score = s;
                }

                // Allow us to sort this class more simply.
                public int CompareTo(HiScore other)
                {
                    return score.CompareTo(other.score);
                }
            }

            // For entering player name.
            public InputField name;

            // Where we're showing the scores.
            public Text displayScores;

            // Whether or not we got a hiscore.
            private bool hiScored = false;

            // The panel containing the interface.
            public GameObject panel;

            string path;
            List<HiScore> scores = new List<HiScore>();

            private void Awake()
            {
                // We can't do this during the constructor, so we'll do it here.
                path = Application.persistentDataPath + "/HiScores.hsf";

                if (File.Exists(path))
                {
                    
                    // Read the text file in.
                    string totalText = File.ReadAllText(path);

                    // Remove returns so we only work with newline.
                    totalText = totalText.Replace("\r", "");

                    // Split by newlines.
                    string [] scoresText = totalText.Split('\n');

                    // Add all the scores to the list and sort them.
                    foreach (string line in scoresText)
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            break;
                        }
                        string[] score = line.Split(',');
                        scores.Add(new HiScore(score[0], int.Parse(score[1])));
                    }
                    scores.Sort();
                    scores.Reverse();

                    // Format the text to show.
                    string display = "";
                    foreach (HiScore score in scores)
                    {
                        display += score.playerName + ": " + score.score + "\n";
                    }

                    // Show the score text on the screen.
                    displayScores.text = display;

                }
            }

            public void Ended()
            {
                // Show hi score panel.
                panel.SetActive(true);

                if (scores.Count < 5)
                {
                    // If there are under 5 scores we get in by default.
                    EnableEntry();
                }
                else if (RSM.Player.PlayerController.Instance.Score > scores[4].score)
                {
                    // Otherwise, we need to ourscore the lowest score.
                    EnableEntry();
                }
            }

            public void EnableEntry()
            {
                // If we scored a hi score, we need to see the name entry field.
                hiScored = true;
                name.gameObject.SetActive(true);
            }

            public void DoneButton()
            {
                if (hiScored)
                {
                    // Add the new score to hiscores data.
                    scores.Add(new HiScore(name.text, RSM.Player.PlayerController.Instance.Score));

                    // Sort the data into the correct order before we save it.
                    scores.Sort();
                    scores.Reverse();

                    // Save score to file.
                    SaveScores();
                }
                else
                {
                    ReturnToMenu();
                }
            }

            private void ReturnToMenu()
            {
                // Return to menu.
                SceneManager.LoadScene("Menu");
            }

            private void SaveScores()
            {
                // Write the stirng to use.
                string data = "";
                for (int index = 0; index < scores.Count && index < 5; ++index)
                {
                    data += scores[index].playerName + "," + scores[index].score + "\r\n";
                }

                // Save scores to file.
                if (!File.Exists(path))
                {
                    // Make sure we close this file after we make it, or we can't edit it.
                    File.Create(path).Close();
                }

                // Write the text to the file.
                File.WriteAllText(path, data);

                ReturnToMenu();
            }
        }
    }
}