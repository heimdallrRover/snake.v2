using System;
using System.Drawing;
using System.Windows;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace yılan
{
    class Program
    {
        static void Main(string[] args)
        {
        game:
            {

                Console.CursorVisible = (false);
                Console.Title = "Fenrir Software!";


                Console.SetWindowSize(56, 38);



                int xx = 0, yy = 2;
                int dxx = 0, dyy = 0;
                int consoleWidthLimit = 56;
                int consoleHeightLimit = 38;

                // yer hesabı




                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;  //console renklendirme
                Console.Clear();

                ConsoleColor bgColor = Console.BackgroundColor;
                ConsoleColor fgColor = Console.ForegroundColor;
                int delay = 100;
                string direction = "right";

                int snakeLength = 8;

                Random rnd = new Random();

                int score = 0;
                int x = 20;
                int y = 20;
                int colourTog = 1;           //değişkenler
                bool alive = true;
                bool pelletOn = false;
                int pelletX = 0;   //yılan parçaları x ve y
                int pelletY = 0;

                int[] xPoints;
                xPoints = new int[8] { 20, 19, 18, 17, 16, 15, 14, 13 };
                int[] yPoints;
                yPoints = new int[8] { 20, 20, 20, 20, 20, 20, 20, 20 };


                while (alive)    //yaşam durumu kontrolü 
                {
                    if (pelletOn == false)
                    {
                        bool collide = false;
                        pelletOn = true;
                        pelletX = rnd.Next(4, Console.WindowWidth - 4);
                        pelletY = rnd.Next(4, Console.WindowHeight - 4);

                        for (int l = (xPoints.Length - 1); l > 1; l--)
                        {
                            if (xPoints[l] == pelletX & yPoints[l] == pelletY)
                            {
                                collide = true;
                            }
                        }
                        if (collide == true)
                        {
                            pelletOn = false;
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(pelletX, pelletY);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.BackgroundColor = bgColor;
                            Console.Write("+");
                            pelletOn = true;
                        }

                    }
                    Array.Resize<int>(ref xPoints, snakeLength);   //yılan yem yedikçe büyüsün onun arrayları
                    Array.Resize<int>(ref yPoints, snakeLength);

                    System.Threading.Thread.Sleep(delay);
                    colourTog++;
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.D:
                                if (direction != "left")
                                {
                                    direction = "right";
                                }
                                break;
                            case ConsoleKey.A:
                                if (direction != "right")
                                {
                                    direction = "left";
                                }
                                break;
                            case ConsoleKey.W:

                                if (direction != "down")
                                {
                                    direction = "up";
                                }
                                break;
                            case ConsoleKey.S:

                                if (direction != "up")
                                {
                                    direction = "down";
                                }
                                break;
                            default:
                                break;
                        }
                    } // tuş girişine göre yönlendmrie 


                    if (direction == "right")
                    {
                        x += 1;
                    }
                    else if (direction == "left")
                    {
                        x -= 1;
                    }
                    else if (direction == "down")
                    {
                        y += 1;
                    }
                    else if (direction == "up")
                    {
                        y -= 1;
                    }

                    xPoints[0] = x;
                    yPoints[0] = y;

                    for (int l = (xPoints.Length - 1); l > 0; l--)
                    {
                        xPoints[l] = xPoints[l - 1];        //uzatma
                        yPoints[l] = yPoints[l - 1];
                    }


                    try
                    {
                        Console.SetCursorPosition(xPoints[0], yPoints[0]);
                    }
                    catch (System.ArgumentOutOfRangeException)  //çerçeve dışına çıkma durumu
                    {
                        xx += dxx;
                        if (xx > consoleWidthLimit)
                            xx = 0;
                        if (xx < 0)
                            xx = consoleWidthLimit;

                        yy += dyy;
                        if (yy > consoleHeightLimit)
                            yy = 2;
                        if (yy < 2)
                            yy = consoleHeightLimit;

                        Console.SetCursorPosition(dxx, dyy);

                    }

                    Console.ForegroundColor = fgColor;
                    Console.Write("*");

                    try
                    {
                        Console.SetCursorPosition(xPoints[xPoints.Length - 1], yPoints[yPoints.Length - 1]);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        alive = false;
                    }
                    Console.BackgroundColor = bgColor;
                    Console.Write(" ");

                    if (x == pelletX & y == pelletY)
                    {
                        pelletOn = false;
                        snakeLength += 1;   //uzama mantığı 
                        delay -= delay / 16;

                    }

                    for (int l = (xPoints.Length - 1); l > 1; l--)
                    {
                        if (xPoints[l] == xPoints[0] & yPoints[l] == yPoints[0])
                        {
                            alive = false;
                        }

                    }
                    score = ((snakeLength) - 8);
                    Console.SetCursorPosition(2, 2);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Puan: {0} ", score);

                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();



                ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

                for (int i = 0; i < 1; i++)  //kapanış ekranı 
                {
                    foreach (var color in colors)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = color;
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n");
                        Console.WriteLine("\n                       oyun bitti :(");
                        Console.WriteLine("\n\n                       skorun: {0} !", score);
                        System.Threading.Thread.Sleep(100);
                    }
                }
                Thread.Sleep(1000);
                Console.WriteLine("\n\n\n\n\n\n                    -- Başla --");
                Thread.Sleep(500);
                Console.ReadKey(true);
                Console.ReadKey(true);
                goto game;
            }
        }
    }
}


