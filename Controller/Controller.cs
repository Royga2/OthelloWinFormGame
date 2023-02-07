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
        private readonly GameManager r_GameManager;
        private int m_BoardSize;
        bool m_IsComputer;
        private UIManager m_UIManager;
        bool m_GameOn;
        int m_Player1Wins = 0;
        int m_Player2Wins = 0;

        public Controller()
        {
            m_UIManager = new UIManager();
            m_BoardSize = m_UIManager.GameSettingForm.BoardSize;
            m_IsComputer = m_UIManager.GameSettingForm.IsAgainstComputer;
            r_GameManager = new GameManager(m_BoardSize, m_IsComputer);
            m_GameOn = true;

            r_GameManager.IsGameOver += OnGameOver;
            m_UIManager.GameForm.OnPictureBoxClicked += OnClickMoveHandler;
            UpdateUIBoard();
            PlayGame();
        }

        private void UpdateUIBoard()
        {
            Dictionary<Cell, List<Cell>> currentLegalMoves = r_GameManager.LegalMoves;

            foreach (Cell currentCell in r_GameManager.GameBoard.Cells)
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
                if (m_UIManager.GameForm.ShowDialog() == DialogResult.Cancel)
                {
                    break;
                }                    
            }
        }

        public void OnClickMoveHandler(int i_Row, int i_Col)
        {
            Cell CurrentMove = new Cell(i_Row, i_Col);
            r_GameManager.MakeMove(CurrentMove);
            UpdateUIBoard();
            m_UIManager.GameForm.ChangeGameFormTitle(r_GameManager.CurrentPlayer.PlayerColor.ToString());

            if (r_GameManager.CurrentPlayer.IsComputer == true)
            {
                Random randomMove = new Random();
                ICollection<Cell> keys = r_GameManager.PlayerLegalMove.Keys;
                Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
                r_GameManager.MakeMove(randomKey);
                UpdateUIBoard();
                m_UIManager.GameForm.ChangeGameFormTitle(r_GameManager.CurrentPlayer.PlayerColor.ToString());
            }

        }

        public void GetLegalMoves()
        {
            Dictionary<Cell, List<Cell>> currentLegalMoves = r_GameManager.LegalMoves;
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            // Close the game view
            m_UIManager.GameForm.Close();
            string endGameMessage = string.Format(@"{0} Won!! ({1}/{2}) ({3}/{4})
Would oy like another round?", r_GameManager.Winner, r_GameManager.GameBoard.BlackCount, r_GameManager.GameBoard.WhiteCount, m_Player1Wins, m_Player2Wins);
            MessageBox.Show(endGameMessage);
        }
    }
}
