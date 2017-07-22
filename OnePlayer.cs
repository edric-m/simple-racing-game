using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RacingGame
{
    public partial class OnePlayer : Form
    {
        string folder;
        
        //load track
        Track track;

        //Create a new car
        Car player1 = new Car(5);

        //used for pausing feature
        bool paused=false,started=false;

        //record and track best score for track
        int score = 0, best;

        public OnePlayer()
        {
            InitializeComponent();

            folder = "C:\\Documents and Settings\\Edric\\Desktop\\mon c#\\RacingGameAssignment\\RacingGame\\gameResources\\";
            track = new Track(folder + "Track.txt");

            this.DoubleBuffered = true;

            //get the best score
            using (StreamReader reader = new StreamReader(folder + "BestScore.txt"))
            {
                best = Convert.ToInt32(reader.ReadLine());
                reader.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ValidateMove();

            base.OnPaint(e);
            Graphics g = e.Graphics;

            //draw track based on car position,
            //the car in the correct position,
            //and the score
            int ctr = 500, remainder, trackRow;
            int x, y;
            for (y = 0; y < 12; y++)
            {
                trackRow = ((ctr + player1.PosY) / 50) - 2;
                remainder = (ctr + player1.PosY) % 50;
                for (x = 0; x < 9; x++)
                {
                    switch (track.array[trackRow, x])
                    {
                        case 'T':
                            g.DrawImage(track.road, new Point((x * 50), ((y * 50) + remainder - 50)));
                            break;
                        case 'G':
                            g.DrawImage(track.grass, new Point((x * 50), ((y * 50) + remainder - 50)));
                            break;
                        case 'R':
                            g.DrawImage(track.rock, new Point((x * 50), ((y * 50) + remainder - 50)));
                            break;
                        case 'E':
                            g.DrawImage(track.end, new Point((x * 50), ((y * 50) + remainder - 50)));
                            break;
                    }
                }
                ctr = ctr - 50;
            }
            g.DrawImage(player1.image, new Point((player1.posX), (400)));
            g.DrawString("Score: " + Convert.ToString(score)+"\t\t"+"Best score: "+Convert.ToString(best), new Font("Arial", 16), new SolidBrush(Color.White), 0, 0);
            score++;

            CheckForPause();
        }

        private void ValidateMove()
        {
            int a = ((player1.PosY) / 50);
            int b = (player1.posX / 50);
            try
            {
                //check if the next line is the end
                if (track.array[a, b] == 'E')
                {
                    //put the car back to the beginning
                    player1.posX = 200;
                    player1.PosY = 150;
                    //pause the game
                    paused = true;
                    //record best score if beat then reset the score
                    if (score - 1 < best)
                    {
                        best = score - 1;
                        using (StreamWriter writer = new StreamWriter(folder + "BestScore.txt"))
                        {
                            writer.WriteLine(Convert.ToString(best));
                            writer.Close();
                        }
                    }
                    score = 0;
                }
                player1.Accelerate(track.array[a, b]);
            }
            catch (IndexOutOfRangeException) { }
            //if these functions were in the same try catch the car would not move away from the side if it touches the edge
            try
            {
                player1.MoveLeft(track.array[a, b - 1], track.array[a - 1, b - 1]);
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                player1.MoveRight(track.array[a, b + 1], track.array[a - 1, b + 1]);
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (a == 3)
                    throw new IndexOutOfRangeException();
                player1.Reverse(track.array[a - 2, b]);
            }
            catch (IndexOutOfRangeException) { }
        }

        private void CheckForPause()
        {
            if (paused)
            {
                MessageBox.Show("Try not to hit rocks or drive on grass", "Paused", MessageBoxButtons.OK);
                paused = false;
                player1.up = false;
                this.Invalidate();
            }
            else
            {
                if (started == false)
                {
                    started = true;
                    paused = true;
                }
                this.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player1.up = true;
                    break;
                case Keys.Down:
                    player1.down = true;
                    break;
                case Keys.Left:
                    player1.left = true;
                    break;
                case Keys.Right:
                    player1.right = true;
                    break;
                case Keys.P:
                    paused = true;
                    break;
            }
        }

        //using key up event enables two keys to be held simulateously
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player1.up = false;
                    break;
                case Keys.Down:
                    player1.down = false;
                    break;
                case Keys.Left:
                    player1.left = false;
                    break;
                case Keys.Right:
                    player1.right = false;
                    break;
            }
        }
    }
}