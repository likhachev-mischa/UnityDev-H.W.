using UnityEngine;

namespace ShootEmUp
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField]
        public Transform FirePoint { get; private set; }

        [SerializeField]
        private BulletSpawner m_bulletSpawner;

        [SerializeField]
        private PhysicsLayer m_physicsLayer;

        [SerializeField]
        private Color m_color;

        [SerializeField]
        private int m_damage;

        [SerializeField]
        private int m_velocity;

        public void SetBulletSpawner(BulletSpawner bulletSpawner)
        {
            m_bulletSpawner = bulletSpawner;
        }

        public void Fire(in Vector2 direction)
        {
            m_bulletSpawner.SpawnBullet(
                FirePoint.position,
                m_damage,
                m_color,
                (int)m_physicsLayer,
                FirePoint.rotation * direction * m_velocity
            );
        }
    }
}