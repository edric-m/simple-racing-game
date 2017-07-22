using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace RacingGame
{
    public class Car
    {
        public int posX;
        public int PosY;
        public int speed;
        public bool up,down,left,right;
        public int topSpeed;
        private string folder;
        public Bitmap image;

        public Car(int a_topSpeed)
        {
            folder = "C:\\Documents and Settings\\Edric\\My Documents\\mon c#\\RacingGameAssignment\\RacingGame\\gameResources\\";
            image = new Bitmap(folder + "car.bmp");
            posX = 200;
            PosY = 150;
            speed = 0;
            up = false;
            down = false;
            left = false;
            right = false;
            topSpeed = a_topSpeed;
        }

        public void Accelerate(char frontTile)
        {
            if (up)
            {
                switch (frontTile)
                {
                    case 'T':
                        speed = topSpeed;
                        break;
                    case 'R':
                        speed = 0;
                        break;
                    case 'G':
                        speed = topSpeed / 2;
                        break;
                }
                PosY = PosY + speed;
            }
        }

        public void Reverse(char backTile)
        {
            if (down)
            {
                if (backTile != 'R')
                {
                    PosY = PosY - 1;
                }
            }
        }

        public void MoveLeft(char lf, char lb)
        {
            if (left)
            {
                switch (speed)
                {
                    case 0:
                        if (lb != 'R')
                        {
                            posX = posX - 5;
                        }
                        break;
                    default:
                        if (lf != 'R' && lb != 'R')
                        {
                            posX = posX - 5;
                        }
                        break;
                }
            }
        }

        public void MoveRight(char rf, char rb)
        {
            if (right)
            {
                switch (speed)
                {
                    case 0:
                        if (rb != 'R')
                        {
                            posX = posX + 5;
                        }
                        break;
                    default:
                        if (rf != 'R' && rb != 'R')
                        {
                            posX = posX + 5;
                        }
                        break;
                }
            }
        }
    }
    public class Track
    {
        private string folder;
        public char[,] array = new char[100, 9];
        public Bitmap road;
        public Bitmap grass;
        public Bitmap rock;
        public Bitmap end;

        public Track(string a_trackPath)
        {
            folder = "C:\\Documents and Settings\\Edric\\Desktop\\mon c#\\RacingGameAssignment\\RacingGame\\gameResources\\";
            road = new Bitmap(folder + "road.bmp");
            grass = new Bitmap(folder + "grass.bmp");
            rock = new Bitmap(folder + "rock.bmp");
            end = new Bitmap(folder + "end.bmp");

            string line;
            int x = 0;
            using (StreamReader reader = new StreamReader(a_trackPath))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    array[x, 0] = line[0];
                    array[x, 1] = line[1];
                    array[x, 2] = line[2];
                    array[x, 3] = line[3];
                    array[x, 4] = line[4];
                    array[x, 5] = line[5];
                    array[x, 6] = line[6];
                    array[x, 7] = line[7];
                    array[x, 8] = line[8];
                    x++;
                }
                reader.Close();
            }
        }
    }
}
