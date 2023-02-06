using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OthelloLogic;
using OthelloWinFormGame;

namespace OthelloController
{
    class Controller
    {
        private readonly GameManager r_GameManager;
        private int m_BoardSize;
        private UIManager m_UIManager; 

        public Controller()
        {
            m_UIManager = new UIManager();
            m_BoardSize = m_UIManager.GameSettingForm.BoardSize;
            bool i_IsComputer = m_UIManager.GameSettingForm.IsAgainstComputer;
            r_GameManager = new GameManager(m_BoardSize, i_IsComputer);
            m_UIManager.GameForm.OnPictureBoxClicked += OnClickMoveHandler;
            UpdateUIBoard();
            m_UIManager.StartGameDialog();
        }

        private void UpdateUIBoard()
        {
            foreach(Cell currentCell in r_GameManager.GameBoard.Cells)
            {
                if(currentCell.CurrentColor != Player.eColor.None)
                {
                    m_UIManager.GameForm.UpdateTablePictureBox(currentCell.CurrentColor.ToString(), currentCell.Row, currentCell.Col);
                }
            }
        }

        public void PlayGame()
        {
            bool wantToPlay = true;
            while (wantToPlay)
            {
                while (r_GameManager.GameOver == false)
                {

                    if (r_GameManager.CurrentPlayer.IsComputer == false)
                    {
                        //m_UIManager.GameForm.PictureBox_Click += ;
                        //string playerInputMove = MoveInput();
                        //if (playerInputMove[0] == 'Q')

                        //int[] userInput = getUserInputInInts(playerInputMove);
                        //Cell userCoicheCell = new Cell(userInput[0], userInput[1]);
                        //MakeMove(userCoicheCell);
                    }
                    else
                    {
                        //Console.WriteLine("Press any key to continue...");
                        //Console.ReadKey();
                        Random randomMove = new Random();
                        ICollection<Cell> keys = m_PlayerLegalMove.Keys;
                        Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
                        MakeMove(randomKey);
                    }
                }
                m_Winner = getWinnerPlayerName();
                //                Console.WriteLine(@"Game Over...
                //The final score is:
                //{0}(Black): {1} - {2}(White): {3}

                //AND THE WINNER IS... *** {4} *** !!!
                //", r_Player1.PlayerName, m_GameBoard.BlackCount, r_Player2.PlayerName, m_GameBoard.WhiteCount, m_Winner);
                //                Console.WriteLine(
                //                    @"Do you want a REMATCH?
                //press 0 to end session or 1 to play again");
                //bool playRematch = inputGameMode();
                //if (playRematch == false)
                {

                    //                    Console.WriteLine(@"Thank you for playing!
                    //Have a good day,
                    //Bye-Bye :)");
                    wantToPlay = false;
                }
                //else
                {

                    int currentBoardSize = m_GameBoard.BoardSize;
                    m_GameBoard = new Board(currentBoardSize, r_Player1.PlayerName, r_Player2.PlayerName);
                    m_GameOver = false;
                    m_CurrentPlayer = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1 : r_Player2;
                    m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);
                }
            }
        }

        public void OnClickMoveHandler(int i_Row, int i_Col)
        {
            Cell CurrentMove = new Cell(i_Row, i_Col);
            r_GameManager.MakeMove(CurrentMove);
        }

    }
}
