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
        private readonly GameStateController m_gameStateController;
        
        public SnakeCollisionObserver(ISnake snake, IWorldBounds worldBounds, GameStateController gameStateController)
        {
            m_snake = snake;
            m_worldBounds = worldBounds;
            m_gameStateController = gameStateController;
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
            m_gameStateController.LoseGame();
        }
    }
}