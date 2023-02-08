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
        int m_BoardSize = 6;
        bool m_IsAgainstComputer;
        public event FormClosingEventHandler OnFormSettingClosing;
        public event EventHandler OnClickGameMode;

        public FormSetting()
        {
            InitializeComponent();
            FormClosing += FormSetting_FormClosing;
            //.FormClosed += ExitApp;
            //if (CancelButton.DialogResult == DialogResult.Cancel)
            //{
            //    Application.Exit();
            //}
        }
      
            private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (OnFormSettingClosing != null)
                {
                    OnFormSettingClosing(sender, e);
                }
            }

            public void ExitApp(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Environment.Exit(0);
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
            //Close();
            OnClickGameMode?.Invoke(sender, e);
        }

        private void buttonPVP_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = false;
            //Close();
            OnClickGameMode?.Invoke(sender, e);
        }

        //public Button ButtonPVP
        //{
        //    get { return this.buttonPVP; }
        //}

        //public Button ButtonPlayCPU
        //{
        //    get { return this.buttonPlayCPU; }
        //}

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        //public bool IsAgainstComputer
        //{
        //    get { return m_IsAgainstComputer; }
        //}

    }
}
