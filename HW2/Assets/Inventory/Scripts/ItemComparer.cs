using System.Collections.Generic;

namespace Inventories
{
    public sealed class ItemComparer : Comparer<Item>
    {
        public override int Compare(Item x, Item y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            int sizeX = x.Size.x * x.Size.y;
            int sizeY = y.Size.x * y.Size.y;
            int result = sizeX > sizeY ? 1 : sizeX < sizeY ? -1 : 0;
            return result;
        }
    }
}