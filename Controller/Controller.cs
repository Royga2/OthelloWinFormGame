using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OthelloLogic;

namespace OthelloController
{
    class Controller
    {
        private readonly GameManager r_GameManager;
        private int m_BoardSize;

        public Controller(int i_BoardSize, bool i_IsComputer)
        {
            r_GameManager = new GameManager(i_BoardSize, i_IsComputer);
        }

    }
}
