using UnityEngine;

namespace ShootEmUp
{
    public class PlayerHealthObserver : MonoBehaviour
    {
        [SerializeField]
        private GameManager m_gameManager;

        [SerializeField]
        private Ship m_playerShip;

        private void OnEnable()
        {
            m_playerShip.OnHealthEmpty += OnPlayerHealthEmpty;
        }

        private void OnPlayerHealthEmpty()
        {
            m_gameManager.FinishGame();
        }

        private void OnDisable()
        {
            m_playerShip.OnHealthEmpty -= OnPlayerHealthEmpty;
        }
    }
}