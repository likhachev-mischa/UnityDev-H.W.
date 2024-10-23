using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Ship : MonoBehaviour
    {
        public Action OnHealthEmpty;

        [field: SerializeField]
        public int Health { get; private set; }

        [SerializeField]
        private Rigidbody2D m_rigidbody;

        [SerializeField]
        private PhysicsLayer m_physicsLayer;

        [SerializeField]
        private float m_speed = 5.0f;

        private void Awake()
        {
            gameObject.layer = (int)m_physicsLayer;
        }

        public void SetHealth(int value)
        {
            Health = value;
            if (Health <= 0)
            {
                OnHealthEmpty?.Invoke();
            }
        }

        public void Move(in Vector2 direction)
        {
            Vector2 moveDirection = direction;
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * m_speed;
            Vector2 targetPosition = m_rigidbody.position + moveStep;
            m_rigidbody.MovePosition(targetPosition);
        }
    }
}