using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private Bullet m_prefab;

        [SerializeField]
        private Transform m_worldTransform;

        [SerializeField]
        private LevelBounds m_levelBounds;

        [SerializeField]
        private Transform m_container;

        private readonly List<Bullet> m_bullets = new();
        private List<int> m_toBeRemovedIdxs = new();

        private ObjectPool<Bullet> m_bulletPool;

        public Bullet SpawnBullet()
        {
            Bullet bullet = m_bulletPool.GetObject();
            bullet.transform.SetParent(m_worldTransform);
            m_bullets.Add(bullet);
            bullet.OnCollisionEntered += OnBulletCollision;
            return bullet;
        }

        private void Awake()
        {
            m_bulletPool = new ObjectPool<Bullet>(m_prefab, 20, m_container);
        }

        private void FixedUpdate()
        {
            for (int i = 0, count = m_bullets.Count; i < count; i++)
            {
                Bullet bullet = m_bullets[i];
                if (!m_levelBounds.InBounds(bullet.transform.position))
                {
                    m_toBeRemovedIdxs.Add(i);
                }
            }

            foreach (int i in m_toBeRemovedIdxs)
            {
                RemoveBullet(m_bullets[i]);
            }

            m_toBeRemovedIdxs.Clear();
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            DealDamage(bullet, collision.gameObject);
            bullet.transform.SetParent(m_container);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            m_bullets.Remove(bullet);
            bullet.OnCollisionEntered -= OnBulletCollision;
            bullet.transform.SetParent(m_container);
            m_bulletPool.ReturnObject(bullet);
        }

        //cringe
        private void DealDamage(Bullet bullet, GameObject other)
        {
            int damage = bullet.Damage;
            if (damage <= 0)
                return;

            if (other.TryGetComponent(out Player player))
            {
                player.DecreaseHealth(damage);
            }
            else if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Health = Mathf.Max(0, enemy.Health - damage);
            }
        }

        private void OnDestroy()
        {
            m_bulletPool.Dispose();
        }
    }
}