using System.Collections;
using System.Collections.Generic;

namespace AillieoUtils.EasyBehaviorTree
{
    public struct BBList<T> : IBlackBoardData, IList<T>
    {
        public List<T> list;

        public BBList(List<T> values)
        {
            list = values;
        }

        public T this[int index] { get => ((IList<T>)list)[index]; set => ((IList<T>)list)[index] = value; }

        public int Count => ((IList<T>)list).Count;

        public bool IsReadOnly => ((IList<T>)list).IsReadOnly;

        public void Add(T item)
        {
            ((IList<T>)list).Add(item);
        }

        public void Clear()
        {
            ((IList<T>)list).Clear();
        }

        public bool Contains(T item)
        {
            return ((IList<T>)list).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((IList<T>)list).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)list).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)list).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)list).Insert(index, item);
        }

        public bool Remove(T item)
        {
            return ((IList<T>)list).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)list).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)list).GetEnumerator();
        }
    }
}