using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;


namespace OthelloUI
{
    public partial class FormGame : Form
    {
        private readonly int r_BoardSize;
        private const int k_PictureBoxSize = 50;
        private readonly TableLayoutPanel r_TableLayoutPanelForPictureBoxes;
        public event Action<int,int> PictureBoxClicked;
        public event FormClosingEventHandler FormGameClosing;


        public FormGame(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            InitializeComponent();
            this.r_TableLayoutPanelForPictureBoxes = new TableLayoutPanel();
            initializeTableLayoutPanel();
            FormClosing += OnFormGameClosing;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void initializeTableLayoutPanel()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size((r_BoardSize * 50), (r_BoardSize * 50));
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            r_TableLayoutPanelForPictureBoxes.Size = new System.Drawing.Size((r_BoardSize * 50), (r_BoardSize * 50));
            r_TableLayoutPanelForPictureBoxes.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            r_TableLayoutPanelForPictureBoxes.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            r_TableLayoutPanelForPictureBoxes.Name = "m_TableLayoutPanelforPictureBoxes";
            r_TableLayoutPanelForPictureBoxes.TabIndex = 0;
            r_TableLayoutPanelForPictureBoxes.Dock = DockStyle.None;
            r_TableLayoutPanelForPictureBoxes.Height = this.Height - 82;
            r_TableLayoutPanelForPictureBoxes.Width = this.Width - 60;
            r_TableLayoutPanelForPictureBoxes.Location = new Point(20, 20);
            r_TableLayoutPanelForPictureBoxes.ColumnCount = r_BoardSize;
            r_TableLayoutPanelForPictureBoxes.RowCount = r_BoardSize;
            r_TableLayoutPanelForPictureBoxes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            for (int i = 0; i < r_BoardSize; i++)
            {
                r_TableLayoutPanelForPictureBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / r_BoardSize));
                r_TableLayoutPanelForPictureBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / r_BoardSize));
            }

            this.Controls.Add(this.r_TableLayoutPanelForPictureBoxes);
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Dock = DockStyle.Fill;
                    pictureBox.Size = new Size(k_PictureBoxSize, k_PictureBoxSize);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Enabled = false;
                    pictureBox.Click += pictureBox_Click;
                    r_TableLayoutPanelForPictureBoxes.Controls.Add(pictureBox, col, row);
                }
            }
            this.ResumeLayout(false);
        }

        public void UpdateTablePictureBox(string i_Color, int i_Row, int i_Col)
        {
            PictureBox currentPictureBox = r_TableLayoutPanelForPictureBoxes.GetControlFromPosition(i_Col, i_Row) as PictureBox;

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

        protected virtual void OnFormGameClosing(object sender, FormClosingEventArgs e)
        {
            if (FormGameClosing != null)
            {
                FormGameClosing.Invoke(sender, e);
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        { 
            PictureBox clickedPictureBox = sender as PictureBox;

            if(clickedPictureBox != null)
            {
                TableLayoutPanelCellPosition position = r_TableLayoutPanelForPictureBoxes.GetPositionFromControl(clickedPictureBox);
                int row = position.Row;
                int col = position.Column;
                OnPictureBoxClicked(row, col);
            }
        }

        protected virtual void OnPictureBoxClicked(int i_Row, int i_Col)
        {
            if(PictureBoxClicked != null)
            {
                PictureBoxClicked(i_Row, i_Col);
            }
        }
        
        public void ChangeFormGameTitle(string i_CurrentPlayerColor)
        {
            this.Text = string.Format("Othello - {0} Turn",i_CurrentPlayerColor);
        }

    }
}
