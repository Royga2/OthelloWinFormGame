using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OthelloUI
{
    public partial class FormSetting : Form
    {
        private int m_BoardSize = 6;
        private bool m_IsAgainstComputer;
        public event FormClosingEventHandler FormSettingClosing;
        public event EventHandler GameModeButtonsClicked;


        public FormSetting()
        {
            InitializeComponent();
            FormClosing += OnFormSettingClosing;
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public bool IsAgainstComputer
        {
            get { return m_IsAgainstComputer; }
            set { m_IsAgainstComputer = value; }
        }

        protected virtual void OnFormSettingClosing(object sender, FormClosingEventArgs e)
        {
            if (FormSettingClosing != null)
            {
                FormSettingClosing.Invoke(sender, e);
            }
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

        protected virtual void OnGameModeButtonsClicked(object sender, EventArgs e)
        {
            if(GameModeButtonsClicked != null)
            {
                GameModeButtonsClicked.Invoke(sender, e);
            }
        }

        private void buttonPlayCPU_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = true;
            OnGameModeButtonsClicked(sender, e);
        }

        private void buttonPVP_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = false;
            OnGameModeButtonsClicked(sender, e);
        }
    }
}
