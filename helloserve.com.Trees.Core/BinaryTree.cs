using helloserve.com.Trees.Core.Base;
using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core
{
    /// <summary>
    /// A customizable binary tree implementation that supports both value and reference types thtough expressions or comparers.
    /// </summary>
    /// <typeparam name="T">The reference or value type that the three should be based on.</typeparam>
    /// <typeparam name="TProperty">The property (or itself) of the type T that binary comparison will be applied on.</typeparam>
    /// <remarks>Although ICollection is implemented, note that some intialization is required to ensure correct operation of some of the implemented methods and properties. For instance, for the CopyTo method to give expected results, the tree must be set to the correct default traverse behavior before a call to CopyTo, since the method implementation doesn't take those values as parameters.</remarks>
    public class BinaryTree<T, TProperty> : TreeBase<T>
    {
        private Expression<Func<T, TProperty>> _expression;
        private Func<T, TProperty> _function;
        private IComparer<TProperty> _comparer;
        private int _itemCount;

        /// <summary>
        /// Initializes the binary tree using a expression only, where the default comparer for TProperty will be used.
        /// </summary>
        /// <param name="expression">The expression to TProperty</param>
        public BinaryTree(Expression<Func<T, TProperty>> expression)
        {
            Initialize(expression, null);
        }

        /// <summary>
        /// Initializes the binary tree using a expression only with the default traverse options. The default comparer for TProperty will be used.
        /// </summary>
        /// <param name="expression">The expression to TProperty</param>
        /// <param name="mode">The default traverse mode to use for non-specific methods like CopyTo() and GetEnumerator().</param>
        /// <param name="order">The default traverse order to use for non-specific methods like CopyTo() and GetEnumerator().</param>
        public BinaryTree(Expression<Func<T, TProperty>> expression, TreeTraverseMode mode, TreeTraverseOrder order) : base(mode, order)
        {
            Initialize(expression, null);
        }

        /// <summary>
        /// Initializes the binary tree with both an expression to TProperty, and a comparer to use on TProperty.
        /// </summary>
        /// <param name="expression">The expression to TProperty</param>
        /// <param name="comparer">The comparer to use on TProperty instead of the default comparer.</param>
        public BinaryTree(Expression<Func<T, TProperty>> expression, IComparer<TProperty> comparer)
        {
            Initialize(expression, comparer);
        }

        /// <summary>
        /// Initializes the binary tree with both an expression to TProperty, a comparer to use on TProperty, and the default traverse options.
        /// </summary>
        /// <param name="expression">The expression to TProperty</param>
        /// <param name="comparer">The comparer to use on TProperty instead of the default comparer.</param>
        /// <param name="mode">The default traverse mode to use for non-specific methods like CopyTo() and GetEnumerator().</param>
        /// <param name="order">The default traverse order to use for non-specific methods like CopyTo and GetEnumerator().</param>
        public BinaryTree(Expression<Func<T, TProperty>> expression, IComparer<TProperty> comparer, TreeTraverseMode mode, TreeTraverseOrder order) : base(mode, order)
        {
            Initialize(expression, comparer);
        }

        private void Initialize(Expression<Func<T, TProperty>> expression, IComparer<TProperty> comparer)
        {
            _expression = expression;
            if (_expression != null)
            {
                _function = _expression.Compile();
            }
            _comparer = comparer;
            Leaf = new BinaryLeaf<T, TProperty>(_function, _comparer);
        }

        #region ITree

        public override void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
                _itemCount++;
            }
        }

        public override IList<T> Traverse(TreeTraverseMode? mode, TreeTraverseOrder? order)
        {
            IList<T> collection = new List<T>();

            BinaryTreeEnumerator<T, TProperty> enumerator = GetEnumerator() as BinaryTreeEnumerator<T, TProperty>;
            enumerator.TraverseMode = mode ?? DefaultTraverseMode;
            enumerator.TraverseOrder = order ?? DefaultTraverseOrder;
            while (enumerator.MoveNext())
            {
                collection.Add(enumerator.Current);
            }

            return collection;
        }

        public override T Find(object key)
        {
            if (!(key is TProperty))
                throw new ArgumentException(string.Format("Key value must be of type {0}", typeof(TProperty).Name));


            var leaf = (Leaf as BinaryLeaf<T, TProperty>).FindLeaf(key);

            if (leaf == null)
                return default(T);

            return leaf.Value;
        }

        #endregion

        #region ICollection

        public override int Count
        {
            get { return _itemCount; }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        //public bool IsSynchronized
        //{
        //    get { return false; }
        //}

        //public Object SyncRoot
        //{
        //    get { return this; }
        //}

        public override void Add(T item)
        {
            (Leaf as LeafBase<T>).Add(item, null);
            _itemCount++;
        }

        public override void Clear()
        {
            (Leaf as LeafBase<T>).Clear();
        }

        public override bool Contains(T item)
        {
            return (Leaf as BinaryLeaf<T, TProperty>).FindLeaf(_function(item)) != null;
        }

        public override void CopyTo(T[] array, int index)
        {
            Traverse(null, null).ToArray().CopyTo(array, index);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator<T, TProperty>(this, DefaultTraverseMode, DefaultTraverseOrder);
        }        

        public override bool Remove(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            var leaf = (Leaf as BinaryLeaf<T, TProperty>).FindLeaf(_function(item)) as BinaryLeaf<T, TProperty>;

            if (leaf == null)
                return false;

            if (leaf == Leaf)
            {
                if (leaf.LeftLeaf != null && leaf.RightLeaf != null)
                {
                    Leaf = leaf.LeftLeaf;
                    (Leaf as LeafBase<T>).Add(leaf.RightLeaf.Value, Leaf);
                }
                else if (leaf.LeftLeaf == null)
                    Leaf = leaf.RightLeaf;
                else
                    Leaf = leaf.LeftLeaf;

                return true;
            }
            else
            {
                return leaf.Remove();
            }
        }

        #endregion
    }

    /// <summary>
    /// A simpler version of BinaryTree, this implementation forces TProperty to T. Use this implementation for value types, or objects where you only need a comparer.
    /// </summary>
    /// <typeparam name="T">The reference or value type that the three should be based on.</typeparam>
    public class SimpleBinaryTree<T> : BinaryTree<T, T>
    {
        /// <summary>
        /// Initializes the simple binary tree with no comparer, where the default comparer for T will be used.
        /// </summary>
        public SimpleBinaryTree()
            : base(x => x)
        {
        }

        /// <summary>
        /// Initializes the simple binary tree with a comparer for T.
        /// </summary>
        /// <param name="comparer">The comparer to use for T.</param>
        public SimpleBinaryTree(IComparer<T> comparer)
            : base(x => x, comparer)
        {

        }
    }
}
