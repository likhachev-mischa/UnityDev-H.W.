using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private BulletManager m_bulletManager;

        public void SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            Vector2 velocity
        )
        {
            Bullet bullet = m_bulletManager.SpawnBullet();

            bullet.transform.position = position;
            bullet.SpriteRenderer.color = color;
            bullet.gameObject.layer = physicsLayer;
            bullet.Damage = damage;
            bullet.Rigidbody.velocity = velocity;
        }
    }
}