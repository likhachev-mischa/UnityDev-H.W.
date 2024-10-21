using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private Player m_character;

        [SerializeField]
        private InputManager m_inputManager;

        private void FixedUpdate()
        {
            Vector2 moveDirection = new Vector2(m_inputManager.InputDirection, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * m_character.Speed;
            Vector2 targetPosition = m_character.Rigidbody.position + moveStep;
            m_character.Rigidbody.MovePosition(targetPosition);
        }
    }
}