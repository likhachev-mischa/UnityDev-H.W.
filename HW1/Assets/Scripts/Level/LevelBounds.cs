using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField]
        private Transform m_leftBorder;

        [SerializeField]
        private Transform m_rightBorder;

        [SerializeField]
        private Transform m_downBorder;

        [SerializeField]
        private Transform m_topBorder;
        
        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > m_leftBorder.position.x
                   && positionX < m_rightBorder.position.x
                   && positionY > m_downBorder.position.y
                   && positionY < m_topBorder.position.y;
        }
    }
}