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
    public partial class FormGame : Form
    {
        //private readonly TableLayoutPanel m_TableLayoutPanel = new TableLayoutPanel();
        private readonly int r_BoardSize;
        private const int k_PictureBoxSize = 50;
        public event Action<int,int> OnPictureBoxClicked;
        public event FormClosingEventHandler OnFormGameClosing;

        public FormGame(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            InitializeComponent();
            this.tableLayoutPanel1.AutoSize = false;
            initializeTableLayoutPanel();
            FormClosing += FormGame_FormClosing;


        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnFormGameClosing != null)
            {
                OnFormGameClosing(sender, e);
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            //initializeTableLayoutPanel();
        }

        private void initializeTableLayoutPanel()
        {
            
            tableLayoutPanel1.Dock = DockStyle.None;
            tableLayoutPanel1.Height = this.Height - 80;
            tableLayoutPanel1.Width = this.Width -60;
            tableLayoutPanel1.Location = new Point(20, 20);
            tableLayoutPanel1.ColumnCount = r_BoardSize;
            tableLayoutPanel1.RowCount = r_BoardSize;
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            //tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //tableLayoutPanel1.Margin = new Padding(30, 30, 30, 30);
            for (int i = 0; i < r_BoardSize; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / r_BoardSize));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / r_BoardSize));
            }

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Dock = DockStyle.Fill;
                    pictureBox.Size = new Size(k_PictureBoxSize, k_PictureBoxSize);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pictureBox.Image = null;
                    pictureBox.Enabled = false;
                    pictureBox.Click += PictureBox_Click;
                    tableLayoutPanel1.Controls.Add(pictureBox, col, row);
                   
                }
            }

        }

        public void UpdateTablePictureBox(string i_Color, int i_Row, int i_Colomn)
        {
            PictureBox currentPictureBox =  tableLayoutPanel1.GetControlFromPosition(i_Colomn, i_Row) as PictureBox;
            if(currentPictureBox != null)
            {
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
                            currentPictureBox.BackColor = Color.LimeGreen;
                            currentPictureBox.Enabled = true;
                            break;
                        }
                }
            
            }

        }



        public void PictureBox_Click(object sender, EventArgs e)
        { 
            PictureBox mySender = sender as PictureBox;

            TableLayoutPanelCellPosition position = tableLayoutPanel1.GetPositionFromControl(mySender);
            int row = position.Row;
            int col = position.Column;
            //MessageBox.Show($"{row},{col}");
            if(OnPictureBoxClicked != null)
            {
                OnPictureBoxClicked(row, col);
            }
            

        }

        //public void OnClickPictureBox(object sender, EventArgs e)
        //{
        //    PictureBox_Click(sender,e);
        //}

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ChangeFormGameTitle(string i_CurrentPlayerColor)
        {
            this.Text = string.Format("Othello - {0} Turn",i_CurrentPlayerColor);
        }

    }
}
