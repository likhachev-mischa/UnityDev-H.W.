using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField]
        public Transform FirePoint;

        [SerializeField]
        public int Health;

        [SerializeField]
        public Rigidbody2D Rigidbody;

        [SerializeField]
        public float Speed = 5.0f;

        [NonSerialized]
        public Player Target;

        [SerializeField]
        private float m_countdown;

        private Vector2 m_destination;
        private float m_currentTime;
        private bool m_isPointReached;

        private int m_initialHealth;

        private void Awake()
        {
            m_initialHealth = Health;
        }

        private void OnEnable()
        {
            Health = m_initialHealth;
        }

        public void SetDestination(Vector2 endPoint)
        {
            m_destination = endPoint;
            m_isPointReached = false;
        }

        //cringe
        private void FixedUpdate()
        {
            if (m_isPointReached)
            {
                //Attack:
                if (Target.Health <= 0)
                    return;

                m_currentTime -= Time.fixedDeltaTime;
                if (m_currentTime <= 0)
                {
                    Vector2 startPosition = FirePoint.position;
                    Vector2 vector = (Vector2)Target.transform.position - startPosition;
                    Vector2 direction = vector.normalized;
                    OnFire?.Invoke(startPosition, direction);

                    //       m_currentTime += m_countdown;
                    m_currentTime = m_countdown;
                }
            }
            else
            {
                //Move:
                Vector2 vector = m_destination - (Vector2)transform.position;
                if (vector.magnitude <= 0.25f)
                {
                    m_isPointReached = true;
                    return;
                }

                Vector2 direction = vector.normalized * Time.fixedDeltaTime;
                Vector2 nextPosition = Rigidbody.position + direction * Speed;
                Rigidbody.MovePosition(nextPosition);
            }
        }
    }
}