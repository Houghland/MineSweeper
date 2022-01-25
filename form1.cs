using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HoughlandLaurenA3
{
    public partial class Form1 : Form
    {
        int totalLoss = 0;
        int Wins = 0;
        int spaces = 0;
        int totalMines = 10;
        int runtime = 0;
        public Cell[,] grid = new Cell[10, 10];
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += OnTimer_Tick;
            CreateGrid();
            PlacedMines();
            setNumbers();
        }

        private void CreateGrid()
        {
            for (int row = 0; row < grid.GetLength(1); row++)
            {
                for (int col = 0; col < grid.GetLength(0); col++)
                {
                    Cell temp = new Cell();
                    temp.X = row;
                    temp.Y = col;
                    temp.Location = new Point(col * temp.Size.Width, row * temp.Size.Height);
                    temp.MyColor = (rand.Next(2) % 2 == 0) ? Color.MediumOrchid : Color.DarkSeaGreen;
                    temp.OnCellClick += OnCellClick;
                    this.Controls.Add(temp);
                    grid[col, row] = temp;

                }
            }

        }
        public void PlacedMines()
        {
            int minesPlaced = 0;

            while (minesPlaced < totalMines)
            {
                int x = rand.Next(grid.GetLength(0));
                int y = rand.Next(grid.GetLength(1));
                if (grid[x, y].MyColor != Color.Red)
                {
                    grid[x, y].MyColor = Color.Red;
                    grid[x, y].MyLabel.BackColor = Color.Red;
                    grid[x, y].MyLabel.Text = "M";
                    minesPlaced++;
                }
            }
        }
        public void setNumbers()
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (grid[i, j].MyLabel.Text != "M")
                    {
                        int count = 0;
                        //left
                        if (i - 1 >= 0) { if (grid[i - 1, j].MyLabel.Text == "M") { count++; } }
                        //bottom left
                        if (i - 1 >= 0 && j + 1 < grid.GetLength(1)) { if (grid[i - 1, j + 1].MyLabel.Text == "M") { count++; } }
                        //down
                        if (j + 1 < grid.GetLength(1)) { if (grid[i, j + 1].MyLabel.Text == "M") { count++; } }
                        //bottom right
                        if (i + 1 < grid.GetLength(1) && j + 1 < grid.GetLength(1)) { if (grid[i + 1, j + 1].MyLabel.Text == "M") { count++; } }

                        //top left
                        if (i - 1 >= 0 && j - 1 >= 0) { if (grid[i - 1, j - 1].MyLabel.Text == "M") { count++; } }

                        //right
                        if (i + 1 < grid.GetLength(1)) { if (grid[i + 1, j].MyLabel.Text == "M") { count++; } }

                        //top right
                        if (i + 1 < grid.GetLength(1) && j - 1 >= 0) { if (grid[i + 1, j - 1].MyLabel.Text == "M") { count++; } }

                        //up
                        if (j - 1 >= 0) { if (grid[i, j - 1].MyLabel.Text == "M") { count++; } }

                        grid[i, j].MyLabel.Text = $"{count}";

                    }
                }
            }
        }

        public void OnCellClick(object sender, EventArgs e)
        {
            
            timer1.Start();
            
            Color targetColor = ((Cell)sender).MyColor;
            timer1.Start();
            int row = ((Cell)sender).X;
            int col = ((Cell)sender).Y;
            if (grid[col, row].MyLabel.Text == "0")
            {
                CheckLeft(row, col);
                CheckRight(row, col);
                CheckDown(row, col);
                CheckUp(row, col);
                CheckTopLeft(row, col);
                CheckTopRight(row, col);
                CheckLowerLeft(row, col);
                CheckLowerRight(row, col);
            }
            else if(grid[col, row].MyLabel.Text == "M")
            {
                timer1.Stop();
                MessageBox.Show("nope");
                totalLoss++;
                
                Application.Restart();
            }
            spaces++;
            Win();
        }
        

        


        public void Win()
        {
            if (spaces == grid.GetLength(0) * grid.GetLength(1) - totalMines)
            {
                timer1.Stop();
                MessageBox.Show("yeet");
                Wins++;
                
                Application.Restart();
            }
        }

        
        public void OnTimer_Tick(object sender, EventArgs e)
        {
            runtime++;
            timerLbl.Text = $"Timer: {runtime}";
           
        }

        #region CheckMines
        private void CheckTopLeft(int row, int col)
        {
            if (row - 1 >= 0 && col + 1 < grid.GetLength(0))
            {
                if (grid[col + 1, row - 1].MyLabel.Text != "M")
                {
                    grid[col + 1, row - 1].MyButton.PerformClick();
                }
            }
        }

        private void CheckTopRight(int row, int col)
        {
            if (row + 1 < grid.GetLength(1) && col + 1 < grid.GetLength(0))
            {
                if (grid[col + 1, row + 1].MyLabel.Text != "M")
                {
                    grid[col + 1, row + 1].MyButton.PerformClick();
                }
            }
        }

        private void CheckDown(int row, int col)
        {
            if (row - 1 >= 0)
            {
                if (grid[col, row - 1].MyLabel.Text != "M")
                {
                    grid[col, row - 1].MyButton.PerformClick();

                }
            }
        }

        private void CheckUp(int row, int col)
        {
            if (row + 1 < grid.GetLength(1))
            {
                if (grid[col, row + 1].MyLabel.Text != "M")
                {
                    grid[col, row + 1].MyButton.PerformClick();
                }
            }
        }

        private void CheckLowerRight(int row, int col)
        {
            if (row + 1 < grid.GetLength(1) && col - 1 >= 0)
            {
                if (grid[col, row + 1].MyLabel.Text != "M")
                {
                    grid[col - 1, row + 1].MyButton.PerformClick();
                }
            }
        }

        private void CheckLowerLeft(int row, int col)
        {
            if (row - 1 >= 0 && col - 1 >= 0)
            {
                if (grid[col - 1, row - 1].MyLabel.Text != "M")
                {
                    grid[col - 1, row - 1].MyButton.PerformClick();
                }
            }
        }

        private void CheckRight(int row, int col)
        {
            if (col < grid.GetLength(0) - 1)
            {
                if (grid[col + 1, row].MyLabel.Text != "M")
                {
                    grid[col + 1, row].MyButton.PerformClick();
                }
            }
        }

        private void CheckLeft(int row, int col)
        {
            if (col > 0)
            {
                if (grid[col - 1, row].MyLabel.Text != "M")
                {
                    grid[col - 1, row].MyButton.PerformClick();
                }
            }
        }
        #endregion



        private void instructionsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("The purpose of the game is to open all the cells of the board which do not contain a bomb. You lose if you set off a bomb cell. Every non - bomb cell you open will tell you the total number of bombs in the neighboring cells. ");
        }

        private void quitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }


        

        private void restartToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
    
}
