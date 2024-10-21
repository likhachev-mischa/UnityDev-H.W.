using UnityEngine;

namespace ShootEmUp
{
    public class PlayerHealthObserver : MonoBehaviour
    {
        [SerializeField]
        private GameManager m_gameManager;

        [SerializeField]
        private Player m_player;

        private void OnEnable()
        {
            m_player.OnHealthEmpty += OnPlayerHealthEmpty;
        }

        private void OnPlayerHealthEmpty()
        {
            m_gameManager.FinishGame();
        }

        private void OnDisable()
        {
            m_player.OnHealthEmpty -= OnPlayerHealthEmpty;
        }
    }
}