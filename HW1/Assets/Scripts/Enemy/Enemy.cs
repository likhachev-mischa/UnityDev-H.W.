using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        [field: SerializeField]
        public Ship Ship { get; private set; }

        [field:SerializeField]
        public Weapon Weapon { get; private set; }

    [SerializeField]
        private float m_countdown;

        private Ship m_target;

        private Vector2 m_destination;
        private float m_currentTime;
        private bool m_isPointReached;
        private int m_initialHealth;

        public void SetDestination(Vector2 endPoint)
        {
            m_destination = endPoint;
            m_isPointReached = false;
        }

        public void SetTarget(Ship target)
        {
            m_target = target;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetPosition(in Vector2 position)
        {
            transform.position = position;
        }

        private void Awake()
        {
            m_initialHealth = Ship.Health;
        }

        private void OnEnable()
        {
            Ship.SetHealth(m_initialHealth);
        }
        
        private void FixedUpdate()
        {
            if (m_isPointReached)
            {
                //Attack:
                if (m_target.Health <= 0)
                    return;

                m_currentTime -= Time.fixedDeltaTime;
                if (m_currentTime <= 0)
                {
                    Vector2 startPosition = Weapon.FirePoint.position;
                    Vector2 vector = (Vector2)m_target.transform.position - startPosition;
                    Vector2 direction = vector.normalized;
                    Weapon.Fire(direction);

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

                Vector2 direction = vector.normalized;
                Ship.Move(direction);
            }
        }
    }
}