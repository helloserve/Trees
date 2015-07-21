using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core
{
    public class BinaryLeaf<T, TProperty> : LeafBase<T>, ILeaf<T>
    {
        internal BinaryLeaf(Func<T, TProperty> function, IComparer<TProperty> comparer)
            : base()
        {
            _function = function;
            _comparer = comparer;
        }

        private T _item;
        private Func<T, TProperty> _function;
        private IComparer<TProperty> _comparer;

        private void EnsureLeafExist(int index, bool create = false)
        {
            if (Leafs == null)
                Leafs = new List<ILeaf<T>>();

            if (Leafs.Count < 2)
                while (Leafs.Count < 2)
                    Leafs.Add(null);

            if (Leafs[index] == null && create)
                Leafs[index] = new BinaryLeaf<T, TProperty>(_function, _comparer);
        }

        private ILeaf<T> GetLeftLeaf()
        {
            EnsureLeafExist(0, create: true);
            return LeftLeaf;
        }

        public ILeaf<T> LeftLeaf
        {
            get
            {
                EnsureLeafExist(0);
                return Leafs[0];
            }
        }

        private ILeaf<T> GetRightLeaf()
        {
            EnsureLeafExist(1, create: true);
            return RightLeaf;
        }

        public ILeaf<T> RightLeaf
        {
            get
            {
                EnsureLeafExist(0);
                return Leafs[1];
            }
        }

        protected override ILeaf<T> AddLeafAt(T item)
        {
            if (_item == null)
                throw new InvalidOperationException("No data to compare against. Set the first item.");

            int compare = CompareLeaf(_item, item);

            if (compare <= 0)
                return GetLeftLeaf();
            else
                return GetRightLeaf();
        }

        /// <summary>
        /// Implements a depth first pre-order traverse to find the leaf, otherwise null.
        /// </summary>
        /// <param name="key">The key to find.</param>
        /// <returns>The leaf found for the key, or null.</returns>
        internal override ILeaf<T> FindLeaf(object key)
        {
            if (!(key is TProperty))
                throw new ArgumentException(string.Format("Key value must be of type {0}", typeof(TProperty).Name));

            if (_item == null)
                throw new InvalidOperationException("No data to compare against. Set the first item.");

            int compare = -1;

            while (true)
            {
                compare = CompareLeaf(_item, (TProperty)key);

                if (compare == 0)
                    break;

                if (compare < 0)
                {
                    if (LeftLeaf == null)
                        return null;

                    return (LeftLeaf as BinaryLeaf<T, TProperty>).FindLeaf(key);
                }
                else
                {
                    if (RightLeaf == null)
                        return null;

                    return (RightLeaf as BinaryLeaf<T, TProperty>).FindLeaf(key);
                }
            }

            return this;
        }

        internal int CompareLeaf(T leafItem, T compareItem)
        {
            return CompareLeaf(leafItem, _function(compareItem));
        }

        internal int CompareLeaf(T leafItem, TProperty key)
        {
            int compare = 0;
            if (_comparer == null)
            {
                var comparable = key as IComparable;
                if (comparable == null)
                    throw new InvalidOperationException(string.Format("'{0}' does not have a default IComparable implementation", typeof(T)));

                compare = comparable.CompareTo(_function(leafItem));
            }
            else
            {
                compare = _comparer.Compare(key, _function(leafItem));
            }

            return compare;
        }

        internal override void Clear()
        {
            if (LeftLeaf != null)
                (LeftLeaf as BinaryLeaf<T, TProperty>).Clear();
            if (RightLeaf != null)
                (RightLeaf as BinaryLeaf<T, TProperty>).Clear();

            Leafs = null;
            Value = default(T);
        }

        internal override void Add(T item, ILeaf<T> parent)
        {
            if (!HasValue)
            {
                _item = item;
                Parent = parent;
                HasValue = true;
                return;
            }

            (AddLeafAt(item) as LeafBase<T>).Add(item, this);
        }

        internal override bool Remove()
        {
            int parentIndex = 0;
            if (Parent.Leafs[1] == this)
                parentIndex = 1;

            if (LeftLeaf != null && RightLeaf != null)
            {
                Parent.Leafs[parentIndex] = LeftLeaf;
                (Parent.Leafs[parentIndex] as LeafBase<T>).Add(RightLeaf.Value, Parent);
            }
            else if (LeftLeaf == null)
                Parent.Leafs[parentIndex] = RightLeaf;
            else
                Parent.Leafs[parentIndex] = LeftLeaf;

            return true;
        }

        public override T Value
        {
            get { return _item; }
            set { _item = value; }
        }

        internal IList<T> TraverseDepthFirst(TreeTraverseOrder order, IList<T> list)
        {
            TraverseDepthFirst_Implementation(order, ref list);
            return list;
        }

        private void TraverseDepthFirst_Implementation(TreeTraverseOrder order, ref IList<T> list)
        {
            if (!HasValue)
                return;

            switch (order)
            {
                case TreeTraverseOrder.PreOrder:
                    list.Add(_item);
                    (LeftLeaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst_Implementation(order, ref list);
                    (RightLeaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst_Implementation(order, ref list);
                    break;
                case TreeTraverseOrder.InOrder:
                    (LeftLeaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst_Implementation(order, ref list);
                    list.Add(_item);
                    (RightLeaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst_Implementation(order, ref list);
                    break;
                case TreeTraverseOrder.PostOrder:
                    (LeftLeaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst_Implementation(order, ref list);
                    (RightLeaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst_Implementation(order, ref list);
                    list.Add(_item);
                    break;
                default:
                    throw new ArgumentException(string.Format("Depth-first traverse mode does not support {0}", order.ToString()));
            }
        }

        internal IList<T> TraverseBreathFirst(IList<T> list)
        {
            throw new NotImplementedException();
        }

        private void TraverseBreathFirst_Implementation(ref IList<T> list)
        {

        }
    }
}
