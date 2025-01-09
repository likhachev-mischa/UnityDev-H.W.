using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class InputAdapter : IInputAdapter, ITickable
    {
        public SnakeDirection Direction { get; private set; }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Direction = SnakeDirection.UP;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Direction = SnakeDirection.DOWN;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Direction = SnakeDirection.LEFT;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Direction = SnakeDirection.RIGHT;
            }
        }
    }
}