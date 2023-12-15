using System;
using System.Threading;

namespace ConsoleApp18
{
    internal class Program
    {
        private static void PlaceWall(int x, int y, int width, int height, char[,] screen)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    screen[y + i, x + j] = '#';
                    Console.SetCursorPosition(x + j, y + i);
                    Console.Write("#");
                }
            }
        }

        private static void PlaceDigit(int x, int y, int digit, ConsoleColor color, char[,] screen)
        {
            if (IsLocationValid(x, y, 1, 1, 0, screen))
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(digit);
                screen[y, x] = (char)(digit + '0');
            }
        }

        private static bool IsLocationValid(int x, int y, int width, int height, int margin, char[,] screen)
        {
            for (int i = -margin; i < height + margin; i++)
            {
                for (int j = -margin; j < width + margin; j++)
                {
                    if (x + j < 0 || x + j >= screen.GetLength(1) || y + i < 0 || y + i >= screen.GetLength(0))
                    {
                        return false; // Geçersiz konum (ekranın dışında)
                    }

                    if (screen[y + i, x + j] == '#' || screen[y + i, x + j] >= '0' || screen[y + i, x + j] == 'P')
                    {
                        return false; // Geçersiz konum (duvar, rakam veya 'P' karakteri var)
                    }
                }
            }
            return true; // Geçerli konum
        }

        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random random = new Random();
            const string p = "P";
            int live = 5;
            int point = 0;

            DateTime timer = DateTime.Now;

            Console.Write("##################################################");

            char[,] screen = new char[23, 51];

            for (int i = 0; i < 23; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine("#");
            }

            for (int i = 0; i < 23; i++)
            {
                Console.SetCursorPosition(50, i);
                Console.Write("#");
            }

            Console.SetCursorPosition(0, 22);
            Console.Write("##################################################");

            int[] xCoordinates = new int[70];
            int[] yCoordinates = new int[70];
            int[] generatedNumbers = new int[70];
            int[] moveTimers = new int[70];

            for (int i = 0; i < 3; i++)
            {
                int wallDirection = random.Next(0, 2);
                int x;
                int y;

                if (wallDirection == 0) //yatay
                {
                    do
                    {
                        x = random.Next(2, 39);
                        y = random.Next(2, 21);
                    } while (!IsLocationValid(x, y, 11, 1, 1, screen));

                    PlaceWall(x, y, 11, 1, screen);
                }
                else //dikey
                {
                    do
                    {
                        x = random.Next(2, 48);
                        y = random.Next(2, 11);
                    } while (!IsLocationValid(x, y, 1, 11, 1, screen));

                    PlaceWall(x, y, 1, 11, screen);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                int wallDirection = random.Next(0, 2);
                int x;
                int y;
                if (wallDirection == 0)
                {
                    do
                    {
                        x = random.Next(2, 42);
                        y = random.Next(2, 21);
                    } while (!IsLocationValid(x, y, 7, 1, 1, screen));
                    PlaceWall(x, y, 7, 1, screen);
                }
                else
                {
                    do
                    {
                        x = random.Next(2, 48);
                        y = random.Next(2, 15);
                    } while (!IsLocationValid(x, y, 1, 7, 1, screen));
                    PlaceWall(x, y, 1, 7, screen);
                }
            }

            for (int i = 0; i < 20; i++)
            {
                int wallDirection = random.Next(0, 2);
                int x;
                int y;
                if (wallDirection == 0)
                {
                    do
                    {
                        x = random.Next(2, 46);
                        y = random.Next(2, 21);
                    } while (!IsLocationValid(x, y, 3, 1, 1, screen));
                    PlaceWall(x, y, 3, 1, screen);
                }
                else
                {
                    do
                    {
                        x = random.Next(2, 48);
                        y = random.Next(2, 19);
                    } while (!IsLocationValid(x, y, 1, 3, 1, screen));
                    PlaceWall(x, y, 1, 3, screen);
                }
            }

            for (int i = 0; i < 70; i++)
            {
                int x1;
                int y1;
                int digit = random.Next(0, 10);

                do
                {
                    x1 = random.Next(2, 48);
                    y1 = random.Next(2, 21);
                } while (!IsLocationValid(x1, y1, 1, 1, 0, screen));

                xCoordinates[i] = x1;
                yCoordinates[i] = y1;
                generatedNumbers[i] = digit;

                PlaceDigit(x1, y1, digit, digit == 0 ? ConsoleColor.Red : ConsoleColor.White, screen);
            }

            int px;
            int py;

            do
            {
                px = random.Next(2, 48);
                py = random.Next(2, 20);
            } while (!IsLocationValid(px, py, 1, 1, 0, screen));

            while (live > 0) // Oyun döngüsü
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    Console.SetCursorPosition(px, py);
                    Console.Write(" ");

                    int yeniPx = px;
                    int yeniPy = py;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            yeniPy = Math.Max(1, py - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            yeniPy = Math.Min(21, py + 1);
                            break;
                        case ConsoleKey.LeftArrow:
                            yeniPx = Math.Max(1, px - 1);
                            break;
                        case ConsoleKey.RightArrow:
                            yeniPx = Math.Min(49, px + 1);
                            break;
                    }

                    if (IsLocationValid(yeniPx, yeniPy, 1, 1, 0, screen))
                    {
                        px = yeniPx;
                        py = yeniPy;
                    }
                }

                DateTime endTime = DateTime.Now;
                TimeSpan elapsedTime = endTime - timer;

                Console.SetCursorPosition(90, 6);
                string formattedElapsedTime = $"Time: {(int)elapsedTime.TotalMinutes:00}.{elapsedTime.Seconds:00}";
                Console.SetCursorPosition(90, 6);
                Console.WriteLine(formattedElapsedTime);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(px, py);
                Console.WriteLine(p);
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(90, 7);
                Console.WriteLine("Live:" + live);

                for (int i = 0; i < generatedNumbers.Length; i++)
                {
                    int x1 = xCoordinates[i];
                    int y1 = yCoordinates[i];

                    if (generatedNumbers[i] == 0)
                    {
                        Console.SetCursorPosition(x1, y1);
                        Console.Write(" ");

                        int yon = random.Next(4);

                        if (elapsedTime.TotalMilliseconds > moveTimers[i])
                        {
                            switch (yon)
                            {
                                case 0:
                                    if (y1 > 1 && IsLocationValid(x1, y1 - 1, 1, 1, 0, screen))
                                    {
                                        y1--;
                                    }
                                    break;
                                case 1:
                                    if (y1 < 21 && IsLocationValid(x1, y1 + 1, 1, 1, 0, screen))
                                    {
                                        y1++;
                                    }
                                    break;
                                case 2:
                                    if (x1 > 1 && IsLocationValid(x1 - 1, y1, 1, 1, 0, screen))
                                    {
                                        x1--;
                                    }
                                    break;
                                case 3:
                                    if (x1 < 49 && IsLocationValid(x1 + 1, y1, 1, 1, 0, screen))
                                    {
                                        x1++;
                                    }
                                    break;
                            }
                            if (px == xCoordinates[i] && py == yCoordinates[i]) // P'nin olduğu yere 0 gelirse
                            {
                                live--; // Canı bir azalt
                                Console.SetCursorPosition(90, 7);
                                Console.WriteLine("Live:" + live);

                                if (live <= 0)
                                {
                                    Console.Clear(); // Ekranı temizle
                                    Console.SetCursorPosition(45, 12);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Oyun Bitti! Canınız bitti.");
                                    Console.SetCursorPosition(45, 13);
                                    Console.WriteLine("Toplam Puan: " + point);
                                    Console.SetCursorPosition(45, 14);
                                    Console.WriteLine("Çıkış yapmak için Enter tuşuna basın.");
                                    Console.ReadLine(); // Kullanıcının Enter tuşuna basmasını bekleyin ve oyunu sonlandırın
                                    return; // Oyun döngüsünden çıkın
                                }
                            }

                            moveTimers[i] = (int)elapsedTime.TotalMilliseconds + 1000; // Yeni bir hareket için 1 saniye ekle
                        }
                        
                        xCoordinates[i] = x1;
                        yCoordinates[i] = y1;

                        Console.SetCursorPosition(x1, y1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(generatedNumbers[i]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Thread.Sleep(50);
            }
        }
    }//34,4
}
