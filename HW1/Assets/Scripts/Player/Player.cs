using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour
    {
        public Action OnHealthEmpty;
        
        [SerializeField]
        public Transform FirePoint;

        [field: SerializeField]
        public int Health { get; private set; }

        [SerializeField]
        public Rigidbody2D Rigidbody;

        [SerializeField]
        public float Speed = 5.0f;

        public void DecreaseHealth(int value)
        {
            Health -= value;
            if (Health <= 0)
            {
                OnHealthEmpty?.Invoke();
            }
        }
    }
}