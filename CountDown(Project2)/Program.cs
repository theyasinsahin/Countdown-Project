using System;
using System.Runtime.CompilerServices;
using System.Threading;


namespace CountDown_Project2_
{
    internal static class Program
    {
        static Random random = new Random(); //It will use in every random situation

        static int cursorX = random.Next(4, 54), cursorY = random.Next(4, 25);   // first position of cursor
        static char[,] theMazeElem = new char[30, 60];
        static bool flag = true; // This is for control of everything
        static int number = '0'; // This is for the control about is the number smashable



        public static void Main(string[] args)
        {

            const int MAZE_LENGTH = 53;


            //The elements will chosen in this variable that keeps coordinates


            // I did "-" all of the elements because default value is zero and we will use zero in our game
            for (int i = 0; i < theMazeElem.GetLength(0); i++)
                for (int j = 0; j < theMazeElem.GetLength(1); j++)
                {
                    theMazeElem[i, j] = '-';
                }

            
            theMazeElem[cursorY, cursorX] = 'P';
            // --- Static screen parts
            //This is for maze's up wall
            Console.SetCursorPosition(3, 3);
            for (int i = 0; i < MAZE_LENGTH; i++)
            {
                Console.Write("#");
                theMazeElem[3, 3 + i] = '#';
            }

            for (int i = 0; i < 22; i++)
            {
                Console.SetCursorPosition(3, 3 + i + 1);
                Console.Write("#");
                theMazeElem[3 + i + 1, 3] = '#';
                for (int j = 0; j < MAZE_LENGTH - 2; j++)
                {
                    Console.Write(" ");
                    theMazeElem[3 + i + 1, j + 4] = '-';
                }
                Console.Write("#");
                theMazeElem[3 + i + 1, 55] = '#';
            }

            Console.SetCursorPosition(3, 25);
            for (int i = 0; i < MAZE_LENGTH; i++)
            {
                Console.Write("#");
                theMazeElem[25, 3 + i] = '#';
            }

            CreateInnerWall(3, 20);
            CreateInnerWall(7, 5);
            CreateInnerWall(11, 3);

            // --- Create the numbers
            int numX = 0;
            int numY = 0;

            int[,] zeroCoordinates = new int[70, 2];
            int zeroXcounter = 0;

            for (int i = 0; i < 70; i++)
            {
                while (flag)
                {
                    flag = false;
                    numX = random.Next(4, 55);
                    numY = random.Next(4, 25);
                    if (theMazeElem[numY, numX] != '-')
                        flag = true;
                }
                flag = true;
                int randomNumber = random.Next(0, 10);

                if (randomNumber == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    zeroCoordinates[zeroXcounter, 0] = numX;
                    zeroCoordinates[zeroXcounter, 1] = numY;
                    zeroXcounter++;
                }
                theMazeElem[numY, numX] = Convert.ToChar(randomNumber+'0');

                Console.SetCursorPosition(numX, numY);
                Console.Write(randomNumber);
                Console.ResetColor();

            }

            Console.SetCursorPosition(60, 4);
            Console.WriteLine("Time: ");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine("Life: ");
            Console.SetCursorPosition(60, 8);
            Console.Write("Score: ");

            DateTime timer = DateTime.Now;
            int score = 0;
            int life = 5;
            int timerForZero = 0;
            bool isDone = true;
            // --- Main game loop --- ////////////////////////////////////////////
            while (true)
            {
                Console.SetCursorPosition(67, 8);
                Console.Write(score);
                Console.SetCursorPosition(67, 6);
                Console.Write(life);
                Console.SetCursorPosition(66, 4);
                DateTime endTime = DateTime.Now;
                TimeSpan elapsedTime = endTime - timer;

                // Get only the minute and second parts of the time passed.
                string formattedElapsedTime = $"{(int)elapsedTime.TotalMinutes:00}:{elapsedTime.Seconds:00}";

                Console.Write(formattedElapsedTime);

                // Numbers Decraese
                if (elapsedTime.Seconds % 15 == 1)
                    isDone = true;
                if (elapsedTime.Seconds % 15 == 0 && isDone == true && elapsedTime.Seconds > 0)
                {
                    for (int i = 0; i < theMazeElem.GetLength(0); i++)
                        for (int j = 0; j < theMazeElem.GetLength(1); j++)
                        {
                            switch (theMazeElem[i, j])
                            {
                                case '2':
                                case '3':
                                case '4':
                                case '5':
                                case '6':
                                case '7':
                                case '8':
                                case '9':
                                    theMazeElem[i, j] = Convert.ToChar(theMazeElem[i,j] - 1);
                                    break;
                                default:
                                    continue;
                            }
                            Console.SetCursorPosition(j, i);
                            Console.Write(theMazeElem[i, j]);

                            if (theMazeElem[i, j] == '1')
                            {
                                int possibility = random.Next(1, 101);
                                if (possibility < 4)
                                {
                                    theMazeElem[i, j] = '0';
                                    zeroCoordinates[zeroXcounter, 0] = numX;
                                    zeroCoordinates[zeroXcounter, 1] = numY;
                                    zeroXcounter++;
                                    Console.SetCursorPosition(j, i);
                                    Console.Write('0');
                                }
                            }
                        }
                    isDone = false;

                }


                if (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer
                    ConsoleKeyInfo cki = Console.ReadKey(true); // required for reading key
                    bool isMovable = true;
                    var nextCounter = 0; // this is for control is the P's next move to a number and after this number how many number in there.
                    if (cki.Key == ConsoleKey.RightArrow && cursorX < 54)
                    {   // key and boundary control
                        nextCounter = 0;
                        while ((theMazeElem[cursorY, cursorX + 1 + nextCounter] != '-') && theMazeElem[cursorY, cursorX + 1 + nextCounter] != '#')
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursorY, cursorX + 1] != '#' && theMazeElem[cursorY, cursorX + 1] != '-' && theMazeElem[cursorY, cursorX + 1 + nextCounter] == '#')
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    if (number > theMazeElem[cursorY, cursorX + nextCounter + i])
                                    {
                                        isMovable = false;
                                    }
                                    number = theMazeElem[cursorY, cursorX + nextCounter + i];
                                }
                            }
                        }
                        if (isMovable)
                        {
                            Console.SetCursorPosition(cursorX, cursorY);
                            if (theMazeElem[cursorY, cursorX + 1] != '#')
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursorX++;
                                if (theMazeElem[cursorY, cursorX] == '0')
                                {
                                    life--;
                                    if (life == 0)
                                    {
                                        Console.Clear();
                                        Console.Write("Game Over");
                                        break;
                                    }
                                }
                                nextCounter = 0;
                                while (theMazeElem[cursorY, cursorX] != '-' && theMazeElem[cursorY, cursorX + 1] != '#')
                                {
                                    nextCounter++;
                                    cursorX++;
                                }

                                Console.SetCursorPosition(cursorX - 1, cursorY);

                                if (theMazeElem[cursorY, cursorX + 1] == '#')
                                {
                                    switch (theMazeElem[cursorY, cursorX])
                                    {
                                        case '1':
                                        case '2':
                                        case '3':
                                        case '4':
                                            score += 2;
                                            break;
                                        case '5':
                                        case '6':
                                        case '7':
                                        case '8':
                                        case '9':
                                            score += 1;
                                            break;
                                        case '0':
                                            score += 20;
                                            break;
                                    }
                                }

                                for (int i = 0; i < nextCounter; i++)
                                {
                                    Console.WriteLine(" "); // delete number (old position) on the maze
                                    theMazeElem[cursorY, cursorX] = theMazeElem[cursorY, cursorX - 1]; // move the number next square in the array 

                                    Console.SetCursorPosition(cursorX, cursorY);
                                    if (theMazeElem[cursorY, cursorX] == '0')
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(theMazeElem[cursorY, cursorX]); //Write The Number
                                    Console.ResetColor();
                                    cursorX--; // decrease because after out this condition we will write P
                                    theMazeElem[cursorY, cursorX] = '-';

                                }
                                theMazeElem[cursorY, cursorX] = '-';

                            }
                        }
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorX > 4)
                    {
                        nextCounter = 0;
                        while ((theMazeElem[cursorY, cursorX - 1 - nextCounter] != '-') && theMazeElem[cursorY, cursorX - 1 - nextCounter] != '#')
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursorY, cursorX - 1] != '#' && theMazeElem[cursorY, cursorX - 1] != '-' && theMazeElem[cursorY, cursorX - 1 - nextCounter] == '#')
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    if (number > theMazeElem[cursorY, cursorX - nextCounter + i])
                                    {
                                        isMovable = false;
                                    }
                                    number = theMazeElem[cursorY, cursorX - nextCounter + i];
                                }
                            }
                        }
                        if (isMovable)
                        {
                            Console.SetCursorPosition(cursorX, cursorY);
                            if (theMazeElem[cursorY, cursorX - 1] != '#')
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursorX--;
                                if (theMazeElem[cursorY, cursorX] == '0')
                                {
                                    life--;
                                    if (life == 0)
                                    {
                                        Console.Clear();
                                        Console.Write("Game Over");
                                        break;
                                    }
                                }
                                nextCounter = 0;
                                while ((theMazeElem[cursorY, cursorX] != '-') && theMazeElem[cursorY, cursorX - 1] != '#')
                                {
                                    nextCounter++;
                                    cursorX--;
                                }

                                Console.SetCursorPosition(cursorX + 1, cursorY);
                                if (theMazeElem[cursorY, cursorX - 1] == '#')
                                { 
                                    switch (theMazeElem[cursorY, cursorX])
                                    {
                                        case '1':
                                        case '2':
                                        case '3':
                                        case '4':
                                            score += 2;
                                            break;
                                        case '5':
                                        case '6':
                                        case '7':
                                        case '8':
                                        case '9':
                                            score += 1;
                                            break;
                                        case '0':
                                            score += 20;
                                            break;
                                    }
                                }
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    Console.WriteLine(" "); // delete number (old position) on the maze
                                    theMazeElem[cursorY, cursorX] = theMazeElem[cursorY, cursorX + 1]; // move the number next square in the array 

                                    if (theMazeElem[cursorY, cursorX] == '0')
                                        Console.ForegroundColor = ConsoleColor.Red;

                                    Console.SetCursorPosition(cursorX, cursorY);
                                    Console.WriteLine(theMazeElem[cursorY, cursorX]); //Write The Number
                                    Console.ResetColor();

                                    cursorX++; // decrease because after out this condition we will write P
                                    theMazeElem[cursorY, cursorX] = '-';
                                }

                                theMazeElem[cursorY, cursorX] = '-';
                            }
                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursorY > 4)
                    {
                        nextCounter = 0;
                        while ((theMazeElem[cursorY - 1 - nextCounter, cursorX] != '-') && theMazeElem[cursorY - 1 - nextCounter, cursorX] != '#')
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursorY - 1, cursorX] != '#' && theMazeElem[cursorY - 1, cursorX] != '-' && theMazeElem[cursorY - 1 - nextCounter, cursorX] == '#')
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    if (number > theMazeElem[cursorY - nextCounter + i, cursorX])
                                    {
                                        isMovable = false;
                                    }
                                    number = theMazeElem[cursorY - nextCounter + i, cursorX];
                                }
                            }
                        }
                        if (isMovable)
                        {
                            Console.SetCursorPosition(cursorX, cursorY);
                            if (theMazeElem[cursorY - 1, cursorX] != '#')
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursorY--;
                                if (theMazeElem[cursorY, cursorX] == '0')
                                {
                                    life--;
                                    if (life == 0)
                                    {
                                        Console.Clear();
                                        Console.Write("Game Over");
                                        break;
                                    }
                                }
                                nextCounter = 0;
                                while ((theMazeElem[cursorY, cursorX] != '-') && theMazeElem[cursorY - 1, cursorX] != '#')
                                {
                                    nextCounter++;
                                    cursorY--;
                                }

                                Console.SetCursorPosition(cursorX, cursorY + 1);
                                if (theMazeElem[cursorY - 1, cursorX] == '#')
                                {
                                    switch (theMazeElem[cursorY, cursorX])
                                    {
                                        case '1':
                                        case '2':
                                        case '3':
                                        case '4':
                                            score += 2;
                                            break;
                                        case '5':
                                        case '6':
                                        case '7':
                                        case '8':
                                        case '9':
                                            score += 1;
                                            break;
                                        case '0':
                                            score += 20;
                                            break;
                                    }
                                }

                                for (int i = 0; i < nextCounter; i++)
                                {
                                    Console.WriteLine(" "); // delete number (old position) on the maze
                                    theMazeElem[cursorY, cursorX] = theMazeElem[cursorY + 1, cursorX]; // move the number next square in the array 

                                    if (theMazeElem[cursorY, cursorX] == '0')
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(cursorX, cursorY);
                                    Console.WriteLine(theMazeElem[cursorY, cursorX]); //Write The Number
                                    Console.ResetColor();

                                    cursorY++; // decrease because after out this condition we will write P
                                    theMazeElem[cursorY, cursorX] = '-';

                                }
                                theMazeElem[cursorY, cursorX] = '-';
                            }
                        }
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursorY < 24)
                    {
                        nextCounter = 0;
                        while ((theMazeElem[cursorY + 1 + nextCounter, cursorX] != '-') && theMazeElem[cursorY + 1 + nextCounter, cursorX] != '#')
                        {
                            nextCounter++;
                        }
                        if (theMazeElem[cursorY + 1, cursorX] != '#' && theMazeElem[cursorY + 1, cursorX] != '-' && theMazeElem[cursorY + 1 + nextCounter, cursorX] == '#')
                        {
                            isMovable = true;
                            if (nextCounter == 1)
                                isMovable = false;
                            else
                            {
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    if (number > theMazeElem[cursorY - nextCounter + i, cursorX])
                                    {
                                        isMovable = false;
                                    }
                                    number = theMazeElem[cursorY - nextCounter + i, cursorX];
                                }
                            }
                        }
                        if (isMovable)
                        {
                            Console.SetCursorPosition(cursorX, cursorY);
                            if (theMazeElem[cursorY + 1, cursorX] != '#')
                            {
                                Console.WriteLine(" "); // delete P (old position)
                                cursorY++;
                                if (theMazeElem[cursorY, cursorX] == '0')
                                {
                                    life--;
                                    if (life == 0)
                                    {
                                        Console.Clear();
                                        Console.Write("Game Over");
                                        break;
                                    }
                                }
                                nextCounter = 0;
                                while ((theMazeElem[cursorY, cursorX] != '-') && theMazeElem[cursorY + 1, cursorX] != '#')
                                {
                                    nextCounter++;
                                    cursorY++;
                                }


                                Console.SetCursorPosition(cursorX, cursorY - 1);
                                if (theMazeElem[cursorY + 1, cursorX] == '#')
                                {
                                    switch (theMazeElem[cursorY, cursorX])
                                    {
                                        case '1':
                                        case '2':
                                        case '3':
                                        case '4':
                                            score += 2;
                                            break;
                                        case '5':
                                        case '6':
                                        case '7':
                                        case '8':
                                        case '9':
                                            score += 1;
                                            break;
                                        case '0':
                                            score += 20;
                                            break;
                                    }
                                }
                                for (int i = 0; i < nextCounter; i++)
                                {
                                    Console.WriteLine(" "); // delete number (old position) on the maze
                                    theMazeElem[cursorY, cursorX] = theMazeElem[cursorY - 1, cursorX]; // move the number next square in the array 

                                    if (theMazeElem[cursorY, cursorX] == '0')
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(cursorX, cursorY);
                                    Console.WriteLine(theMazeElem[cursorY, cursorX]); // write The Number
                                    Console.ResetColor();

                                    cursorY--; // decrease because after out this condition we will write P
                                    theMazeElem[cursorY, cursorX] = '-';

                                }
                                theMazeElem[cursorY, cursorX] = '-';
                            }
                        }
                    }

                    if (cki.Key == ConsoleKey.Escape) break;
                }


                Console.SetCursorPosition(cursorX, cursorY);    // refresh X (current position)
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("P");
                Console.ResetColor();

                Thread.Sleep(50);     // sleep 50 ms

                timerForZero++; // Thread.Sleep(50) kodunu kullandığımız için her döngünün işlem süresini dikkate almadan her döngünün süresini 50ms olarak düşündüm ve her döngünün sonunda artan bu değişken 20 olduğunda aslında yaklaşık bir saniye geçmiş olacak.
                int zeroDirection = 0;
                if (timerForZero % 20 == 0)
                {
                    for (int i = 0; zeroCoordinates[i, 0] != 0; i++)
                    {
                        flag = true;
                        do
                        {
                            zeroDirection = random.Next(1, 5);
                            if (zeroCoordinates[i, 0] < 54 && zeroDirection == 1 && theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0] + 1] == '-')
                                flag = false;

                            else if (zeroCoordinates[i, 0] > 4 && zeroDirection == 2 && theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0] - 1] == '-')
                                flag = false;

                            else if (zeroCoordinates[i, 1] < 24 && zeroDirection == 3 && theMazeElem[zeroCoordinates[i, 1] + 1, zeroCoordinates[i, 0]] == '-')
                                flag = false;

                            else if (zeroCoordinates[i, 1] > 4 && zeroDirection == 4 && theMazeElem[zeroCoordinates[i, 1] - 1, zeroCoordinates[i, 0]] == '-')
                                flag = false;
                        }
                        while (flag);
                        Console.ForegroundColor = ConsoleColor.Red;
                        switch (zeroDirection)
                        {
                            case 1:
                                theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0]] = '-';
                                theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0] + 1] = '0';
                                Console.SetCursorPosition(zeroCoordinates[i, 0], zeroCoordinates[i, 1]);
                                Console.Write(" ");
                                Console.SetCursorPosition(zeroCoordinates[i, 0] + 1, zeroCoordinates[i, 1]);
                                Console.Write("0");
                                zeroCoordinates[i, 0] += 1;
                                break;
                            case 2:
                                theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0]] = '-';
                                theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0] - 1] = '0';
                                Console.SetCursorPosition(zeroCoordinates[i, 0], zeroCoordinates[i, 1]);
                                Console.Write(" ");
                                Console.SetCursorPosition(zeroCoordinates[i, 0] - 1, zeroCoordinates[i, 1]);
                                Console.Write("0");
                                zeroCoordinates[i, 0] -= 1;
                                break;
                            case 3:
                                theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0]] = '-';
                                theMazeElem[zeroCoordinates[i, 1] + 1, zeroCoordinates[i, 0]] = '0';
                                Console.SetCursorPosition(zeroCoordinates[i, 0], zeroCoordinates[i, 1]);
                                Console.Write(" ");
                                Console.SetCursorPosition(zeroCoordinates[i, 0], zeroCoordinates[i, 1] + 1);
                                Console.Write("0");
                                zeroCoordinates[i, 1] += 1;
                                break;
                            case 4:
                                theMazeElem[zeroCoordinates[i, 1], zeroCoordinates[i, 0]] = '-';
                                theMazeElem[zeroCoordinates[i, 1] - 1, zeroCoordinates[i, 0]] = '0';
                                Console.SetCursorPosition(zeroCoordinates[i, 0], zeroCoordinates[i, 1]);
                                Console.Write(" ");
                                Console.SetCursorPosition(zeroCoordinates[i, 0], zeroCoordinates[i, 1] - 1);
                                Console.Write("0");
                                zeroCoordinates[i, 1] -= 1;
                                break;
                        }
                        Console.ResetColor();
                    }
                }







            }

            Console.ReadLine();
        }



        // --- Create the walls
        static void CreateInnerWall(int innerWallLenght, int innerWallPiece)
        {
            for (int j = 0; j < innerWallPiece; j++)
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
                        column = random.Next(5, 55 - innerWallLenght);

                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < innerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row - 1, column + y] == '#')
                                        flag = false;
                                    if (theMazeElem[row + 1, column + y] == '#')
                                        flag = false;
                                    if (theMazeElem[row, column + y] == '#' || theMazeElem[row, column + y] == 'P')
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row, column - 1] == '#' || theMazeElem[row, column + innerWallLenght] == '#')
                                    flag = false;
                            }
                    }
                    while (flag == false);

                    for (int k = 0; k < innerWallLenght; k++)
                    {
                        theMazeElem[row, column + k] = '#';
                        Console.SetCursorPosition(column + k, row);
                        Console.Write("#");
                    }

                }

                else //Vertical
                {
                    do
                    {
                        flag = true;
                        row = random.Next(5, 25 - innerWallLenght);
                        column = random.Next(5, 54);
                        for (int k = 0; k < theMazeElem.GetLength(0); k++)
                            for (int l = 0; l < theMazeElem.GetLength(1); l++)
                            {
                                //Control The #'s up and down
                                for (int y = -1; y < innerWallLenght + 1; y++)
                                {
                                    if (theMazeElem[row + y, column - 1] == '#')
                                        flag = false;
                                    if (theMazeElem[row + y, column + 1] == '#')
                                        flag = false;
                                    if (theMazeElem[row + y, column] == '#' || theMazeElem[row + y, column] == 'p')
                                        flag = false;
                                }
                                // Control the #'s forward and backward
                                if (theMazeElem[row - 1, column] == '#' || theMazeElem[row + innerWallLenght, column] == '#')
                                    flag = false;
                            }
                    }
                    while (flag == false);

                    for (int k = 0; k < innerWallLenght; k++)
                    {
                        theMazeElem[row + k, column] = '#';
                        Console.SetCursorPosition(column, row + k);
                        Console.Write("#");
                    }
                }
            }
        }

        static void PlayerMovements()
        {

        }
    }
}