using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private BulletManager m_bulletManager;

        public void SpawnBullet(
            Vector2 position,
            int damage,
            Color color,
            int physicsLayer,
            Vector2 velocity
        )
        {
            Bullet bullet = m_bulletManager.SpawnBullet();

            bullet.SetPosition(position);
            bullet.SetDamage(damage);
            bullet.SetColor(color);
            bullet.SetPhysicsLayer(physicsLayer);
            bullet.SetVelocity(velocity);
        }
    }
}