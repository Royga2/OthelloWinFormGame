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
        private readonly int r_BoardSize;
        private readonly bool r_IsComputer;
        private UIManager m_UIManager;
        private bool m_GameOn;
        private int m_Player1Wins = 0;
        private int m_Player2Wins = 0;

        public Controller()
        {
            m_UIManager = new UIManager();
            r_BoardSize = m_UIManager.FormSetting.BoardSize;
            r_IsComputer = m_UIManager.FormSetting.IsAgainstComputer;
            startGame(r_BoardSize, r_IsComputer);
            
        }

        private void startGame(int i_BoarSize, bool i_IsComputer)
        {
            m_GameManager = new GameManager(i_BoarSize, i_IsComputer);
            m_GameOn = true;
            m_GameManager.IsGameOver += OnGameOver;
            m_UIManager.FormGame.OnFormGameClosing += FormGameClosingHandler;
            m_UIManager.FormGame.OnPictureBoxClicked += OnClickMoveHandler;
            UpdateUIBoard();
            PlayGame();
        }
        private void UpdateUIBoard()
        {
            Dictionary<Cell, List<Cell>> currentLegalMoves = m_GameManager.LegalMoves;

            foreach (Cell currentCell in m_GameManager.GameBoard.Cells)
            {

                m_UIManager.FormGame.UpdateTablePictureBox(currentCell.CurrentColor.ToString(), currentCell.Row, currentCell.Col);
            }
            foreach (Cell currentCell in currentLegalMoves.Keys)
            {
                m_UIManager.FormGame.UpdateTablePictureBox("Green", currentCell.Row, currentCell.Col);
            }
        }

        private void FormGameClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }

        }
        private void startAnotherRound()
        {
            m_UIManager.FormGame = new FormGame(r_BoardSize);
            m_GameOn = true;
            m_UIManager.FormGame.OnFormGameClosing += FormGameClosingHandler;
            m_GameManager.IsGameOver += OnGameOver;
            m_UIManager.FormGame.OnPictureBoxClicked += OnClickMoveHandler;
            UpdateUIBoard();
        }

        public void PlayGame()
        {
            while (m_GameOn == true)
            {
                
                if (m_UIManager.FormGame == null || m_UIManager.FormGame.IsDisposed)
                {
                    startAnotherRound();
                    
                }
                
                else if (m_UIManager.FormGame.ShowDialog() == DialogResult.Cancel)
                {
                    break;
                }
            }
        }

        private void playerMove(int i_Row, int i_Col)
        {
            Cell currentMove = new Cell(i_Row, i_Col);
            m_GameManager.MakeMove(currentMove);
            UpdateUIBoard();
            m_UIManager.FormGame.ChangeFormGameTitle(m_GameManager.CurrentPlayer.PlayerColor.ToString());
        }

        private void cpuMove()
        {
            Random randomMove = new Random();
            ICollection<Cell> keys = m_GameManager.PlayerLegalMove.Keys;
            Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
            m_GameManager.MakeMove(randomKey);
            UpdateUIBoard();
            m_UIManager.FormGame.ChangeFormGameTitle(m_GameManager.CurrentPlayer.PlayerName.ToString());
        }

        public void OnClickMoveHandler(int i_Row, int i_Col)
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
            UpdateUIBoard();
            m_GameOn = false;
            m_GameManager.IsGameOver -= OnGameOver;
            m_UIManager.FormGame.OnPictureBoxClicked -= OnClickMoveHandler;
            m_UIManager.FormGame.OnFormGameClosing -= FormGameClosingHandler;

            return roundWinner;
        }

        private void gameOverMessageBox(string i_Message)
        {
            DialogResult result = MessageBox.Show(i_Message, "Othello", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                startGame(r_BoardSize, r_IsComputer);
            }
            else if (result == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }
        }
        private void OnGameOver(object sender, EventArgs e)
        {
            string roundWinnerName = endRoundAndReturnWinnerName();
            string endGameMessage = string.Format(@"{0} Won!! ({1}/{2}) ({3}/{4})
Would you like another round?", roundWinnerName, m_GameManager.GameBoard.BlackCount, m_GameManager.GameBoard.WhiteCount, m_Player1Wins, m_Player2Wins);
            m_UIManager.FormGame.Dispose();
            gameOverMessageBox(endGameMessage);

        }
    }
}
