using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RacingGame
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnePlayer game = new OnePlayer();
            this.Hide();
            game.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TwoPlayer game = new TwoPlayer();
            this.Hide();
            game.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Server gameServer = new Server();
        }
    }
}