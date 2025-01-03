using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class InputController : IInputController, ITickable
    {
        public Vector2Int Direction { get; private set; }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Direction = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Direction = Vector2Int.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Direction = Vector2Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Direction = Vector2Int.right;
            }
        }
    }
}