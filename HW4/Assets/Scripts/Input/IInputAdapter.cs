using Modules;

namespace SnakeGame
{
    public interface IInputAdapter
    {
        SnakeDirection Direction { get; }
    }
}