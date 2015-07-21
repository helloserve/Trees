using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Base
{
    public abstract class TreeBase<T> : ITree<T>
    {
        public TreeBase()
        {
        }

        public TreeBase(TreeTraverseMode mode, TreeTraverseOrder order)
        {
            DefaultTraverseMode = mode;
            DefaultTraverseOrder = order;
        }

        #region ITree

        public virtual TreeTraverseMode DefaultTraverseMode { get; set; }
        public virtual TreeTraverseOrder DefaultTraverseOrder { get; set; }

        public ILeaf<T> Leaf { get; set; }
        public abstract void AddRange(IEnumerable<T> collection);

        public abstract IList<T> Traverse(TreeTraverseMode? mode, TreeTraverseOrder? order);
        public abstract T Find(object key);

        #endregion

        #region ICollection

        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }

        public abstract void Add(T item);
        public abstract void Clear();
        public abstract bool Contains(T item);
        public abstract void CopyTo(T[] array, int index);
        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public abstract bool Remove(T item);

        #endregion
    }
}
