using Modules;
using Zenject;

namespace SnakeGame
{
    public class SnakeTurnController : IFixedTickable
    {
        private readonly ISnake m_snake;
        private readonly IInputAdapter m_inputAdapter;

        public SnakeTurnController(ISnake snake, IInputAdapter inputAdapter)
        {
            m_snake = snake;
            m_inputAdapter = inputAdapter;
        }

        public void FixedTick()
        {
            m_snake.Turn(m_inputAdapter.Direction);
        }
    }
}