using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TysonLuke.Utilities;

namespace RSM
{
    namespace Levels
    {
        public class LoadLevelFromFile : MonoBehaviour
        {
            public Dictionary<string, CSVData> levels = new Dictionary<string, CSVData>();

            private string nextLevel = "0";

            // The object we're using for level generation.
            public GameObject ground, falling, enemy, headSmash, spike, checkpoint, endOfLevel, coin;

            /// <summary>
            /// Load the entire default folder on start, prepare the CSV files for use.
            /// </summary>
            public void Start()
            {
                // Get all levels from the default folder.
                Object[] files;
                files = Resources.LoadAll("", typeof(TextAsset));

                // Load them all into a dictionary with their names so we can look them up.
                foreach (var csv in files)
                {
                    CSVData current = new CSVData((TextAsset)csv);
                    levels.Add(current.Cell(0, 1), current);
                }

                if (PlayerPrefs.HasKey("LevelToLoad"))
                {
                    // Load the string for the level to load and then delete the prefs.
                    nextLevel = PlayerPrefs.GetString("LevelToLoad");

                    // Delete all is fine here because we've saved nothing else.
                    PlayerPrefs.DeleteAll();
                }

                LoadLevel();
            }

            public void LoadLevel()
            {

                // If there is no file name given, assume we've finished all levels and end the game.
                CSVData loading;
                if (string.IsNullOrEmpty(nextLevel))
                {
                    Player.PlayerController.Instance.GameOver();
                    return;
                }
                else
                {
                    // Unload previous level from the map.
                    LevelController.Instance.UnloadLevel();
                    LevelController.Instance.StartCoroutine("FadeIn");

                    // Get the array from the CSV data.
                    loading = levels[nextLevel];
                }

                // Take the name of the next level down.
                nextLevel = loading.Cell(1, 1);

                // Get the size of tiles this spreadsheet is using.
                Vector2 tileSize = new Vector2(float.Parse(loading.Cell(2, 1)), float.Parse(loading.Cell(3, 1)));

                Vector2 playerLocation = Vector2.zero;

                float yOffset = (loading.RowCount() - 3) * tileSize.y;

                for (int xAxis = 0; xAxis < loading.ColumnCount(); ++xAxis)
                {
                    for (int yAxis = 3; yAxis < loading.RowCount(); ++yAxis)
                    {
                        // The object we plan to place.
                        GameObject toPlace = null;

                        // If there is nothing in the cell, or it is just a newline they are useless to us.
                        if (string.IsNullOrEmpty(loading.Cell(xAxis, yAxis)) || loading.Cell(xAxis, yAxis) == "\r")
                        {
                            continue;
                        }

                        float perTileY = 0;

                        // Parse only the first character, as the final cell will contain newline characters.
                        switch (loading.Cell(xAxis, yAxis)[0])
                        {
                            case 'g':
                            {
                                toPlace = ground;
                                break;
                            }
                            case 'f':
                            {
                                toPlace = falling;
                                break;
                            }
                            case 'e':
                            {
                                toPlace = enemy;
                                break;
                            }
                            case 'h':
                            {
                                toPlace = headSmash;
                                break;
                            }
                            case 's':
                            {
                                toPlace = spike;
                                break;
                            }
                            case 'c':
                            {
                                toPlace = checkpoint;
                                break;
                            }
                            case 'n':
                            {
                                // End of level tile is scaled differently to the rest of the objects, needs it's own offset.
                                perTileY = tileSize.y / 2;
                                toPlace = endOfLevel;
                                break;
                            }
                            case 'p':
                            {
                                playerLocation = new Vector2(xAxis * tileSize.x, (-yAxis * tileSize.y) + yOffset);
                                continue;
                            }
                            case 'o':
                            {
                                toPlace = coin;
                                break;
                            }
                            default:
                            {
                                // If we ended up here, it means we haven't handled something correctly, and should find out what it was.
                                Debug.LogError("Cell contents were not handled at " + xAxis + ", " + yAxis + " contents are: " + loading.Cell(xAxis, yAxis));
                                break;
                            }
                        }

                        // Tell the player to get in position and update it's info.
                        RSM.Player.PlayerController.Instance.LevelLoaded(playerLocation);

                        Instantiate(toPlace, new Vector2(xAxis * tileSize.x, (-yAxis * tileSize.y) + yOffset + perTileY), Quaternion.identity, gameObject.transform);
                    }
                }

            }
        }
    }
}
