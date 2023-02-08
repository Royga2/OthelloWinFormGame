using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OthelloLogic
{
    public class Board
    {
        private readonly int r_BoardSize;
        private readonly Cell[,] r_Cells;
        private int m_BlackCount;
        private int m_WhiteCount;


        public Board(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_Cells = new Cell[r_BoardSize, r_BoardSize];
            m_BlackCount = 2;
            m_WhiteCount = 2;

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_Cells[i, j] = new Cell(i, j);
                }
            }

            initializedCells();
        }

        public Board(string i_FirstPlayerName, string i_SecondPlayerName)
        {
            r_Cells = new Cell[r_BoardSize, r_BoardSize];
            m_BlackCount = 2;
            m_WhiteCount = 2;

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_Cells[i, j] = new Cell(i, j);
                }
            }

            initializedCells();
        }

        private void initializedCells()
        {
            r_Cells[r_BoardSize / 2 - 1, r_BoardSize / 2 - 1].CurrentColor = Player.eColor.White;
            r_Cells[r_BoardSize / 2, r_BoardSize / 2].CurrentColor = Player.eColor.White;
            r_Cells[r_BoardSize / 2 - 1, r_BoardSize / 2].CurrentColor = Player.eColor.Black;
            r_Cells[r_BoardSize / 2, r_BoardSize / 2 - 1].CurrentColor = Player.eColor.Black;
        }

        public Cell[,] Cells
        {
            get { return r_Cells; }
        }

        public int BlackCount
        {
            get { return m_BlackCount; }
            set { m_BlackCount = value; }
        }

        public int WhiteCount
        {
            get{ return m_WhiteCount; }
            set { m_WhiteCount = value; }
        }

        public int BoardSize
        {
            get
            { return r_BoardSize; }
        }

        public void UpdatesScore(Player i_MovePlayer, int i_CapturbaleCellsCount)
        {
            if (i_MovePlayer.PlayerColor == Player.eColor.Black)
            {
                m_BlackCount += i_CapturbaleCellsCount + 1;
                m_WhiteCount -= i_CapturbaleCellsCount;
            }
            else
            {
                m_WhiteCount += i_CapturbaleCellsCount + 1;
                m_BlackCount -= i_CapturbaleCellsCount;
            }
        }
    }
}

