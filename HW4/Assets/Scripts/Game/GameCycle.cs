using System;
using UnityEngine;

namespace SnakeGame
{
    public class GameCycle
    {
        public event Action GameStarted;
        public event Action GameWon;
        public event Action GameLost;

        public void StartGame()
        {
            GameStarted?.Invoke();
        }

        public void WinGame()
        {
            GameWon?.Invoke();
            Time.timeScale = 0;
        }

        public void LoseGame()
        {
            GameLost?.Invoke();
            Time.timeScale = 0;
        }
    }
}