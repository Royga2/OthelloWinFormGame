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
        private readonly Board r_GameBoard;
        private Player m_CurrentPlayer;
        private Dictionary<Cell, List<Cell>> m_PlayerLegalMove;
        public event EventHandler GameOver;
        public event Action<string> TurnSkipped;
        public event Action<string, int, int> CellColorChanged;


        public GameManager(int i_BoardSize, bool i_IsComputer)
        {
            r_Player1 = new Player();
            r_Player2 = new Player(r_Player1.PlayerColor, i_IsComputer);
            r_GameBoard = new Board(i_BoardSize);
            m_CurrentPlayer = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1 : r_Player2;
            m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);
        }

        public Board GameBoard
        {
            get { return r_GameBoard; }
        }

        public Dictionary<Cell, List<Cell>> LegalMoves
        {
            get { return m_PlayerLegalMove; }
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

        protected virtual void OnGameOver(object sender, EventArgs e)
        {
            if (GameOver != null)
            {
                GameOver.Invoke(sender, e);
            }

        }

        protected virtual void OnTurnSkipped(string i_Color)
        {
            if (TurnSkipped != null)
            {
                TurnSkipped.Invoke(i_Color);
            }
        }

        protected virtual void OnCellColorChanged(string i_Color, int i_Row, int i_Col)
        {
            if (CellColorChanged != null)
            {
                CellColorChanged.Invoke(i_Color, i_Row, i_Col);
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
            r_GameBoard.Cells[i_Cell.Row, i_Cell.Col].CurrentColor = CurrentPlayer.PlayerColor;
            CellColorChanged?.Invoke(m_CurrentPlayer.PlayerColor.ToString(), i_Cell.Row, i_Cell.Col);
            flipDiscs(capturbaleCells);
            r_GameBoard.UpdatesScore(movePlayer, capturbaleCells.Count);
            passTurn();
            return true;
        }

        private List<Cell> capturbaleCellsInline(Cell i_Cell, Player i_Player, int i_RowOffSet, int i_ColOffSet)
        {
            List<Cell> capturbaleCells = new List<Cell>();
            int row = i_Cell.Row + i_RowOffSet;
            int col = i_Cell.Col + i_ColOffSet;
            bool capturableCellsFound = false;

            while (isInsideBoard(row, col) && r_GameBoard.Cells[row, col].CurrentColor != Player.eColor.None)
            {
                if (r_GameBoard.Cells[row, col].CurrentColor == OpponentPlayerColor(i_Player.PlayerColor))
                {
                    capturbaleCells.Add(new Cell(row, col));
                    row += i_RowOffSet;
                    col += i_ColOffSet;
                }
                else
                {
                    capturableCellsFound = true;
                    break;
                }
            }

            return capturableCellsFound ? capturbaleCells : new List<Cell>();
        }
        
        public static Player.eColor OpponentPlayerColor(Player.eColor i_PlayerColor)
        {
            Player.eColor opponentColor;

            switch (i_PlayerColor)
            {
                case Player.eColor.White:
                    {
                        opponentColor = Player.eColor.Black;
                        break;
                    }
                case Player.eColor.Black:
                    {
                        opponentColor = Player.eColor.White;
                        break;
                    }
                default:
                    {
                        opponentColor = Player.eColor.None;
                        break;
                    }
            }

            return opponentColor;
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
            bool isMoveLegal = false;
            if (r_GameBoard.Cells[i_Cell.Row, i_Cell.Col].CurrentColor != Player.eColor.None)
            {
                o_CapturbaleCells = null;
            }
            else
            {
                o_CapturbaleCells = this.capturbaleCells(i_Cell, i_Player);
                isMoveLegal = o_CapturbaleCells.Count > 0;
            }

            return isMoveLegal;
        }

        private bool isInsideBoard(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < r_GameBoard.BoardSize && i_Col >= 0 && i_Col < r_GameBoard.BoardSize;
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == r_Player1 ? r_Player2 : r_Player1;
            foreach(Cell cell in m_PlayerLegalMove.Keys)
            {
                OnCellColorChanged("None", cell.Row, cell.Col);
            }
            m_PlayerLegalMove = findLegalMoves(CurrentPlayer);
        }

        public string GetWinnerPlayerName()
        {
            string winnerName = "NO ONE :(";
            if (r_GameBoard.BlackCount > r_GameBoard.WhiteCount)
            {
                winnerName = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1.PlayerName : r_Player2.PlayerName;
            }

            if (r_GameBoard.WhiteCount > r_GameBoard.BlackCount)
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
                OnGameOver(this, EventArgs.Empty);
            }
            else
            {
                OnTurnSkipped(message);
            }
        }

        private void flipDiscs(List<Cell> i_Cells)
        {
            foreach (Cell cell in i_Cells)
            {
                r_GameBoard.Cells[cell.Row, cell.Col].CurrentColor = m_CurrentPlayer.PlayerColor;
                OnCellColorChanged(m_CurrentPlayer.PlayerColor.ToString(),cell.Row,cell.Col);
            }
        }

        private Dictionary<Cell, List<Cell>> findLegalMoves(Player i_Player)
        {
            Dictionary<Cell, List<Cell>> legalMoves = new Dictionary<Cell, List<Cell>>();
            for (int row = 0; row < r_GameBoard.BoardSize; row++)
            {
                for (int col = 0; col < r_GameBoard.BoardSize; col++)
                {
                    Cell cellToAdd = new Cell(row, col);
               
                    if (isMoveLegal(i_Player, cellToAdd, out List<Cell> capturbaleCells))
                    {
                        legalMoves[cellToAdd] = capturbaleCells;
                        OnCellColorChanged("Green", cellToAdd.Row, cellToAdd.Col);
                    }
                }
            }
            
            return legalMoves;
        }


    }
}

