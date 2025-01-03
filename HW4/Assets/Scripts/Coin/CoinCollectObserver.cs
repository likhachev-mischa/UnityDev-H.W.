using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class CoinCollectObserver : IInitializable, IDisposable
    {
        private readonly SnakeCollectCoinObserver m_snakeCollectCoinObserver;

        private readonly ICoinSpawner m_coinSpawner;

        private readonly IScore m_score;
        private readonly IDifficulty m_difficulty;

        private readonly GameStateController m_gameStateController;

        private readonly ISnake m_snake;

        public CoinCollectObserver(SnakeCollectCoinObserver snakeCollectCoinObserver, ICoinSpawner coinSpawner,
            IScore score, IDifficulty difficulty, GameStateController gameStateController, ISnake snake)
        {
            m_snakeCollectCoinObserver = snakeCollectCoinObserver;
            m_coinSpawner = coinSpawner;
            m_score = score;
            m_difficulty = difficulty;
            m_gameStateController = gameStateController;
            m_snake = snake;
        }

        void IInitializable.Initialize()
        {
            m_snakeCollectCoinObserver.CoinCollected += SetScoreOnCoinCollected;
            m_snakeCollectCoinObserver.CoinCollected += SetDifficultyOnCoinCollected;
            m_snakeCollectCoinObserver.CoinCollected += ExpandSnakeOnCoinCollected;
        }

        void IDisposable.Dispose()
        {
            m_snakeCollectCoinObserver.CoinCollected -= SetScoreOnCoinCollected;
            m_snakeCollectCoinObserver.CoinCollected -= SetDifficultyOnCoinCollected;
            m_snakeCollectCoinObserver.CoinCollected -= ExpandSnakeOnCoinCollected;
        }

        private void SetScoreOnCoinCollected(ICoin obj)
        {
            m_score.Add(obj.Score);
        }

        private void SetDifficultyOnCoinCollected(ICoin obj)
        {
            if (!m_coinSpawner.IsEmpty())
            {
                return;
            }

            if (!m_difficulty.Next(out _))
            {
                m_gameStateController.WinGame();
            }
        }
        
        private void ExpandSnakeOnCoinCollected(ICoin obj)
        {
            m_snake.Expand(obj.Bones);
        }
    }
}