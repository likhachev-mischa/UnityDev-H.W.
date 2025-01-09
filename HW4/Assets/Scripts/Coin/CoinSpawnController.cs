using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class CoinSpawnController : IInitializable, IDisposable
    {
        private IDifficulty m_difficulty;
        private ICoinSpawner m_coinSpawner;

        public CoinSpawnController(IDifficulty difficulty, ICoinSpawner coinSpawner)
        {
            m_difficulty = difficulty;
            m_coinSpawner = coinSpawner;
        }

        void IInitializable.Initialize()
        {
            m_difficulty.OnStateChanged += OnDifficultyChanged;
        }

        void IDisposable.Dispose()
        {
            m_difficulty.OnStateChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            for (int i = 0, count = m_difficulty.Current; i < count; ++i)
            {
                m_coinSpawner.Spawn();
            }
        }
    }
}