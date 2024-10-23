using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private Ship m_playerShip;

        [SerializeField]
        private InputManager m_inputManager;

        private void FixedUpdate()
        {
            m_playerShip.Move(new Vector2(m_inputManager.InputDirection,0));
        }
    }
}