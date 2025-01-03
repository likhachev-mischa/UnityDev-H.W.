using UnityEngine;

namespace SnakeGame
{
    public interface IInputController
    {
        Vector2Int Direction { get; }
    }
}