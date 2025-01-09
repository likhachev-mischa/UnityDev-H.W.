using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class SnakeExpandController : IInitializable, IDisposable
    {
        private ICoinSpawner m_spawner;
        private ISnake m_snake;

        public SnakeExpandController(ICoinSpawner spawner, ISnake snake)
        {
            m_spawner = spawner;
            m_snake = snake;
        }

        void IInitializable.Initialize()
        {
            m_spawner.CoinDespawned += OnCoinDespawned;
        }

        void IDisposable.Dispose()
        {
            m_spawner.CoinDespawned -= OnCoinDespawned;
        }

        private void OnCoinDespawned(ICoin obj)
        {
            m_snake.Expand(obj.Bones);
        }
    }
}