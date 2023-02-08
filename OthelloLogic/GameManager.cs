using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OthelloLogic
{
    public class GameManager
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Board m_GameBoard;
        private Player m_CurrentPlayer;
        private Dictionary<Cell, List<Cell>> m_PlayerLegalMove;
        private bool m_GameOver;
        private string m_Winner;
        public event EventHandler IsGameOver;
        public event Action<string> TurnSkipped;


        public GameManager(int i_BoardSize, bool i_IsComputer)
        {
   
            r_Player1 = new Player();
            r_Player2 = new Player(r_Player1.PlayerColor, i_IsComputer);
            m_GameBoard = new Board(i_BoardSize);
            m_CurrentPlayer = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1 : r_Player2;
            m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);

        }

        //for test purpeses
        //public GameManager(string i_Player1Name)
        //{
        //    r_Player1 = new Player(i_Player1Name, Player.eColor.Black);
        //    r_Player2 = new Player(Player.eColor.Black, true);
        //    m_CurrentPlayer = r_Player1;
        //    m_GameBoard = new Board(6, r_Player1.PlayerName, r_Player2.PlayerName);
        //    m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);
        //    //PlayGame();

        //}

        public Board GameBoard
        {
            get { return m_GameBoard; }
            //set { m_GameBoard = value; }
        }

        public bool GameOver
        {
            get { return m_GameOver; }
            set { m_GameOver = value; }
        }

        public string Winner
        {
            get { return m_Winner; }
            set { m_Winner = value; }
        }

        public Dictionary<Cell, List<Cell>> LegalMoves
        {
            get
            {
                return m_PlayerLegalMove;
            }
        }
        public bool MakeMove(Cell i_Cell)
        {
            if (!m_PlayerLegalMove.ContainsKey((i_Cell)))
            {
                return false;
            }

            Player movePlayer = m_CurrentPlayer;
            List<Cell> capturbaleCells = m_PlayerLegalMove[i_Cell];
            m_GameBoard.Cells[i_Cell.Row, i_Cell.Col].CurrentColor = CurrentPlayer.PlayerColor;
            flipDiscs(capturbaleCells);
            m_GameBoard.UpdatesScore(movePlayer, capturbaleCells.Count);
            passTurn();
            //m_GameBoard.DisplayBoard(r_Player1.PlayerName, r_Player2.PlayerName, m_CurrentPlayer.PlayerName);
            return true;
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public Dictionary<Cell, List<Cell>> PlayerLegalMove
        {
            get { return m_PlayerLegalMove; }
            set { m_PlayerLegalMove = value; }
        }

        //TODO : NEED TO Fiz func to have only 1 return! 
        private List<Cell> capturbaleCellsInline(Cell i_Cell, Player i_Player, int i_RowOffSet, int i_ColOffSet)
        {
            List<Cell> capturbaleCells = new List<Cell>();
            int row = i_Cell.Row + i_RowOffSet;
            int col = i_Cell.Col + i_ColOffSet;

            while (isInsideBoard(row, col) && m_GameBoard.Cells[row, col].CurrentColor != Player.eColor.None)
            {
                if (m_GameBoard.Cells[row, col].CurrentColor == OpponentPlayerColor(i_Player.PlayerColor))
                {
                    capturbaleCells.Add(new Cell(row, col));
                    row += i_RowOffSet;
                    col += i_ColOffSet;
                }
                else
                {
                    return capturbaleCells;
                }
            }

            return new List<Cell>();
        }

        public static Player.eColor OpponentPlayerColor(Player.eColor i_PlayerColor)
        {
            switch (i_PlayerColor)
            {
                case Player.eColor.White:
                    {
                        return Player.eColor.Black;
                    }
                case Player.eColor.Black:
                    {
                        return Player.eColor.White;
                    }
            }

            return Player.eColor.None;
        }

        private List<Cell> capturbaleCells(Cell i_Cell, Player i_Player)
        {
            List<Cell> capturbaleCells = new List<Cell>();
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (rowOffset == 0 && colOffset == 0)
                    {
                        continue;
                    }

                    capturbaleCells.AddRange(capturbaleCellsInline(i_Cell, i_Player, rowOffset, colOffset));
                }
            }

            return capturbaleCells;
        }

        private bool isMoveLegal(Player i_Player, Cell i_Cell, out List<Cell> o_CapturbaleCells)
        {
            if (m_GameBoard.Cells[i_Cell.Row, i_Cell.Col].CurrentColor != Player.eColor.None)
            {
                o_CapturbaleCells = null;
                return false;
            }

            o_CapturbaleCells = this.capturbaleCells(i_Cell, i_Player);
            return o_CapturbaleCells.Count > 0;
        }

        private bool isInsideBoard(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < m_GameBoard.BoardSize && i_Col >= 0 && i_Col < m_GameBoard.BoardSize;
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == r_Player1 ? r_Player2 : r_Player1;
            m_PlayerLegalMove = findLegalMoves(CurrentPlayer);
        }

        public string GetWinnerPlayerName()
        {
            string winnerName = "NO ONE :(";
            if (m_GameBoard.BlackCount > m_GameBoard.WhiteCount)
            {
                winnerName = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1.PlayerName : r_Player2.PlayerName;
            }

            if (m_GameBoard.WhiteCount > m_GameBoard.BlackCount)
            {
                winnerName = r_Player1.PlayerColor == Player.eColor.White ? r_Player1.PlayerName : r_Player2.PlayerName;
            }

            return winnerName;

        }

        private void passTurn()
        {
            switchPlayer();
            if (m_PlayerLegalMove.Count > 0)
            {
                return;
            }

            string message = string.Format(
                @"{0} you don't have any legal move your turn is skipped",
                m_CurrentPlayer.PlayerName);
            
            switchPlayer();

            if (m_PlayerLegalMove.Count == 0)
            {
                IsGameOver?.Invoke(this, EventArgs.Empty);
                //m_GameOver = true;
            }
            else
            {
                TurnSkipped?.Invoke(message);
            }
        }

        private void flipDiscs(List<Cell> i_Cells)
        {
            foreach (Cell cell in i_Cells)
            {
                m_GameBoard.Cells[cell.Row, cell.Col].CurrentColor = m_CurrentPlayer.PlayerColor;
            }
        }

        private Dictionary<Cell, List<Cell>> findLegalMoves(Player i_Player)
        {
            Dictionary<Cell, List<Cell>> legalMoves = new Dictionary<Cell, List<Cell>>();
            for (int row = 0; row < m_GameBoard.BoardSize; row++)
            {
                for (int col = 0; col < m_GameBoard.BoardSize; col++)
                {
                    Cell cellToAdd = new Cell(row, col);
               
                    if (isMoveLegal(i_Player, cellToAdd, out List<Cell> capturbaleCells))
                    {
                        legalMoves[cellToAdd] = capturbaleCells;
                    }
                }
            }

            return legalMoves;
        }


    }
}

