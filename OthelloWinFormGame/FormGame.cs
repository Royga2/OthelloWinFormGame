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
        private TableLayoutPanel m_TableLayoutPanelforPictureBoxes;
        public event Action<int,int> PictureBoxClicked;
        public event FormClosingEventHandler FormGameClosing;


        public FormGame(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            InitializeComponent();
            this.m_TableLayoutPanelforPictureBoxes = new TableLayoutPanel();
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
            m_TableLayoutPanelforPictureBoxes.Size = new System.Drawing.Size((r_BoardSize * 50), (r_BoardSize * 50));
            m_TableLayoutPanelforPictureBoxes.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            m_TableLayoutPanelforPictureBoxes.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            m_TableLayoutPanelforPictureBoxes.Name = "m_TableLayoutPanelforPictureBoxes";
            m_TableLayoutPanelforPictureBoxes.TabIndex = 0;
            m_TableLayoutPanelforPictureBoxes.Dock = DockStyle.None;
            m_TableLayoutPanelforPictureBoxes.Height = this.Height - 82;
            m_TableLayoutPanelforPictureBoxes.Width = this.Width - 60;
            m_TableLayoutPanelforPictureBoxes.Location = new Point(20, 20);
            m_TableLayoutPanelforPictureBoxes.ColumnCount = r_BoardSize;
            m_TableLayoutPanelforPictureBoxes.RowCount = r_BoardSize;
            m_TableLayoutPanelforPictureBoxes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            for (int i = 0; i < r_BoardSize; i++)
            {
                m_TableLayoutPanelforPictureBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / r_BoardSize));
                m_TableLayoutPanelforPictureBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / r_BoardSize));
            }

            this.Controls.Add(this.m_TableLayoutPanelforPictureBoxes);
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
                    m_TableLayoutPanelforPictureBoxes.Controls.Add(pictureBox, col, row);
                }
            }
            this.ResumeLayout(false);
        }

        public void UpdateTablePictureBox(string i_Color, int i_Row, int i_Col)
        {
            PictureBox currentPictureBox = m_TableLayoutPanelforPictureBoxes.GetControlFromPosition(i_Col, i_Row) as PictureBox;

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
                TableLayoutPanelCellPosition position = m_TableLayoutPanelforPictureBoxes.GetPositionFromControl(clickedPictureBox);
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
