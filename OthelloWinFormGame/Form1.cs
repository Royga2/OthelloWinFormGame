using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OthelloWinFormGame
{
    public partial class Form1 : Form
    {
        //private readonly TableLayoutPanel m_TableLayoutPanel = new TableLayoutPanel();
        private readonly Cell[,] m_Cells;
        private readonly int m_BoardSize;
        private readonly int m_PictureBoxSize = 50;
        public Form1(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_Cells = new Cell[m_BoardSize, m_BoardSize];

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeTableLayoutPanel();
        }

        private void initializeTableLayoutPanel()
        {

            //tableLayoutPanel1.Dock = DockStyle.Fill;
            //tableLayoutPanel1.ColumnCount = m_BoardSize;
            //tableLayoutPanel1.RowCount = m_BoardSize;
            //for (int i = 0; i < m_BoardSize; i++)
            //{
            //    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / m_BoardSize));
            //    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / m_BoardSize));
            //}

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_Cells[i, j] = new Cell(i, j);
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Dock = DockStyle.Fill;
                    pictureBox.Size = new System.Drawing.Size(m_PictureBoxSize, m_PictureBoxSize);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pictureBox.Image = Properties.Resources.CoinRed;
                    tableLayoutPanel1.Controls.Add(pictureBox, j, i);
                }
            }

            (tableLayoutPanel1.GetControlFromPosition(m_BoardSize / 2 - 1, (m_BoardSize / 2) - 1) as PictureBox).Image = Properties.Resources.CoinRed;
            (tableLayoutPanel1.GetControlFromPosition(m_BoardSize / 2, m_BoardSize / 2) as PictureBox).Image = Properties.Resources.CoinRed;
            (tableLayoutPanel1.GetControlFromPosition(m_BoardSize / 2, m_BoardSize / 2 - 1) as PictureBox).Image = Properties.Resources.CoinYellow;
            (tableLayoutPanel1.GetControlFromPosition(m_BoardSize / 2 - 1, m_BoardSize / 2) as PictureBox).Image = Properties.Resources.CoinYellow;

            this.Controls.Add(tableLayoutPanel1);
        }

    }
}
