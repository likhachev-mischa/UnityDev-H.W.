using System;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeCollisionObserver: IInitializable, IDisposable
    {
        private readonly ISnake m_snake;
        private readonly IWorldBounds m_worldBounds;
        private readonly GameCycle m_gameCycle;
        
        public SnakeCollisionObserver(ISnake snake, IWorldBounds worldBounds, GameCycle gameCycle)
        {
            m_snake = snake;
            m_worldBounds = worldBounds;
            m_gameCycle = gameCycle;
        }
        
        public void Initialize()
        {
            m_snake.OnMoved += OnSnakeMoved;
            m_snake.OnSelfCollided += OnSnakeCollision;
        }
        
        public void Dispose()
        {
           m_snake.OnMoved -= OnSnakeMoved; 
           m_snake.OnSelfCollided -= OnSnakeCollision;
        }

        private void OnSnakeMoved(Vector2Int snakePos)
        {
            if (!m_worldBounds.IsInBounds(snakePos))
            {
                OnSnakeCollision();
            }
        }
        
        private void OnSnakeCollision()
        {
            m_snake.SetActive(false);
            m_gameCycle.LoseGame();
        }
    }
}