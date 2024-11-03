using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Count { get; private set; }

        private List<Item> m_items = new();

        private Item[,] m_matrix;

        public Inventory(in int width, in int height)
        {
            if (width < 0 || height < 0 || (width == 0 && height == 0))
            {
                throw new ArgumentException("Invalid inventory size!");
            }

            Width = width;
            Height = height;
            Count = 0;
            m_matrix = new Item[Width, Height];
        }

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentException("Items are null!");
            }

            foreach ((Item key, Vector2Int value) in items)
            {
                AddItem(key, value);
            }
        }

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentException("Items are null!");
            }

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentException("Items are null!");
            }

            foreach ((Item key, Vector2Int value) in items)
            {
                AddItem(key, value);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentException("Items are null!");
            }

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            return CanAddItem(item, position.x, position.y);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY)
        {
            if (!IsItemOk(item))
            {
                return false;
            }

            if (Contains(item))
            {
                return false;
            }

            return CanAddItem(item.Size, posX, posY);
        }

        public bool CanAddItem(in Vector2Int itemSize, in int posX, in int posY)
        {
            if (posX < 0 || posY < 0 || posX + itemSize.x > Width || posY + itemSize.y > Height)
            {
                return false;
            }

            bool canFit = true;

            for (int w = posX; w < posX + itemSize.x; ++w)
            for (int h = posY; h < posY + itemSize.y; ++h)
            {
                canFit &= (m_matrix[w, h] == null);
                if (!canFit)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            return AddItem(item, position.x, position.y);
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            if (!IsItemOk(item))
            {
                return false;
            }

            if (Contains(item))
            {
                return false;
            }

            if (!CanAddItem(item, posX, posY))
            {
                return false;
            }

            m_items.Add(item);
            SetItemPositionMatrix(item, posX, posY, item.Size.x, item.Size.y);
            ++Count;
            OnAdded?.Invoke(item, new Vector2Int(posX, posY));
            return true;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (!IsItemOk(item))
            {
                return false;
            }

            if (Contains(item))
            {
                return false;
            }

            int itemWidth = item.Size.x;
            int itemHeight = item.Size.y;
            for (int w = 0; w < Width - itemWidth; ++w)
            {
                for (int h = 0; h < Height - itemHeight; ++h)
                {
                    if (CanAddItem(item.Size, w, h))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null)
            {
                return false;
            }

            if (item.Size.x <= 0 || item.Size.y <= 0 || item.Size.x > Width || item.Size.y > Height)
            {
                throw new ArgumentException("Invalid item size!");
            }

            if (!FindFreePosition(item, out Vector2Int freePosition))
            {
                return false;
            }

            return AddItem(item, freePosition);
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Item item, out Vector2Int freePosition)
        {
            return FindFreePosition(item.Size.x, item.Size.y, out freePosition);
        }

        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            return FindFreePosition(size.x, size.y, out freePosition);
        }

        public bool FindFreePosition(in int sizeX, int sizeY, out Vector2Int freePosition)
        {
            if (!IsSizeOk(new Vector2Int(sizeX, sizeY)))
            {
                freePosition = default;
                return false;
            }

            for (int h = 0; h <= Height - sizeY; ++h)
            for (int w = 0; w <= Width - sizeX; ++w)
            {
                if (CanAddItem(new Vector2Int(sizeX, sizeY), w, h))
                {
                    freePosition = new Vector2Int(w, h);
                    return true;
                }
            }

            freePosition = default;
            return false;
        }

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            foreach (Item key in m_items)
            {
                if (item.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            return IsOccupied(position.x, position.y);
        }

        public bool IsOccupied(in int x, in int y)
        {
            return m_matrix[x, y] != null;
        }

        /// <summary>
        /// Checks if the a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            return !IsOccupied(position);
        }

        public bool IsFree(in int x, in int y)
        {
            return !IsOccupied(x, y);
        }

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            return RemoveItem(item, out _);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            if (!Contains(item))
            {
                position = default;
                return false;
            }

            position = GetPositions(item)[0];
            m_items.Remove(item);
            SetItemPositionMatrix(null, position.x, position.y, item.Size.x, item.Size.y);
            --Count;
            OnRemoved?.Invoke(item, position);
            return true;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            return GetItem(position.x, position.y);
        }

        public Item GetItem(in int x, in int y)
        {
            if (m_matrix[x, y] == null)
            {
                throw new NullReferenceException("Item is null!");
            }

            return m_matrix[x, y];
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            return TryGetItem(position.x, position.y, out item);
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Width || m_matrix[x, y] == null)
            {
                item = default;
                return false;
            }

            item = m_matrix[x, y];
            return true;
        }

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Item is null!");
            }

            if (!Contains(item))
            {
                throw new KeyNotFoundException("Item not found!");
            }

            Vector2Int[] result = new Vector2Int[item.Size.x * item.Size.y];
            int resultIdx = 0;
            for (int w = 0; w < Width; ++w)
            {
                for (int h = 0; h < Height; ++h)
                {
                    if (m_matrix[w, h] != null && m_matrix[w, h].Equals(item))
                    {
                        result[resultIdx] = new Vector2Int(w, h);
                        ++resultIdx;
                    }
                }
            }

            return result;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            if (item == null || !Contains(item))
            {
                positions = default;
                return false;
            }

            positions = GetPositions(item);
            return true;
        }

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if (Count == 0)
            {
                return;
            }

            m_items.Clear();
            for (int w = 0; w < Width; ++w)
            for (int h = 0; h < Height; ++h)
            {
                m_matrix[w, h] = null;
            }

            OnCleared?.Invoke();
            Count = 0;
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;
            foreach (Item item in m_items)
            {
                if (item.Name == null)
                {
                    count = name == null ? ++count : count;
                }
                else if (item.Name.Equals(name))
                {
                    ++count;
                }
            }

            return count;
        }

        /// <summary>
        /// Moves a specified item at target position if exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int position)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "is null!");
            }

            if (!Contains(item) || position.x + item.Size.x > Width ||
                position.y + item.Size.y > Height || position.x < 0 || position.y < 0)
            {
                return false;
            }

            for (int w = position.x; w <= position.x + item.Size.x; ++w)
            for (int h = position.y; h <= position.y + item.Size.y; ++h)
            {
                if (IsOccupied(w, h) && !m_matrix[w, h].Equals(item))
                {
                    return false;
                }
            }

            Vector2Int[] positions = GetPositions(item);
            foreach (Vector2Int vec in positions)
            {
                m_matrix[vec.x, vec.y] = null;
            }

            SetItemPositionMatrix(item, position.x, position.y, item.Size.x, item.Size.y);
            OnMoved?.Invoke(item, position);
            return true;
        }

        /// <summary>
        /// Reorganizes a inventory space so that the free area is uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            for (int w = 0; w < Width; ++w)
            for (int h = 0; h < Height; ++h)
            {
                m_matrix[w, h] = null;
            }

            var sortedItems = m_items.OrderByDescending(i => i, new ItemComparer());

            foreach (Item item in sortedItems)
            {
                FindFreePosition(item, out Vector2Int pos);
                SetItemPositionMatrix(item, pos.x, pos.y, item.Size.x, item.Size.y);
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentException("Matrix is null!");
            }

            if (matrix.GetLength(0) < m_matrix.GetLength(0) ||
                matrix.GetLength(1) < m_matrix.GetLength(1))
            {
                throw new ArgumentException("Matrix size is invalid!");
            }

            for (int w = 0; w < Width; ++w)
            for (int h = 0; h < Height; ++h)
            {
                matrix[w, h] = m_matrix[w, h];
            }
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        private void SetItemPositionMatrix(Item value, in int posX, in int posY,
            in int sizeX, in int sizeY)
        {
            for (int w = posX; w < posX + sizeX; ++w)
            {
                for (int h = posY; h < posY + sizeY; ++h)
                {
                    m_matrix[w, h] = value;
                }
            }
        }

        private bool IsItemOk(Item item)
        {
            if (item == null)
            {
                return false;
            }

            return IsSizeOk(item.Size);
        }

        private bool IsSizeOk(in Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentException("Invalid size!");
            }

            if (size.x > Width || size.y > Height)
            {
                return false;
            }

            return true;
        }
    }
}