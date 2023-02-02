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
    public partial class Othello_GameSetting : Form
    {
        int m_BoardSize = 6;
        bool m_IsAgainstComputer;
        public Othello_GameSetting()
        {
            InitializeComponent();
        }

        private void Othello_GameSetting_Load(object sender, EventArgs e)
        {

        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            if (m_BoardSize < 12)
            {
                m_BoardSize += 2;
            }
            else
            {
                m_BoardSize = 6;
            }
            string buttonBoardSizeTitle = string.Format(@"Board Size: {0}x{0}(click to increase)", m_BoardSize);
            buttonBoardSize.Text = buttonBoardSizeTitle;
        }

        private void buttonPlayCPU_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = true;
        }

        private void buttonPVP_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = false;
        }
    }
}
