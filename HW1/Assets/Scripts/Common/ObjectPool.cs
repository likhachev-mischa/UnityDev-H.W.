using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public class ObjectPool<T> : IDisposable where T : MonoBehaviour
    {
        private T m_prefab;

        private List<int> m_availableObjectIdxs;
        private List<T> m_objects;

        private int m_resizeStep;

        public ObjectPool(T prefab, int initialCapacity, Transform parent, int resizeStep = 5)
        {
            m_prefab = prefab;
            m_resizeStep = resizeStep;

            m_availableObjectIdxs = new List<int>(initialCapacity);
            m_objects = new List<T>(initialCapacity);

            for (int i = 0; i < initialCapacity; ++i)
            {
                T obj = Object.Instantiate(m_prefab, parent);
                obj.gameObject.SetActive(false);
                m_objects.Add(obj);
                m_availableObjectIdxs.Add(i);
            }
        }

        public T GetObject()
        {
            if (m_availableObjectIdxs.Count == 0)
            {
                Resize();
            }

            T result = m_objects[m_availableObjectIdxs[0]];
            result.gameObject.SetActive(true);
            m_availableObjectIdxs.RemoveAt(0);
            return result;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            int idx = m_objects.IndexOf(obj);
            m_availableObjectIdxs.Add(idx);
        }

        public void Dispose()
        {
            foreach (T monoBehaviour in m_objects)
            {
                Object.Destroy(monoBehaviour);
            }

            m_objects.Clear();
        }

        private void Resize()
        {
            for (int i = 0; i < m_resizeStep; ++i)
            {
                T obj = Object.Instantiate(m_prefab);
                obj.gameObject.SetActive(false);
                m_objects.Add(obj);
                m_availableObjectIdxs.Add(m_objects.Count - 1);
            }
        }
    }
}