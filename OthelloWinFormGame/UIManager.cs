using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace OthelloUI
{
    public class UIManager
    {
        private readonly FormSetting m_FormSetting;
        private FormGame m_FormGame;

        public UIManager()
        {
            m_FormSetting = new FormSetting();
            //m_GameSettingForm.FormClosed += FormSettingClosed;
            //m_GameSettingForm.
            m_FormSetting.OnFormSettingClosing += FormSettingClosingHandler;
            m_FormSetting.ButtonPVP.Click += button_Clicked;
            m_FormSetting.ButtonPlayCPU.Click += button_Clicked;
            m_FormSetting.ShowDialog();

            //if (m_GameSettingForm.ShowDialog() == DialogResult.Cancel)
            //{
            //    //Environment.Exit(0);
            //}

        }
        

        private void FormSettingClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
            
        }
     

        private void button_Clicked(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            if(senderButton != null)
            {
                FormSetting gameSettingForm = senderButton.Parent as FormSetting;
                if(gameSettingForm != null)
                {
                    m_FormGame = new FormGame(gameSettingForm.BoardSize);
                    m_FormSetting.Dispose();
                }
            }
        }

        //private void RestoreGameForm()
        //{
        //    m_GameForm.Controls.Clear();
        //    m_GameForm.InitializeComponent();
        //}

        //public void testFunc()
        //{
        //    Othello_GameSetting gameSettingForm = sender as Othello_GameSetting;
        //    m_GameForm = new GameForm(gameSettingForm.BoardSize);
        //    m_GameSettingForm.Dispose();
        //}
        //public void FormSettingClosed(object sender, FormClosedEventArgs e)
        //{
        //    Othello_GameSetting gameSettingForm = sender as Othello_GameSetting;
        //    //Button buttonSender = sender as Button;
        //    //if (buttonSender.Name == "buttonPlayCPU" || buttonSender.Name == "buttonPVP")
        //    //{
        //        m_GameForm = new GameForm(gameSettingForm.BoardSize);
        //        m_GameSettingForm.Dispose();
        //        //m_GameForm.ShowDialog();
        //    //}
        //    //else
        //    //{
        //        //gameSettingForm.Dispose();
        //    //}
        //}


        public FormSetting FormSetting
        {
            get { return m_FormSetting; }
        }

        public FormGame FormGame
        {
            get { return m_FormGame; }
            set { m_FormGame = value; }
        }
    }

}
