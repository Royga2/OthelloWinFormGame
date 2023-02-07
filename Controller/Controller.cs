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
        bool m_IsComputer;
        private UIManager m_UIManager; 

        public Controller()
        {
            m_UIManager = new UIManager();
            m_BoardSize = m_UIManager.GameSettingForm.BoardSize;
            m_IsComputer = m_UIManager.GameSettingForm.IsAgainstComputer;
            r_GameManager = new GameManager(m_BoardSize, m_IsComputer);


            m_UIManager.GameForm.OnPictureBoxClicked += OnClickMoveHandler;
            UpdateUIBoard();
            //m_UIManager.StartGameDialog();
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
            bool wantToPlay = true;
            while (wantToPlay)
            {
                m_UIManager.StartGameDialog();
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
                        //UpdateUIBoard();
                    }
                    else
                    {
                        //Console.WriteLine("Press any key to continue...");
                        //Console.ReadKey();
                        Random randomMove = new Random();
                        ICollection<Cell> keys = r_GameManager.PlayerLegalMove.Keys;
                        Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
                        r_GameManager.MakeMove(randomKey);
                    }
                }
                //r_GameManager.Winner = r_GameManager.getWinnerPlayerName();
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

                    //int currentBoardSize = m_GameBoard.BoardSize;
                    //m_GameBoard = new Board(currentBoardSize, r_Player1.PlayerName, r_Player2.PlayerName);
                    //m_GameOver = false;
                    //m_CurrentPlayer = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1 : r_Player2;
                    //m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);
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
                m_UIManager.GameForm.ChangeGameFormTitle(r_GameManager.CurrentPlayer.PlayerColor.ToString());
            }

        }

        public void GetLegalMoves()
        {
            Dictionary<Cell, List<Cell>> currentLegalMoves = r_GameManager.LegalMoves;
        }

    }
}
