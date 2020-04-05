using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramGridControl
{
    public class SortedList<T> : List<T>
    {
        public SortedList() : base()
        { }

        public SortedList(IComparer<T> comparer) : base()
        {
            this.comparer = comparer;
        }

        public SortedList(Comparison<T> comparison) : base()
        {
            this.comparison = comparison;
        }

        public SortedList(IEnumerable<T> collection) : base(collection.Distinct().ToList())
        {
            SortItems();
        }

        public SortedList(IEnumerable<T> collection, IComparer<T> comparer) : base(collection.Distinct().ToList())
        {
            this.comparer = comparer;
            SortItems();
        }

        public SortedList(IEnumerable<T> collection, Comparison<T> comparison) : base(collection.Distinct().ToList())
        {
            this.comparison = comparison;
            SortItems();
        }

        public SortedList(int capacity) : base(capacity)
        { }

        public SortedList(int capacity, IComparer<T> comparer) : base(capacity)
        {
            this.comparer = comparer;
        }

        public SortedList(int capacity, Comparison<T> comparison) : base(capacity)
        {
            this.comparison = comparison;
        }

        public new void Add(T item)
        {
            if (Contains(item))
                return;
            base.Add(item);
            SortItems();
        }

        private void SortItems()
        {
            if (comparer != null)
                Sort(comparer);
            else if (comparison != null)
                Sort(comparison);
            else
                Sort();
        }

        IComparer<T> comparer;
        Comparison<T> comparison;
    }
}
