using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeMoveController : IFixedTickable
    {
        private readonly ISnake m_snake;
        private readonly IInputController m_inputController;

        public SnakeMoveController(ISnake snake, IInputController inputController)
        {
            m_snake = snake;
            m_inputController = inputController;
        }

        public void FixedTick()
        {
            SnakeDirection snakeDirection;
            Vector2Int inputDirection = m_inputController.Direction;
            
            if (inputDirection == Vector2Int.up)
            {
                snakeDirection = SnakeDirection.UP;
            }
            else if (inputDirection == Vector2Int.down)
            {
                snakeDirection = SnakeDirection.DOWN;
            }
            else if (inputDirection == Vector2Int.left)
            {
                snakeDirection = SnakeDirection.LEFT;
            }
            else if (inputDirection == Vector2Int.right)
            {
                snakeDirection = SnakeDirection.RIGHT;
            }
            else
            {
                return;
            }

            m_snake.Turn(snakeDirection);
        }
    }
}