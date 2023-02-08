using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OthelloLogic;
using OthelloUI;
using System.Windows.Forms;

namespace OthelloController
{
    class Controller
    {
        private  GameManager m_GameManager;
        private  int m_BoardSize = 6;
        private bool m_IsComputer;
        private readonly FormSetting m_FormSetting;
        private FormGame m_FormGame;
        private bool m_GameOn;
        private int m_Player1Wins = 0;
        private int m_Player2Wins = 0;


        public Controller()
        {
            m_FormSetting = new FormSetting();
            m_FormSetting.FormSettingClosing += formSetting_Closing;
            m_FormSetting.GameModeButtonsClicked += formSetting_GameModeButtonsClicked;
            m_FormSetting.ShowDialog();
            startGame(m_BoardSize, m_IsComputer);
        }

        

        private void startGame(int i_BoarSize, bool i_IsComputer)
        {
            m_GameManager = new GameManager(i_BoarSize, i_IsComputer);
            initializedGame();
            PlayGame();
        }

        private void initializedGame()
        {
            m_FormGame = new FormGame(m_BoardSize);
            m_GameOn = true;
            m_FormGame.FormGameClosing += formGame_Closing;
            m_FormGame.PictureBoxClicked += pictureBox_Click;
            m_GameManager.GameOver += gameManager_GameOver;
            m_GameManager.TurnSkipped += gameManager_TurnSkipped;
            m_GameManager.CellColorChanged += gameManager_CellColorChanged;
            initializedUI();
        }

        private void initializedUI()
        {
            foreach (Cell currentCell in m_GameManager.GameBoard.Cells)
            {
                m_FormGame.UpdateTablePictureBox(currentCell.CurrentColor.ToString(), currentCell.Row, currentCell.Col);
            }

            Dictionary<Cell, List<Cell>> currentLegalMoves = m_GameManager.LegalMoves;

            foreach (Cell currentCell in currentLegalMoves.Keys)
            {
                m_FormGame.UpdateTablePictureBox("Green", currentCell.Row, currentCell.Col);
            }
        }

        private void gameManager_CellColorChanged(string i_Color, int i_Row, int i_Colomn)
        {
            m_FormGame.UpdateTablePictureBox(i_Color,  i_Row, i_Colomn);
        }

        private void formGame_Closing(object sender, FormClosingEventArgs e)
        {
            formClosingCheck(sender, e);
        }

        private void formClosingCheck(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }
        private void formSetting_Closing(object sender, FormClosingEventArgs e)
        {
            formClosingCheck(sender, e);
        }

        public void PlayGame()
        {
            while (m_GameOn == true)
            {
                if (m_FormGame.ShowDialog() == DialogResult.Cancel)
                {
                    break;
                }
            }
        }

        private void gameManager_TurnSkipped(string i_Message)
        {
            MessageBox.Show(i_Message);
        }

        private void playerMove(int i_Row, int i_Col)
        {
            Cell currentMove = new Cell(i_Row, i_Col);
            m_GameManager.MakeMove(currentMove);
            m_FormGame.ChangeFormGameTitle(m_GameManager.CurrentPlayer.PlayerColor.ToString());
        }

        private void cpuMove()
        {
            Random randomMove = new Random();
            ICollection<Cell> keys = m_GameManager.PlayerLegalMove.Keys;
            Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
            m_GameManager.MakeMove(randomKey);
            m_FormGame.ChangeFormGameTitle(m_GameManager.CurrentPlayer.PlayerName.ToString());
        }

        private void pictureBox_Click(int i_Row, int i_Col)
        {
            playerMove(i_Row, i_Col);

            if (m_GameManager.CurrentPlayer.IsComputer == true)
            {
                cpuMove();
            }
        }

        private string updateRoundWinner()
        {
            string roundWinner = m_GameManager.GetWinnerPlayerName();

            if (roundWinner == "Black")
            {
                m_Player1Wins += 1;
            }
            else if (roundWinner == "White")
            {
                m_Player2Wins += 1;
            }

            return roundWinner;
        }

        private string endRoundAndReturnWinnerName()
        {
            string roundWinner = updateRoundWinner();
            m_GameOn = false;
            m_GameManager.GameOver -= gameManager_GameOver;
            m_FormGame.PictureBoxClicked -= pictureBox_Click;
            m_FormGame.FormGameClosing -= formGame_Closing;

            return roundWinner;
        }

        private void gameOverMessageBox(string i_Message)
        {
            DialogResult result = MessageBox.Show(i_Message, "Othello", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                startGame(m_BoardSize, m_IsComputer);
            }
            else if (result == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }
        }
        private void formSetting_GameModeButtonsClicked(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;

            if (buttonSender != null)
            {
                FormSetting formSender = buttonSender.Parent as FormSetting;
                if (formSender != null)
                {
                    m_BoardSize = formSender.BoardSize;
                    if (buttonSender.Name == "buttonPlayCPU")
                    {
                        m_IsComputer = true;
                    }
                    else if (buttonSender.Name == "buttonPVP")
                    {
                        m_IsComputer = false;
                    }
                    formSender.Dispose();
                }
            }
        }

        private void gameManager_GameOver(object sender, EventArgs e)
        {
            string roundWinnerName = endRoundAndReturnWinnerName();
            string endGameMessage = string.Format(@"{0} Won!! ({1}/{2}) ({3}/{4})
Would you like another round?", roundWinnerName, m_GameManager.GameBoard.BlackCount, m_GameManager.GameBoard.WhiteCount, m_Player1Wins, m_Player2Wins);
            m_FormGame.Dispose();
            gameOverMessageBox(endGameMessage);

        }
    }
}
