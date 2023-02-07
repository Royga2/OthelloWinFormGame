using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace OthelloWinFormGame
{
    public class UIManager
    {
        bool m_IsAgainstComputer;
        private Othello_GameSetting m_GameSettingForm;
        private GameForm m_GameForm;

        public UIManager()
        {
            m_GameSettingForm = new Othello_GameSetting();
            //m_GameSettingForm.FormClosed += FormSettingClosed;
            //m_GameSettingForm.
            m_GameSettingForm.ButtonPVP.Click += button_Clicked;
            m_GameSettingForm.ButtonPlayCPU.Click += button_Clicked;
            m_GameSettingForm.ShowDialog();

        }

        private void button_Clicked(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            Othello_GameSetting gameSettingForm = senderButton.Parent as Othello_GameSetting;
            m_GameForm = new GameForm(gameSettingForm.BoardSize);
            m_GameSettingForm.Dispose();
        }

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

        public void StartGameDialog()
        {
            //m_GameForm.ShowDialog();
            if (m_GameForm.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        public Othello_GameSetting GameSettingForm
        {
            get
            {
                return m_GameSettingForm;
            }
        }

        public GameForm GameForm
        {
            get
            {
                return m_GameForm;
            }
            set
            {
                m_GameForm = value;
            }
        }
    }

}
