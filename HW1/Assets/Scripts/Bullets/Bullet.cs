using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        private int m_damage;

        [SerializeField]
        private Rigidbody2D m_rigidbody;

        [SerializeField]
        private SpriteRenderer m_spriteRenderer;

        public void SetPosition(in Vector2 position)
        {
            transform.position = position;
        }

        public void SetDamage(int damage)
        {
            m_damage = damage;
        }

        public void SetColor(in Color color)
        {
            m_spriteRenderer.color = color;
        }

        public void SetPhysicsLayer(int layer)
        {
            gameObject.layer = layer;
        }

        public void SetVelocity(in Vector2 velocity)
        {
            m_rigidbody.velocity = velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            OnCollisionEntered?.Invoke(this, collision);
        }
        
        private void DealDamage(GameObject other)
        {
            if (m_damage <= 0)
            {
                return;
            }

            if (other.TryGetComponent(out Ship ship))
            {
                ship.SetHealth(Mathf.Max(0, ship.Health - m_damage));
            }
        }

    }
}