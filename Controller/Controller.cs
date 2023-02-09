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
        private readonly FormSetting r_FormSetting;
        private FormGame m_FormGame;
        private bool m_GameOn;
        private readonly int[] r_WinsCounter;


        public Controller()
        {
            r_WinsCounter = new int[3];
            r_FormSetting = new FormSetting();
            r_FormSetting.FormSettingClosing += formSetting_Closing;
            r_FormSetting.GameModeButtonsClicked += formSetting_GameModeButtonsClicked;
            r_FormSetting.ShowDialog();
            startGame(m_BoardSize, m_IsComputer);
        }

        private void startGame(int i_BoarSize, bool i_IsComputer)
        {
            m_GameManager = new GameManager(i_BoarSize, i_IsComputer);
            initializedGame();
            playGame();
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
            initializeUI();
        }

        private void initializeUI()
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

        private void gameManager_CellColorChanged(string i_Color, int i_Row, int i_Col)
        {
            m_FormGame.UpdateTablePictureBox(i_Color,  i_Row, i_Col);
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

        private void playGame()
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
            m_FormGame.ChangeFormGameTitle(getCurrentPlayer());
        }

        private void cpuMove()
        {
            Random randomMove = new Random();
            ICollection<Cell> keys = m_GameManager.PlayerLegalMove.Keys;
            Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
            m_GameManager.MakeMove(randomKey);
            m_FormGame.ChangeFormGameTitle(getCurrentPlayer());
        }

        private string getCurrentPlayer()
        {
            string currentPlayer = string.Format($"{m_GameManager.CurrentPlayer.PlayerColor}");

            return currentPlayer;
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
                r_WinsCounter[0] += 1;
            }
            else if (roundWinner == "White")
            {
                r_WinsCounter[1] += 1;
            }
            else
            {
                r_WinsCounter[2] += 1;
            }
            
            return roundWinner;
        }

        private string[] endRoundAndReturnParamsForMsg()
        {
            string roundWinner = updateRoundWinner();
            string[] msgParams = getMsgParams(roundWinner);
            m_GameOn = false;
            m_FormGame.FormGameClosing -= formGame_Closing;
            m_FormGame.PictureBoxClicked -= pictureBox_Click;
            m_GameManager.GameOver -= gameManager_GameOver;
            m_GameManager.TurnSkipped -= gameManager_TurnSkipped;
            m_GameManager.CellColorChanged -= gameManager_CellColorChanged;

            return msgParams;
        }

        private string[] getMsgParams(string i_RoundWinner)
        {
            string[] endOfRoundMsgParams = new string[5];
            endOfRoundMsgParams[0] = string.Format($"{i_RoundWinner}");
            endOfRoundMsgParams[1] = string.Format($"{m_GameManager.GameBoard.BlackCount}");
            endOfRoundMsgParams[2] = string.Format($"{m_GameManager.GameBoard.WhiteCount}");
            string numberOfWinsForWinner;

            switch(i_RoundWinner)
            {
                case "Black":
                    {
                        numberOfWinsForWinner = string.Format($"{r_WinsCounter[0]}");
                        break;
                    }
                case "White":
                    {
                        numberOfWinsForWinner = string.Format($"{r_WinsCounter[1]}");
                        break;
                    }
                default:
                    {
                        numberOfWinsForWinner = string.Format($"{r_WinsCounter[2]}");
                        break;
                    }
            }
            endOfRoundMsgParams[3] = numberOfWinsForWinner;
            endOfRoundMsgParams[4] = string.Format($"{r_WinsCounter[0] + r_WinsCounter[1] + r_WinsCounter[2]}");

            return endOfRoundMsgParams;
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
            string[] endOfRoundParams = endRoundAndReturnParamsForMsg();
            string endGameMessage = string.Format(@"{0} Won!! ({1}/{2}) ({3}/{4})
Would you like another round?", endOfRoundParams);
            m_FormGame.Dispose();
            gameOverMessageBox(endGameMessage);

        }
    }
}
