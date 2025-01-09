using System;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeCollectCoinObserver : IInitializable, IDisposable
    {
        private readonly ISnake m_snake;
        private readonly ICoinSpawner m_coinSpawner;

        public SnakeCollectCoinObserver(ISnake snake, ICoinSpawner coinSpawner)
        {
            m_snake = snake;
            m_coinSpawner = coinSpawner;
        }

        void IInitializable.Initialize()
        {
            m_snake.OnMoved += OnSnakeMoved;
        }

        void IDisposable.Dispose()
        {
            m_snake.OnMoved -= OnSnakeMoved;
        }
        
        private void OnSnakeMoved(Vector2Int snakePos)
        {
            var coins = m_coinSpawner.Coins;
            ICoin coinToRemove = default;

            foreach (var coin in coins)
            {
                if (coin.Position == snakePos)
                {
                    coinToRemove = coin;
                    break;
                }
            }

            if (coinToRemove != default)
            {
                m_coinSpawner.Despawn(coinToRemove);
            }
        }

    }
}