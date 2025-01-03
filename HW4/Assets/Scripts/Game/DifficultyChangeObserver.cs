using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class DifficultyChangeObserver : IInitializable, IDisposable
    {
        private readonly IDifficulty m_difficulty;

        private readonly ISnake m_snake;
        private readonly ICoinSpawner m_coinSpawner;

        public DifficultyChangeObserver(IDifficulty difficulty, ISnake snake, ICoinSpawner coinSpawner)
        {
            m_difficulty = difficulty;
            m_snake = snake;
            m_coinSpawner = coinSpawner;
        }

        void IInitializable.Initialize()
        {
            m_difficulty.OnStateChanged += SetSpeedOnDifficultyChanged;
            m_difficulty.OnStateChanged += SpawnCoinsOnDifficultyChanged;
        }

        void IDisposable.Dispose()
        {
            m_difficulty.OnStateChanged -= SetSpeedOnDifficultyChanged;
            m_difficulty.OnStateChanged -= SpawnCoinsOnDifficultyChanged;
        }

        private void SetSpeedOnDifficultyChanged()
        {
            m_snake.SetSpeed(m_difficulty.Current);
        }

        private void SpawnCoinsOnDifficultyChanged()
        {
            for (int i = 0; i < m_difficulty.Current; ++i)
            {
                m_coinSpawner.Spawn();
            }
        }
    }
}