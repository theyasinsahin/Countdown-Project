using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountDown_Project2_
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random(); //It will use in every random situation

            //The elements will choosen in this variable that keeps coordinates
            


            string[,] theMazeElem = new string[25, 55];

            int cursorx = random.Next(3, 54), cursory = random.Next(4, 25);   // position of cursor
            ConsoleKeyInfo cki; // required for readkey

            // --- Static screen parts

            Console.SetCursorPosition(3, 3);
            Console.WriteLine("#####################################################");
            for (int i = 0; i < 22; i++)
            {
                Console.SetCursorPosition(3, 3 + i + 1);
                Console.WriteLine("#                                                   #");
            }
            Console.SetCursorPosition(3, 25);
            Console.WriteLine("#####################################################");


            // --- Create the walls
            int[,] wallCoordinates = new int[28, 2];
            int row = 0; // Those are for set the #'s places
            int column = 0;
            bool flag = true; // This is for control the empty spaces

            int InnerWallLenght = 3; //This for loop make 20 inner wall that lenght 3
            int InnerWallPiece = 20;
            
            for (int j = 0; j < InnerWallPiece; j++)
            {
                int direction = random.Next(1, 3);
                if (direction == 1) //Horizontal
                {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 24);
                        column = random.Next(5, 52);

                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y<InnerWallLenght+1;y++)
                                {
                                    if (theMazeElem[row - 1, column + y] == "#")
                                        flag = false;
                                    if (theMazeElem[row + 1, column + y] == "#")
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row,column-1] == "#" || theMazeElem[row,column+InnerWallLenght] == "#")
                                    flag = false;
                            }
                    }
                    while (flag == false);

                    for (int k = 0; k < InnerWallLenght; k++)
                    {
                        theMazeElem[row, column + k] = "#";
                        Console.SetCursorPosition(column + k, row);
                        Console.Write("#");
                    }

                }

                else //Vertical
                {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 22);
                        column = random.Next(5, 54);
                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < InnerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row + y, column - 1] == "#")
                                        flag = false;
                                    if (theMazeElem[row + y, column + 1] == "#")
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row -1, column] == "#" || theMazeElem[row + InnerWallLenght, column] == "#")
                                    flag = false;
                            }
                    }
                    while (flag == false);

                    for (int k = 0; k < InnerWallLenght; k++)
                    {
                        theMazeElem[row + k, column] = "#";
                        Console.SetCursorPosition(column, row + k);
                        Console.Write("#");
                    }
                }
            }

            
            //This for loop make 3 inner wall that lenght 11
            InnerWallLenght = 11;
            InnerWallPiece = 3;
            for (int j = 0; j < InnerWallPiece; j++)
                {
                    int direction = random.Next(1, 3);
                    if (direction == 1) //Horizontal
                    {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 24);
                        column = random.Next(5, 44);
                        for(int k = 0; k < theMazeElem.GetLength(0); k++)
                            for(int  l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < InnerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row - 1, column + y] == "#")
                                        flag = false;
                                    if (theMazeElem[row + 1, column + y] == "#")
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row, column - 1] == "#" || theMazeElem[row, column + InnerWallLenght] == "#")
                                    flag = false;
                            }
                    }
                    while (flag == false);
                    
                        
                        for(int k = 0; k < InnerWallLenght; k++)
                        {
                            theMazeElem[row, column+k] = "#";
                            Console.SetCursorPosition(column+k, row);
                            Console.Write("#");
                        }
                        
                    }

                    else //Vertical
                    {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 14);
                        column = random.Next(5, 54);
                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < InnerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row + y, column - 1] == "#")
                                        flag = false;
                                    if (theMazeElem[row + y, column + 1] == "#")
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row - 1, column] == "#" || theMazeElem[row + InnerWallLenght, column] == "#")
                                    flag = false;
                            }
                    }
                    while (flag == false);

                        for (int k = 0; k < 11; k++)
                        {
                            theMazeElem[row+k, column] = "#";
                            Console.SetCursorPosition(column, row+k);
                            Console.Write("#");
                        }
                    }
                }
            
            //This for loop make 5 inner wall that lenght 7
            InnerWallPiece = 5;
            InnerWallLenght = 7;
            for (int j = 0; j < InnerWallPiece; j++)
            {
                int direction = random.Next(1, 3);
                if (direction == 1) //Horizontal
                {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 24);
                        column = random.Next(5, 48);
                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < InnerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row - 1, column + y] == "#")
                                        flag = false;
                                    if (theMazeElem[row + 1, column + y] == "#")
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row, column - 1] == "#" || theMazeElem[row, column + InnerWallLenght] == "#")
                                    flag = false;
                            }
                    }
                    while (flag == false);
                    flag = true;

                    for (int k = 0; k < InnerWallLenght; k++)
                    {
                        theMazeElem[row, column + k] = "#";
                        Console.SetCursorPosition(column + k, row);
                        Console.Write("#");
                    }

                }

                else //Vertical
                {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 18);
                        column = random.Next(5, 54);
                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < InnerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row + y, column - 1] == "#")
                                        flag = false;
                                    if (theMazeElem[row + y, column + 1] == "#")
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row - 1, column] == "#" || theMazeElem[row + InnerWallLenght, column] == "#")
                                    flag = false;
                            }
                    }
                    while (flag == false);
                    flag = true;

                    for (int k = 0; k < 7; k++)
                    {
                        theMazeElem[row + k, column] = "#";
                        Console.SetCursorPosition(column, row + k);
                        Console.Write("#");
                    }
                }
            }

           


            // --- Create the numbers
            int[,] numCoordinates = new int[70, 2];
            flag = true; // This is for control the coordinates of numbers
            int numx = 0;
            int numy = 0;
            for (int i = 0; i < 70; i++)
            {
                while (flag)
                {
                    flag = false;
                    numx = random.Next(4, 55);
                    numy = random.Next(4, 25);
                    for (int j = 0; j < i; j++)
                        if (theMazeElem[numy,numx] == "#" || theMazeElem[numy, numx] == "0" || theMazeElem[numy, numx] == "1" || theMazeElem[numy, numx] == "2" || theMazeElem[numy, numx] == "3" || theMazeElem[numy, numx] == "4" || theMazeElem[numy, numx] == "5" || theMazeElem[numy, numx] == "6" || theMazeElem[numy, numx] == "7" || theMazeElem[numy, numx] == "8" || theMazeElem[numy, numx] == "9")
                            flag = true;
                }
                flag = true;
                numCoordinates[i, 0] = numx;
                numCoordinates[i, 1] = numy;
                int randomNumber = random.Next(0, 10);
                theMazeElem[numy, numx] = Convert.ToString(randomNumber);

                Console.SetCursorPosition(numx, numy);
                Console.Write(randomNumber);
            }
            
            // --- Main game loop
            while (true)
            {
                if (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);       // true: do not write character 

                    
                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 54)
                    {   // key and boundary control
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory, cursorx + 1] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursorx++;

                        }
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory, cursorx - 1] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursorx--;
                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 4)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory-1, cursorx] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursory--;
                        }
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 24)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory + 1, cursorx] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursory++;
                        }
                    }
                    if (cki.KeyChar >= 97 && cki.KeyChar <= 102)
                    {       // keys: a-f 
                        Console.SetCursorPosition(50, 5);
                        Console.WriteLine("Pressed Key: " + cki.KeyChar);
                    }
                    if (cki.Key == ConsoleKey.Escape) break;
                }
                

                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position)
                Console.WriteLine("P");

                Thread.Sleep(50);     // sleep 50 ms

            }
            Console.ReadLine();
        }

    }
}
