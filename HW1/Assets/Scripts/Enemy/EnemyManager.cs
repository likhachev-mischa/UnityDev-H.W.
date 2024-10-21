using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private int m_enemyLimit;

        [SerializeField]
        private Transform[] m_spawnPositions;

        [SerializeField]
        private Transform[] m_attackPositions;

        [SerializeField]
        private Player m_character;

        [SerializeField]
        private Transform m_worldTransform;

        [SerializeField]
        private Transform m_container;

        [SerializeField]
        private Enemy m_prefab;

        [SerializeField]
        private BulletSpawner m_bulletSpawner;

        private ObjectPool<Enemy> m_enemyPool;
        private List<Enemy> m_enemies = new();

        private float m_timer;

        private List<int> m_toBeRemovedIdxs = new();

        private void Awake()
        {
            int capacity = 7;
            m_enemyPool = new ObjectPool<Enemy>(m_prefab, capacity, m_container, 1);
            m_enemies.Capacity = capacity;
            m_toBeRemovedIdxs.Capacity = capacity;
            m_timer = Random.Range(1, 2);
        }

        private void SpawnEnemy()
        {
            if (m_enemies.Count > m_enemyLimit)
            {
                return;
            }

            Enemy enemy = m_enemyPool.GetObject();
            m_enemies.Add(enemy);
            enemy.transform.SetParent(m_worldTransform);

            Transform spawnPosition = RandomPoint(this.m_spawnPositions);
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = RandomPoint(this.m_attackPositions);
            enemy.SetDestination(attackPosition.position);
            enemy.Target = m_character;

            if (m_enemies.Count < 5)
            {
                enemy.OnFire += OnFire;
            }
        }

        private void FixedUpdate()
        {
            m_timer -= Time.fixedDeltaTime;
            if (m_timer <= 0)
            {
                SpawnEnemy();
                m_timer = Random.Range(1, 2);
            }


            for (int i = 0; i < m_enemies.Count; ++i)
            {
                Enemy enemy = m_enemies[i];
                if (enemy.Health <= 0)
                {
                    enemy.OnFire -= OnFire;
                    enemy.transform.SetParent(m_container);

                    m_toBeRemovedIdxs.Add(i);
                }
            }

            foreach (int i in m_toBeRemovedIdxs)
            {
                Enemy enemy = m_enemies[i];
                m_enemyPool.ReturnObject(enemy);
                m_enemies.Remove(enemy);
            }

            m_toBeRemovedIdxs.Clear();
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            m_bulletSpawner.SpawnBullet(
                position,
                Color.red,
                (int)PhysicsLayer.ENEMY_BULLET,
                1,
                direction * 2
            );
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }

        private void OnDestroy()
        {
            m_enemyPool.Dispose();
        }
    }
}