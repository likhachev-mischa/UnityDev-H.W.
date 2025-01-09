using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class SnakeSpeedController: IInitializable, IDisposable
    {
        private ISnake m_snake;
        private IDifficulty m_difficulty;

        public SnakeSpeedController(ISnake snake, IDifficulty difficulty)
        {
            m_snake = snake;
            m_difficulty = difficulty;
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
            m_snake.SetSpeed(m_difficulty.Current);
        }
    }
}