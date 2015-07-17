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
            Leafs = new List<ILeaf<T>>();
        }

        private T _item;
        private Func<T, TProperty> _function;
        private IComparer<TProperty> _comparer;

        private void EnsureLeafsExist(int index)
        {
            if (Leafs.Count < 2)
                while (Leafs.Count < 2)
                    Leafs.Add(new BinaryLeaf<T, TProperty>(_function, _comparer));

            if (Leafs[index] == null)
                Leafs[index] = new BinaryLeaf<T, TProperty>(_function, _comparer);
        }

        protected ILeaf<T> LeftLeaf
        {
            get
            {
                EnsureLeafsExist(0);
                return Leafs[0];
            }
        }

        protected ILeaf<T> RightLeaf
        {
            get
            {
                EnsureLeafsExist(1);
                return Leafs[1];
            }
        }

        protected override ILeaf<T> FindLeaf(T item)
        {
            if (_item == null)
                throw new InvalidOperationException("No data to compare against. Set the first item.");

            int compare = 0;
            if (_comparer == null)
            {
                var comparable = _function(item) as IComparable;
                if (comparable == null)
                    throw new InvalidOperationException(string.Format("'{0}' does not have a default IComparable implementation", typeof(T)));

                compare = comparable.CompareTo(_function(_item));
            }
            else
            {
                compare = _comparer.Compare(_function(item), _function(_item));
            }

            if (compare <= 0)
                return LeftLeaf;
            else
                return RightLeaf;
        }

        public override void Add(T item)
        {
            if (!HasValue)
            {
                _item = item;
                HasValue = true;
                return;
            }

            FindLeaf(item).Add(item);
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
