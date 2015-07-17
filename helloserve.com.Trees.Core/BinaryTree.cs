using helloserve.com.Trees.Core.Base;
using helloserve.com.Trees.Core.Interfaces;
using System;
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
    public class BinaryTree<T, TProperty> : TreeBase<T>, ITree<T>
    {
        private Expression<Func<T, TProperty>> _expression;
        private Func<T, TProperty> _function;
        private IComparer<TProperty> _comparer;

        /// <summary>
        /// Initializes the binary tree using a expression only, where the default comparer for TProperty will be used.
        /// </summary>
        /// <param name="expression">The expression to TProperty</param>
        public BinaryTree(Expression<Func<T, TProperty>> expression)
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

        public override void Add(T item)
        {
            Leaf.Add(item);
        }

        public override void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public override IList<T> Traverse(TreeTraverseMode mode, TreeTraverseOrder order)
        {
            IList<T> collection = new List<T>();
            if (mode == TreeTraverseMode.DepthFirst)
                return (Leaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst(order, collection);
            else
                return (Leaf as BinaryLeaf<T, TProperty>).TraverseBreathFirst(collection);
        }
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
        public SimpleBinaryTree(IComparer<T> comparer) : base(x => x, comparer)
        {
            
        }
    }
}
