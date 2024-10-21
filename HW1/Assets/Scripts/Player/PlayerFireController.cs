using UnityEngine;

namespace ShootEmUp
{
    public class PlayerFireController : MonoBehaviour
    {
        [SerializeField]
        private Player m_character;

        [SerializeField]
        private BulletSpawner m_bulletSpawner;

        [SerializeField]
        private InputManager m_inputManager;

        private void OnEnable()
        {
            m_inputManager.FireEvent += OnFireTriggered;
        }

        private void OnFireTriggered()
        {
            m_bulletSpawner.SpawnBullet(
                m_character.FirePoint.position,
                Color.blue,
                (int)PhysicsLayer.PLAYER_BULLET,
                1,
                m_character.FirePoint.rotation * Vector3.up * 3
            );
        }

        private void OnDisable()
        {
            m_inputManager.FireEvent -= OnFireTriggered;
        }
    }
}