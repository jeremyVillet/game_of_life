using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Of_Life_Interface
{
    class Game_Logic
    {

        private readonly int Size;
        public int[,] Board { get; set; }
        public int[,] Board_Temp { get; set; }
        public Game_Logic(int size)
        {
            this.Size = size;
            this.Board = new int[Size, Size];
            this.Board_Temp = new int[Size, Size];

            StreamReader file_map = new StreamReader("paternpulsar.txt");
            string line;
            int x = 0;
            while ((line = file_map.ReadLine()) != null)
            {
                
                for (int y = 0; y < line.Length; y++)
                {
                    if (line[y] == '1')
                    {
                        this.Board_Temp[x, y] = 1;
                     
                    }

                }
                x++;
            }

            file_map.Close();
            DeepCopy();

        }

        public void PrintBoard()
        {
            for (int x = 0; x < this.Size; x++)
            {
                for (int y = 0; y < this.Size; y++)
                {
                    Console.Write($"{this.Board[x, y]} ");
                }
                Console.WriteLine();
            }
        }
        public void PassGeneration()
        {
            for (int x = 0; x < this.Size; x++)
            {
                for (int y = 0; y < this.Size; y++)
                {
                    Check_Cellule(x, y);
                }

            }
            DeepCopy();
        }

        private void Check_Cellule(int i, int j)
        {
            int cell_adj_alive = 0;
            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    if (y >= 0 && y < this.Size && x >= 0 && x < this.Size && !(x == i && y == j) && Board[x, y] == 1)
                    {
                        cell_adj_alive++;
                    }
                }
            }
      
            if (cell_adj_alive == 3 && this.Board[i, j] == 0)
            {
                this.Board_Temp[i, j] = 1;
            }
            else if ((cell_adj_alive == 3 || cell_adj_alive == 2) && this.Board[i, j] == 1)
            {
                this.Board_Temp[i, j] = 1;
            }
            else
            {
                this.Board_Temp[i, j] = 0;
            }
        }

        public void DeepCopy()
        {
            for (int x = 0; x < this.Size; x++)
            {
                for (int y = 0; y < this.Size; y++)
                {
                    this.Board[x, y] = this.Board_Temp[x, y];
                }
            }
        }
    }

}
