using System;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeCollectCoinObserver: IInitializable, IDisposable
    {
        public event Action<ICoin> CoinCollected;
        
        private readonly ISnake m_snake;
        private readonly ICoinSpawner m_coinSpawner;
        
        public SnakeCollectCoinObserver(ISnake snake, ICoinSpawner coinSpawner)
        {
            m_snake = snake;
            m_coinSpawner = coinSpawner;
        }

        public void Initialize()
        {
            m_snake.OnMoved += OnSnakeMoved;
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
                CoinCollected?.Invoke(coinToRemove);
            }
            
        }

        public void Dispose()
        {
            m_snake.OnMoved -= OnSnakeMoved;
        }

    }
}