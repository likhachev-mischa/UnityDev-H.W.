using System;
using UnityEngine;

namespace ShootEmUp
{
    public class InputManager : MonoBehaviour
    {
        public event Action FireEvent;
        public float InputDirection { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FireEvent?.Invoke();

            if (Input.GetKey(KeyCode.LeftArrow))
                InputDirection = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
                InputDirection = 1;
            else
                InputDirection = 0;
        }
    }
}