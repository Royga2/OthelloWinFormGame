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
    public partial class GameForm : Form
    {
        //private readonly TableLayoutPanel m_TableLayoutPanel = new TableLayoutPanel();
        private readonly int r_BoardSize;
        private const int k_PictureBoxSize = 50;
        public event Action<int,int> OnPictureBoxClicked;

        public GameForm(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            InitializeComponent();
            initializeTableLayoutPanel();

        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //initializeTableLayoutPanel();
        }

        private void initializeTableLayoutPanel()
        {
            tableLayoutPanel1.Dock = DockStyle.None;
            tableLayoutPanel1.Height = this.Height - 80;
            tableLayoutPanel1.Width = this.Width -60;
            tableLayoutPanel1.Location = new Point(20, 20);
            //tableLayoutPanel1.Margin = new Padding(30, 30, 30, 30);
            //tableLayoutPanel1.ColumnCount = m_Board.BoardSize;
            //tableLayoutPanel1.RowCount = m_Board.BoardSize;
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            //tableLayoutPanel1.AutoSize = false;
            //tableLayoutPanel1.AutoSize = true;
            //tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //tableLayoutPanel1.Margin = new Padding(30, 30, 30, 30);
            //tableLayoutPanel1.Dock = DockStyle.Fill;
            //tableLayoutPanel1.ColumnCount = m_BoardSize;
            //tableLayoutPanel1.RowCount = m_BoardSize;
            for (int i = 0; i < r_BoardSize; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / r_BoardSize));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / r_BoardSize));
            }

            //foreach (Control control in tableLayoutPanel1.Controls)
            //{
            //    PictureBox pictureBox = control as PictureBox;
            //    if (pictureBox != null)
            //    {
            //        // Perform operations on the picture box
            //    }
            //}
            //foreach (Control control in tableLayoutPanel1.Controls)
            //{
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Dock = DockStyle.Fill;
                    pictureBox.Size = new Size(k_PictureBoxSize, k_PictureBoxSize);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pictureBox.Image = setImage(cell.CurrentColor);
                    //pictureBox.Image = null;
                    pictureBox.Enabled = false;
                    pictureBox.Click += PictureBox_Click;
                    //if (cell.CurrentColor == Player.eColor.Green)
                    //{
                    //    pictureBox.BackColor = Color.LimeGreen;
                    //}
                    //else
                    //{
                    //  pictureBox.Enabled = false;
                    //}

                    //int row = tableLayoutPanel1.GetRow(control);
                    //int col = tableLayoutPanel1.GetColumn(control);

                    ///cell.PictureBox = pictureBox;

                    //pictureBox.Click += PictureBox_Click;
                    //pictureBox.Image = Properties.Resources.CoinRed;
                    tableLayoutPanel1.Controls.Add(pictureBox, col, row);
                    //x += m_PictureBoxSize;
                    //if (x >= tableLayoutPanel1.Width * m_PictureBoxSize)
                    //{
                    //    x = 0;
                    //    y += m_PictureBoxSize;
                    //}
                }
            }

        }

        public void UpdateTablePictureBox(string i_Color, int i_Row, int i_Colomn)
        {
            PictureBox currentPictureBox =  tableLayoutPanel1.GetControlFromPosition(i_Colomn, i_Row) as PictureBox;
            currentPictureBox.BackColor = Color.Empty;
            currentPictureBox.Enabled = false;
            switch (i_Color)
            {
                case "Black":
                    {
                        currentPictureBox.Image = Properties.Resources.CoinRed;
                        break;
                    }
                case "White":
                    {
                        currentPictureBox.Image = Properties.Resources.CoinYellow;
                        break;
                    }
                case "Green":
                    {
                        currentPictureBox.BackColor = Color.Green;
                        currentPictureBox.Enabled = true;
                        break;
                    }
            }

        }



        public void PictureBox_Click(object sender, EventArgs e)
        { 
            PictureBox mySender = sender as PictureBox;

            TableLayoutPanelCellPosition position = tableLayoutPanel1.GetPositionFromControl(mySender);
            int row = position.Row;
            int col = position.Column;
            MessageBox.Show($"{row},{col}");
            OnPictureBoxClicked(row, col);

        }

        //public void OnClickPictureBox(object sender, EventArgs e)
        //{
        //    PictureBox_Click(sender,e);
        //}

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ChangeGameFormTitle(string i_CurrentPlayerColor)
        {
            this.Text = string.Format("Othello - {0} Turn",i_CurrentPlayerColor);
        }

    }
}
