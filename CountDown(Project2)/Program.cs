using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
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

            // I did "-" all of the elements because default value is zero and we will use zero in our game
            for (int i = 0; i < theMazeElem.GetLength(0); i++)
                for (int j = 0; j < theMazeElem.GetLength(1); j++)
                {
                    theMazeElem[i, j] = "-";
                }

            int cursorx = random.Next(4, 54), cursory = random.Next(4, 25);   // position of cursor
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
                                if (theMazeElem[row - 1, column] == "#" || theMazeElem[row + InnerWallLenght, column] == "#")
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
                        theMazeElem[row + k, column] = "#";
                        Console.SetCursorPosition(column, row + k);
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
                    if (theMazeElem[numy, numx] != "-")
                        flag = true;
                }
                flag = true;
                int randomNumber = random.Next(0, 10);
                theMazeElem[numy, numx] = Convert.ToString(randomNumber);

                Console.SetCursorPosition(numx, numy);
                Console.Write(randomNumber);
            }

            Console.SetCursorPosition(60, 4);  //Those text are for just UI. We must do control those texts.
            Console.WriteLine("Time: 0");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine("Life: 5");
            Console.SetCursorPosition(60, 8);
            Console.Write("Score: 0");

            int nextCounter = 0; // this is for control is the P's next move to a number and after this number how many number in there.
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
                            nextCounter = 0;
                            while ((theMazeElem[cursory, cursorx] != "-") && cursorx < 54 && cursorx > 4 && cursory < 25 && cursory > 3 && theMazeElem[cursory, cursorx + 1] != "#")
                            {
                                nextCounter++;
                                cursorx++;
                            }

                            for (int i = 0; i < nextCounter; i++)
                            {
                                Console.SetCursorPosition(cursorx - 1, cursory);
                                Console.WriteLine(" "); // delete number (old position) on the maze
                                theMazeElem[cursory, cursorx] = theMazeElem[cursory, cursorx - 1]; // move the number next square in the array 

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(theMazeElem[cursory, cursorx]); //Write The Number
                                cursorx--; // dicrease because after out this condition we will write P
                                theMazeElem[cursory, cursorx] = "-";

                            }
                            theMazeElem[cursory, cursorx] = "-";


                        }
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory, cursorx - 1] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursorx--;
                            nextCounter = 0;
                            while ((theMazeElem[cursory, cursorx] != "-") && cursorx < 54 && cursorx > 4 && cursory < 25 && cursory > 3 && theMazeElem[cursory, cursorx - 1] != "#")
                            {
                                nextCounter++;
                                cursorx--;
                            }

                            for (int i = 0; i < nextCounter; i++)
                            {
                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" "); // delete number (old position) on the maze
                                theMazeElem[cursory, cursorx] = theMazeElem[cursory, cursorx + 1]; // move the number next square in the array 

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(theMazeElem[cursory, cursorx]); //Write The Number
                                cursorx++; // dicrease because after out this condition we will write P
                                theMazeElem[cursory, cursorx] = "-";

                            }
                            theMazeElem[cursory, cursorx] = "-";

                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 4)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory - 1, cursorx] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursory--;
                            nextCounter = 0;
                            while ((theMazeElem[cursory, cursorx] != "-") && cursorx < 54 && cursorx > 4 && cursory < 25 && cursory > 3 && theMazeElem[cursory - 1, cursorx] != "#")
                            {
                                nextCounter++;
                                cursory--;
                            }

                            for (int i = 0; i < nextCounter; i++)
                            {
                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" "); // delete number (old position) on the maze
                                theMazeElem[cursory, cursorx] = theMazeElem[cursory + 1, cursorx]; // move the number next square in the array 

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(theMazeElem[cursory, cursorx]); //Write The Number
                                cursory++; // dicrease because after out this condition we will write P
                                theMazeElem[cursory, cursorx] = "-";

                            }
                            theMazeElem[cursory, cursorx] = "-";

                        }
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 24)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (theMazeElem[cursory + 1, cursorx] != "#")
                        {
                            Console.WriteLine(" "); // delete P (old position)
                            cursory++;
                            nextCounter = 0;
                            while ((theMazeElem[cursory, cursorx] != "-") && cursorx < 54 && cursorx > 4 && cursory < 25 && cursory > 3 && theMazeElem[cursory + 1, cursorx] != "#")
                            {
                                nextCounter++;
                                cursory++;
                            }

                            for (int i = 0; i < nextCounter; i++)
                            {
                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" "); // delete number (old position) on the maze
                                theMazeElem[cursory, cursorx] = theMazeElem[cursory - 1, cursorx]; // move the number next square in the array 

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(theMazeElem[cursory, cursorx]); //Write The Number
                                cursory--; // dicrease because after out this condition we will write P
                                theMazeElem[cursory, cursorx] = "-";

                            }
                            theMazeElem[cursory, cursorx] = "-";

                        }
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
