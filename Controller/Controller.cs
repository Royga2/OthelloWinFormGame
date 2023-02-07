using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OthelloLogic;
using OthelloWinFormGame;
using System.Windows.Forms;

namespace OthelloController
{
    class Controller
    {
        private  GameManager m_GameManager;
        private int m_BoardSize;
        private bool m_IsComputer;
        private UIManager m_UIManager;
        private bool m_GameOn;
        private int m_Player1Wins = 0;
        private int m_Player2Wins = 0;

        public Controller()
        {
            m_UIManager = new UIManager();
            m_BoardSize = m_UIManager.GameSettingForm.BoardSize;
            m_IsComputer = m_UIManager.GameSettingForm.IsAgainstComputer;
            startGame(m_BoardSize, m_IsComputer);
            
        }

        private void startGame(int i_BoarSize, bool i_IsComputer)
        {
            m_GameManager = new GameManager(i_BoarSize, i_IsComputer);
            m_GameOn = true;
            m_GameManager.IsGameOver += OnGameOver;
            m_UIManager.GameForm.OnPictureBoxClicked += OnClickMoveHandler;
            UpdateUIBoard();
            PlayGame();
        }
        private void UpdateUIBoard()
        {
            Dictionary<Cell, List<Cell>> currentLegalMoves = m_GameManager.LegalMoves;

            foreach (Cell currentCell in m_GameManager.GameBoard.Cells)
            {

                m_UIManager.GameForm.UpdateTablePictureBox(currentCell.CurrentColor.ToString(), currentCell.Row, currentCell.Col);
            }
            foreach (Cell currentCell in currentLegalMoves.Keys)
            {
                m_UIManager.GameForm.UpdateTablePictureBox("Green", currentCell.Row, currentCell.Col);
            }
        }
        
        public void PlayGame()
        {
            while (m_GameOn == true)
            {
                if (m_UIManager.GameForm == null || m_UIManager.GameForm.IsDisposed)
                {
                    m_UIManager.GameForm = new GameForm(m_BoardSize);
                }
                
                if (m_UIManager.GameForm.ShowDialog() == DialogResult.Cancel)
                {
                    break;
                }
            }
        }


        public void OnClickMoveHandler(int i_Row, int i_Col)
        {
            Cell currentMove = new Cell(i_Row, i_Col);
            m_GameManager.MakeMove(currentMove);
            UpdateUIBoard();
            m_UIManager.GameForm.ChangeGameFormTitle(m_GameManager.CurrentPlayer.PlayerColor.ToString());

            if (m_GameManager.CurrentPlayer.IsComputer == true)
            {
                Random randomMove = new Random();
                ICollection<Cell> keys = m_GameManager.PlayerLegalMove.Keys;
                Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
                m_GameManager.MakeMove(randomKey);
                UpdateUIBoard();
                m_UIManager.GameForm.ChangeGameFormTitle(m_GameManager.CurrentPlayer.PlayerName.ToString());
            }

        }

        public void GetLegalMoves()
        {
            Dictionary<Cell, List<Cell>> currentLegalMoves = m_GameManager.LegalMoves;
        }

        private void updateRoundWinner()
        {
            if(m_GameManager.GetWinnerPlayerName() == "Black")
            {
                m_Player1Wins += 1;
            }
            else
            {
                m_Player2Wins += 1;
            }
        }
        private void OnGameOver(object sender, EventArgs e)
        {
            UpdateUIBoard();
            updateRoundWinner();
            m_GameOn = false;
            m_GameManager.IsGameOver -= OnGameOver;
            m_UIManager.GameForm.OnPictureBoxClicked -= OnClickMoveHandler;
            
            string endGameMessage = string.Format(@"{0} Won!! ({1}/{2}) ({3}/{4})
Would you like another round?", m_GameManager.GetWinnerPlayerName(), m_GameManager.GameBoard.BlackCount, m_GameManager.GameBoard.WhiteCount, m_Player1Wins, m_Player2Wins);
            //MessageBox.Show(endGameMessage);
            m_UIManager.GameForm.Dispose();
            DialogResult result = MessageBox.Show(endGameMessage, "Othello", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                startGame(m_BoardSize, m_IsComputer);
            }
            else if (result == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }
        }
    }
}
