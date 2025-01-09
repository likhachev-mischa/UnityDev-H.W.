using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class DifficultyController: IInitializable, IDisposable
    {
        private IDifficulty m_difficulty;
        private ICoinSpawner m_coinSpawner;

        private GameCycle m_gameCycle;

        public DifficultyController(IDifficulty difficulty, ICoinSpawner coinSpawner, GameCycle gameCycle)
        {
            m_difficulty = difficulty;
            m_coinSpawner = coinSpawner;
            m_gameCycle = gameCycle;
        }

        void IInitializable.Initialize()
        {
            m_coinSpawner.CoinDespawned += OnCoinDespawned;
            m_gameCycle.GameStarted += OnGameStarted;
        }

        void IDisposable.Dispose()
        {
            m_coinSpawner.CoinDespawned -= OnCoinDespawned;
            m_gameCycle.GameStarted -= OnGameStarted;
        }
        
        private void OnCoinDespawned(ICoin obj)
        {
            if (!m_coinSpawner.IsEmpty())
            {
                return;
            }

            if (!m_difficulty.Next(out _))
            {
               m_gameCycle.WinGame();
            }
        }

        private void OnGameStarted()
        {
            m_difficulty.Next(out _);
        }
    }
}