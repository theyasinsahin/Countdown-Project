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

            string[,] theMazeElem = new string[30, 60];

            int cursorx = random.Next(4, 54), cursory = random.Next(4, 25);   // position of cursor
            theMazeElem[cursory, cursorx] = "P";


            // I did "-" all of the elements because default value is zero and we will use zero in our game
            for (int i = 0; i < theMazeElem.GetLength(0); i++)
                for (int j = 0; j < theMazeElem.GetLength(1); j++)
                {
                    theMazeElem[i, j] = "-";
                }

            ConsoleKeyInfo cki; // required for readkey

            // --- Static screen parts
            //This is for maze's up wall
            Console.SetCursorPosition(3, 3);
            int mazeLength = 53;
            for(int i = 0; i<mazeLength;i++)
            {
                Console.Write("#");
                theMazeElem[3,3+i]= "#";
            }
            for (int i = 0; i < 22; i++)
            {
                Console.SetCursorPosition(3, 3 + i + 1);
                Console.Write("#");
                theMazeElem[3+i+1,3] = "#";
                for (int j = 0; j < mazeLength-2; j++)
                {
                    Console.Write(" ");
                    theMazeElem[3+i+1,j+4] = "-";
                }
                Console.Write("#");
                theMazeElem[3 + i + 1, 55]="#";
            }
            Console.SetCursorPosition(3, 25);
            for (int i = 0; i < mazeLength; i++)
            {
                Console.Write("#");
                theMazeElem[25, 3 + i] = "#";
            }

            CreateInnerWall(3, 20, theMazeElem, random);
            CreateInnerWall(7, 5, theMazeElem, random);
            CreateInnerWall(11, 3, theMazeElem, random);

            // --- Create the numbers
            bool flag = true; // This is for control the coordinates of numbers
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

            Console.SetCursorPosition(60, 4);
            Console.WriteLine("Time: ");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine("Life: 5");
            Console.SetCursorPosition(60, 8);
            Console.Write("Score: ");

            DateTime timer = DateTime.Now;
            bool isMovable = true;
            int score = 0;
            int nextCounter = 0; // this is for control is the P's next move to a number and after this number how many number in there.
            // --- Main game loop
            while (true)
            {
                Console.SetCursorPosition(67, 8);
                Console.Write(score);
                Console.SetCursorPosition(66, 4);
                DateTime endTime = DateTime.Now;
                TimeSpan elapsedTime = endTime - timer;

                // Geçen sürenin sadece dakika ve saniye kısmını al
                string formattedElapsedTime = $"{(int)elapsedTime.TotalMinutes:00}:{elapsedTime.Seconds:00}";
                Console.Write(formattedElapsedTime);
                if (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);       // true: do not write character 
                    isMovable = true;
                    int number = -1; // This is for the control about is the number smashable
                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 54)
                    {   // key and boundary control
                        nextCounter = 0;
                        while ((theMazeElem[cursory, cursorx+1+nextCounter] != "-") && theMazeElem[cursory, cursorx + 1+nextCounter] != "#")
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursory, cursorx+1] != "#" && theMazeElem[cursory, cursorx + 1] != "-" && theMazeElem[cursory, cursorx + 1+nextCounter] == "#")
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    int.TryParse(theMazeElem[cursory, cursorx + nextCounter-i], out int num);
                                    if (number > num)
                                    {
                                        isMovable = false;
                                    }
                                    number = num;
                                }
                            }
                        }
                        if(isMovable){ 
                        
                        Console.SetCursorPosition(cursorx, cursory);
                            if (theMazeElem[cursory, cursorx + 1] != "#")
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursorx++;
                                nextCounter = 0;
                                while ((theMazeElem[cursory, cursorx] != "-") && theMazeElem[cursory, cursorx + 1] != "#")
                                {
                                    nextCounter++;
                                    cursorx++;
                                }

                                

                                    Console.SetCursorPosition(cursorx - 1, cursory);
                                    if (theMazeElem[cursory, cursorx+1] == "#")
                                    {
                                        if (theMazeElem[cursory, cursorx] == "1" || theMazeElem[cursory, cursorx] == "2" || theMazeElem[cursory, cursorx] == "3" || theMazeElem[cursory, cursorx] == "4")
                                            score += 2;
                                        else if (theMazeElem[cursory, cursorx] == "5" || theMazeElem[cursory, cursorx] == "6" || theMazeElem[cursory, cursorx] == "7" || theMazeElem[cursory, cursorx] == "8" || theMazeElem[cursory, cursorx] == "9")
                                            score += 1;
                                        else if (theMazeElem[cursory, cursorx] == "0")
                                            score += 20;
                                    }
                                for (int i = 0; i < nextCounter; i++)
                                {
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
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4)
                    {
                        nextCounter = 0;
                        while ((theMazeElem[cursory, cursorx - 1 - nextCounter] != "-") && theMazeElem[cursory, cursorx - 1 - nextCounter] != "#")
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursory, cursorx - 1] != "#" && theMazeElem[cursory, cursorx - 1] != "-" && theMazeElem[cursory, cursorx - 1 - nextCounter] == "#")
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    int.TryParse(theMazeElem[cursory, cursorx - nextCounter + i], out int num);
                                    if (number > num)
                                    {
                                        isMovable = false;
                                    }
                                    number = num;
                                }
                            }
                        }
                        if(isMovable) { 
                        Console.SetCursorPosition(cursorx, cursory);
                            if (theMazeElem[cursory, cursorx - 1] != "#")
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursorx--;
                                nextCounter = 0;
                                while ((theMazeElem[cursory, cursorx] != "-") && theMazeElem[cursory, cursorx - 1] != "#")
                                {
                                    nextCounter++;
                                    cursorx--;
                                }

                                    Console.SetCursorPosition(cursorx+1, cursory);
                                    if (theMazeElem[cursory, cursorx - 1] == "#")
                                    {
                                        if (theMazeElem[cursory, cursorx] == "1" || theMazeElem[cursory, cursorx - 1] == "2" || theMazeElem[cursory, cursorx] == "3" || theMazeElem[cursory, cursorx] == "4")
                                            score += 2;
                                        else if (theMazeElem[cursory, cursorx] == "5" || theMazeElem[cursory, cursorx - 1] == "6" || theMazeElem[cursory, cursorx] == "7" || theMazeElem[cursory, cursorx] == "8" || theMazeElem[cursory, cursorx] == "9")
                                            score += 1;
                                        else if (theMazeElem[cursory, cursorx] == "0")
                                            score += 20;
                                    }
                                for (int i = 0; i < nextCounter; i++)
                                {
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
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 4)
                    {
                        nextCounter = 0;
                        while ((theMazeElem[cursory - 1 - nextCounter, cursorx] != "-") && theMazeElem[cursory - 1 - nextCounter, cursorx] != "#")
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursory - 1, cursorx] != "#" && theMazeElem[cursory - 1, cursorx] != "-" && theMazeElem[cursory - 1 - nextCounter, cursorx] == "#")
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    int.TryParse(theMazeElem[cursory - nextCounter + i, cursorx], out int num);
                                    if (number > num)
                                    {
                                        isMovable = false;
                                    }
                                    number = num;
                                }
                            }
                        }
                        if(isMovable) { 
                        Console.SetCursorPosition(cursorx, cursory);
                            if (theMazeElem[cursory - 1, cursorx] != "#")
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursory--;
                                nextCounter = 0;
                                while ((theMazeElem[cursory, cursorx] != "-") && theMazeElem[cursory - 1, cursorx] != "#")
                                {
                                    nextCounter++;
                                    cursory--;
                                }

                                    Console.SetCursorPosition(cursorx, cursory+1);
                                    if (theMazeElem[cursory-1, cursorx] == "#")
                                    {
                                        if (theMazeElem[cursory, cursorx] == "1" || theMazeElem[cursory, cursorx] == "2" || theMazeElem[cursory, cursorx] == "3" || theMazeElem[cursory, cursorx] == "4")
                                            score += 2;
                                        else if (theMazeElem[cursory, cursorx] == "5" || theMazeElem[cursory, cursorx] == "6" || theMazeElem[cursory, cursorx] == "7" || theMazeElem[cursory, cursorx] == "8" || theMazeElem[cursory, cursorx] == "9")
                                            score += 1;
                                        else if (theMazeElem[cursory, cursorx] == "0")
                                            score += 20;
                                    }

                                for (int i = 0; i < nextCounter; i++)
                                {
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
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 24)
                    {
                        nextCounter = 0;
                        while ((theMazeElem[cursory + 1 + nextCounter, cursorx] != "-") && theMazeElem[cursory + 1 + nextCounter, cursorx] != "#")
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursory + 1, cursorx] != "#" && theMazeElem[cursory + 1, cursorx] != "-" && theMazeElem[cursory + 1 + nextCounter, cursorx] == "#")
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    int.TryParse(theMazeElem[cursory + nextCounter - i, cursorx], out int num);
                                    if (number > num)
                                    {
                                        isMovable = false;
                                    }
                                    number = num;
                                }
                            }
                        }
                        if(isMovable) { 
                        Console.SetCursorPosition(cursorx, cursory);
                            if (theMazeElem[cursory + 1, cursorx] != "#")
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursory++;
                                nextCounter = 0;
                                while ((theMazeElem[cursory, cursorx] != "-") && theMazeElem[cursory + 1, cursorx] != "#")
                                {
                                    nextCounter++; 
                                    cursory++;
                                }

                                
                                    Console.SetCursorPosition(cursorx, cursory-1);
                                    if (theMazeElem[cursory + 1, cursorx] == "#")
                                    {
                                        if (theMazeElem[cursory, cursorx] == "1" || theMazeElem[cursory, cursorx] == "2" || theMazeElem[cursory, cursorx] == "3" || theMazeElem[cursory, cursorx] == "4")
                                            score += 2;
                                        else if (theMazeElem[cursory, cursorx] == "5" || theMazeElem[cursory, cursorx] == "6" || theMazeElem[cursory, cursorx] == "7" || theMazeElem[cursory, cursorx] == "8" || theMazeElem[cursory, cursorx] == "9")
                                            score += 1;
                                        else if (theMazeElem[cursory, cursorx] == "0")
                                            score += 20;
                                    }
                                for (int i = 0; i < nextCounter; i++)
                                {
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
                    }

                    if (cki.Key == ConsoleKey.Escape) break;
                }


                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position)
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("P");
                Console.ResetColor();


                Thread.Sleep(50);     // sleep 50 ms

            }
            Console.ReadLine();
        }



        // --- Create the walls
        static void CreateInnerWall(int InnerWallLenght, int InnerWallPiece, string[,] theMazeElem,Random random)
        {
            for (int j = 0; j < InnerWallPiece; j++)
            {
                int row = 0; // Those are for set the #'s places
                int column = 0;
                bool flag = true; // This is for control the empty spaces
                int direction = random.Next(1, 3);
                if (direction == 1) //Horizontal
                {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 24);
                        column = random.Next(5, 55-InnerWallLenght);

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
                                    if (theMazeElem[row, column + y] == "#" || theMazeElem[row, column + y] == "P")
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
                        row = random.Next(5, 25-InnerWallLenght);
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
                                    if (theMazeElem[row + y, column] == "#" || theMazeElem[row + y, column] == "P")
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
        }

    }
}