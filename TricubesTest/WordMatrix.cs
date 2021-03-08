using System;
using System.IO;

namespace TricubesTest
{
    public class WordMatrix
    {
        int[] rowDirections = { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] columnDirections = { -1, 0, 1, 1, 1, 0, -1, -1 };
        string filePath = @"matrix.txt";
        MatrixCell[][] matrix;

        public WordMatrix()
        {
        }

        public void Start()
        {
            this.BuildMatrix();
            this.PrintMatrix();
            Console.WriteLine("Enter a Word:");
            string word = Console.ReadLine();
            word = word.ToUpper();
            while(word != "Q")
            {
                this.Query(word);
                this.PrintMatrix();
                Console.WriteLine("Enter a Word:");
                word = Console.ReadLine();
                word = word.ToUpper();
            }
        }

        public void BuildMatrix()
        {
            string[] letters = File.ReadAllLines(filePath);
            matrix = new MatrixCell[letters.Length][];
            for (int i = 0; i < letters.Length; ++i)
            {
                matrix[i] = new MatrixCell[letters[i].Length];

                for (int j = 0; j < letters[i].Length; ++j)
                {
                    matrix[i][j] = new MatrixCell(letters[i][j]);
                }
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    Console.Write(matrix[i][j].Letter);
                }

                Console.Write(' ');

                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    Console.Write(matrix[i][j].Found ? matrix[i][j].Letter : ' ');
                }

                Console.WriteLine();
            }
        }

        public void Query(string word)
        {
            for(int i=0; i< matrix.Length; ++i)
            {
                for(int j=0;j < matrix[i].Length; ++j)
                {
                    if (word[0] == matrix[i][j].Letter)
                    {
                        InitiateSearch(word, i, j);
                    }
                }
            }
        }

        private void InitiateSearch(string word, int i, int j)
        {
            if (word.Length == 1)
            {
                matrix[i][j].Found = true;
                return;
            }

            bool foundAtLeastOnce = false;

            for(int k = 0; k < rowDirections.Length; k++)
            {
                int newRow = i + rowDirections[k];
                int newColumn = j + columnDirections[k];

                if (newRow > -1 && newRow < matrix.Length && newColumn > -1 && newColumn < matrix[0].Length)
                {
                    if (find(word.Substring(1), newRow, newColumn, rowDirections[k], columnDirections[k]) && !foundAtLeastOnce)
                    {
                        foundAtLeastOnce = true;
                        matrix[i][j].Found = true;
                    }
                }
            }
        }

        private bool find(string word, int currentRow, int currentColumn, int rowIncrement, int columnIncrement)
        {
            if (matrix[currentRow][currentColumn].Letter != word[0])
            {
                return false;
            }

            if (word.Length == 1)
            {
                matrix[currentRow][currentColumn].Found = true;
                return true;
            }

            int newRow = currentRow + rowIncrement;
            int newColumn = currentColumn + columnIncrement;

            if (newRow > -1 && newRow < matrix.Length && newColumn > -1 && newColumn < matrix[0].Length)
            {
                bool res = find(word.Substring(1), newRow, newColumn, rowIncrement, columnIncrement);

                if (res)
                {
                    matrix[currentRow][currentColumn].Found = true;
                    return true;
                }
            }

            return false;
        }
    }
}
