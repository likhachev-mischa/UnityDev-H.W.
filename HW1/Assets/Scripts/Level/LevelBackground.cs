using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        private Params m_params;

        private float m_startPositionY;
        private float m_endPositionY;
        private float m_movingSpeedY;
        private float m_positionX;
        private float m_positionZ;

        private Transform m_transform;

        private void Awake()
        {
            m_startPositionY = m_params.StartPositionY;
            m_endPositionY = m_params.EndPositionY;
            m_movingSpeedY = m_params.MovingSpeedY;
            m_transform = transform;
            var position = m_transform.position;
            m_positionX = position.x;
            m_positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (m_transform.position.y <= this.m_endPositionY)
            {
                m_transform.position = new Vector3(
                    m_positionX,
                    m_startPositionY,
                    m_positionZ
                );
            }

            m_transform.position -= new Vector3(
                m_positionX,
                m_movingSpeedY * Time.fixedDeltaTime,
                m_positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField]
            public float StartPositionY;

            [SerializeField]
            public float EndPositionY;

            [SerializeField]
            public float MovingSpeedY;
        }
    }
}