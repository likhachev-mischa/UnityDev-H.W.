using UnityEngine;

namespace ShootEmUp
{
    public class PlayerFireController : MonoBehaviour
    {
        [SerializeField]
        private Weapon m_playerWeapon;

        [SerializeField]
        private InputManager m_inputManager;

        private void OnEnable()
        {
            m_inputManager.FireEvent += OnFireTriggered;
        }

        private void OnFireTriggered()
        {
            m_playerWeapon.Fire(Vector2.up);
        }

        private void OnDisable()
        {
            m_inputManager.FireEvent -= OnFireTriggered;
        }
    }
}