using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;

namespace OthelloWinFormGame
{
    public partial class Form1 : Form
    {
        //private readonly TableLayoutPanel m_TableLayoutPanel = new TableLayoutPanel();
        private readonly Board r_Board;
        private readonly int m_BoardSize;
        private readonly int m_PictureBoxSize = 50;

        public Form1(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            InitializeComponent();
            r_Board = new Board(i_BoardSize);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeTableLayoutPanel();
        }

        private void initializeTableLayoutPanel()
        {
            //tableLayoutPanel1.Dock = DockStyle.None;
            //tableLayoutPanel1.Height = this.Height - 80;
            //tableLayoutPanel1.Width = this.Width -60;
            //tableLayoutPanel1.Location = new Point(20, 20);
            //tableLayoutPanel1.Margin = new Padding(30, 30, 30, 30);
            //tableLayoutPanel1.ColumnCount = m_Board.BoardSize;
            //tableLayoutPanel1.RowCount = m_Board.BoardSize;
            //tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            //tableLayoutPanel1.AutoSize = true;
            //tableLayoutPanel1.AutoSize = true;
            //tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //tableLayoutPanel1.Margin = new Padding(30, 30, 30, 30);
            //tableLayoutPanel1.Dock = DockStyle.Fill;
            //tableLayoutPanel1.ColumnCount = m_BoardSize;
            //tableLayoutPanel1.RowCount = m_BoardSize;
            for (int i = 0; i < m_BoardSize; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / m_BoardSize));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / m_BoardSize));
            }

            foreach(Cell cell in r_Board.Cells)
            {
                PictureBox pictureBox = new PictureBox();
                //pictureBox.Location = new Point(cell.Col * m_PictureBoxSize, cell.Row * m_PictureBoxSize);
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Size = new Size(m_PictureBoxSize, m_PictureBoxSize);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Image = setImage(cell.CurrentColor);
                pictureBox.Click += PictureBox_Click;
                if (cell.CurrentColor == Player.eColor.Green)
                {
                    pictureBox.BackColor = Color.LimeGreen;
                }
                else
                {
                  pictureBox.Enabled = false;
                }

                cell.PictureBox = pictureBox;
                   
                 //pictureBox.Click += PictureBox_Click;
                 //pictureBox.Image = Properties.Resources.CoinRed;
                 tableLayoutPanel1.Controls.Add(pictureBox, cell.Col, cell.Row);
                 //x += m_PictureBoxSize;
                 //if (x >= tableLayoutPanel1.Width * m_PictureBoxSize)
                 //{
                 //    x = 0;
                 //    y += m_PictureBoxSize;
                 //}
            }

        }
        /// <summary>
        /// Todo Event hendler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Click(object sender, EventArgs e)
        { 
            PictureBox mySender = sender as PictureBox;

            Cell cell = null;
            foreach (Cell currentCell in r_Board.Cells)
            {
                if (currentCell.PictureBox == mySender)
                {
                    cell = currentCell;
                    break;
                }
            }

            MessageBox.Show(cell.ToString());

        }

        private Image setImage(Player.eColor i_Color)
        {
            Image imageToSet = null;

            switch(i_Color)
            {
                case Player.eColor.Black:
                    {
                        imageToSet = Properties.Resources.CoinRed;
                        break;
                    }
                case Player.eColor.White:
                    {
                        imageToSet = Properties.Resources.CoinYellow;
                        break;
                    }
            }

            return imageToSet;
        }

    }
}
