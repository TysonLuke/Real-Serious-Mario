using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TysonLuke
{
    namespace Utilities
    {

        /// <summary>
        /// Used for holding data from a CSV file. Data will be held as strings and will need to be parsed.
        /// </summary>
        public class CSVData
        {
            private string[,] contents;

            /// <summary>
            /// Used to parse a CSV file into a CSVData.
            /// <param name="data">The CSV file to be parsed.</params>
            /// </summary>
            public CSVData(TextAsset data)
            {
                MakeArray(data);
            }

            private void MakeArray(TextAsset input)
            {
                // Turn the text asset into a string.
                string text = input.text;
                // Split it by row.
                string[] rows = text.Split('\n');

                // Get the row legnth.
                int length = rows[0].Split(',').Length;

                // Set the array size so we've got somewhere to assign to.
                contents = new string[length, rows.Length];

                for (int y = 0; y < rows.Length; ++y)
                {
                    // Split current row into invidual cells.
                    string[] current = rows[y].Split(',');

                    for (int x = 0; x < current.Length; ++x)
                    {
                        // Add each cell to it's correct location.
                        contents[x, y] = current[x];
                    }
                }
            }

            public int ColumnCount()
            {
                return contents.GetLength(0);
            }

            public int RowCount()
            {
                return contents.GetLength(1);
            }

            public string Cell(int x, int y)
            {
                return contents[x,y];
            }

            public string[,] Data()
            {
                return contents;
            }

            public string[] Row(int row)
            {
                string[] currentRow = new string[ColumnCount()];
                for (int i = 0; i < ColumnCount(); ++i)
                {
                    currentRow[i] = Cell(i, row);
                }

                return currentRow;
            }

            public string[] Column(int column)
            {
                string[] currentColumn = new string[RowCount()];
                for (int i = 0; i < RowCount(); ++i)
                {
                    currentColumn[i] = Cell(column, i);
                }

                return currentColumn;
            }

        }
    }
}
