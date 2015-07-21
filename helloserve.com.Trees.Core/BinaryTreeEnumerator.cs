using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core
{
    public class BinaryTreeEnumerator<T, TProperty> : IEnumerator<T>
    {
        private class LeafState
        {
            public BinaryLeaf<T, TProperty> Leaf { get; set; }

            public bool VisitedSelf { get; set; }
            public bool VisitedLeft { get; set; }
            public bool VisitedRight { get; set; }

            public LeafState(BinaryLeaf<T, TProperty> leaf)
            {
                Leaf = leaf;
            }
        }

        private Stack<LeafState> _leafStates;

        private BinaryTree<T, TProperty> _tree;
        private BinaryLeaf<T, TProperty> _leaf;
        private TreeTraverseMode _traverseMode;
        private TreeTraverseOrder _traverseOrder;

        public BinaryTreeEnumerator(BinaryTree<T, TProperty> tree, TreeTraverseMode mode, TreeTraverseOrder order)
        {
            Initialize(tree);
            TraverseMode = mode;
            TraverseOrder = order;
        }

        private void Initialize(BinaryTree<T, TProperty> tree)
        {
            _tree = tree;
            _leaf = null;
        }

        public TreeTraverseMode TraverseMode
        {
            get { return _traverseMode; }
            set { _traverseMode = value; }
        }

        public TreeTraverseOrder TraverseOrder
        {
            get { return _traverseOrder; }
            set { _traverseOrder = value; }
        }

        private BinaryLeaf<T, TProperty> TraverseDepthFirst()
        {
            if (_leaf == null)
            {
                _leaf = _tree.Leaf as BinaryLeaf<T, TProperty>;
                _leafStates = new Stack<LeafState>();
                _leafStates.Push(new LeafState(_leaf));
            }

            switch (_traverseOrder)
            {
                case TreeTraverseOrder.PreOrder:
                    TraverseDepthFirstPreOrder();
                    break;
                case TreeTraverseOrder.InOrder:
                    TraverseDepthFirstInOrder();
                    break;
                case TreeTraverseOrder.PostOrder:
                    TraverseDepthFirstPostOrder();
                    break;
            }

            if (_leafStates.Count == 0)
                return null;

            return _leafStates.Peek().Leaf;
        }

        private void TraverseDepthFirstInOrder()
        {
            while (_leafStates.Count > 0)
            {
                if (!_leafStates.Peek().VisitedSelf && VisitLeft())
                {
                    _leafStates.Push(new LeafState(_leafStates.Peek().Leaf.LeftLeaf as BinaryLeaf<T, TProperty>));
                }
                else
                {
                    if (!_leafStates.Peek().VisitedSelf)
                    {
                        VisitSelf();
                        break;
                    }

                    if (VisitRight())
                    {
                        _leafStates.Push(new LeafState(_leafStates.Peek().Leaf.RightLeaf as BinaryLeaf<T, TProperty>));
                    }
                    else
                        _leafStates.Pop();
                }
            }
        }

        private void TraverseDepthFirstPreOrder()
        {
            while (_leafStates.Count > 0)
            {
                if (!_leafStates.Peek().VisitedSelf)
                {
                    VisitSelf();
                    break;
                }

                if (VisitLeft())
                {
                    _leafStates.Push(new LeafState(_leafStates.Peek().Leaf.LeftLeaf as BinaryLeaf<T, TProperty>));
                }
                else
                {
                    if (VisitRight())
                    {
                        _leafStates.Push(new LeafState(_leafStates.Peek().Leaf.RightLeaf as BinaryLeaf<T, TProperty>));
                    }
                    else
                        _leafStates.Pop();
                }
            }
        }

        private void TraverseDepthFirstPostOrder()
        {
            while (_leafStates.Count > 0)
            {
                if (!_leafStates.Peek().VisitedSelf && VisitLeft())
                {
                    _leafStates.Push(new LeafState(_leafStates.Peek().Leaf.LeftLeaf as BinaryLeaf<T, TProperty>));
                }
                else
                {
                    if (VisitRight())
                    {
                        _leafStates.Push(new LeafState(_leafStates.Peek().Leaf.RightLeaf as BinaryLeaf<T, TProperty>));
                    }
                    else
                    {
                        if (!_leafStates.Peek().VisitedSelf)
                        {
                            VisitSelf();
                            break;
                        }

                        _leafStates.Pop();
                    }
                }
            }
        }

        private bool VisitLeft()
        {
            if (_leafStates.Peek().VisitedLeft)
                return false;

            _leafStates.Peek().VisitedLeft = true;
            BinaryLeaf<T, TProperty> leftLeaf = _leafStates.Peek().Leaf.LeftLeaf as BinaryLeaf<T, TProperty>;
            if (leftLeaf == null)
                return false;

            return true;
        }

        private bool VisitSelf()
        {
            if (_leafStates.Peek().VisitedSelf)
                return false;

            _leafStates.Peek().VisitedSelf = true;

            return true;
        }

        private bool VisitRight()
        {
            if (_leafStates.Peek().VisitedRight)
                return false;

            _leafStates.Peek().VisitedRight = true;
            BinaryLeaf<T, TProperty> rightLeaf = _leafStates.Peek().Leaf.RightLeaf as BinaryLeaf<T, TProperty>;
            if (rightLeaf == null)
                return false;

            return true;
        }

        private BinaryLeaf<T, TProperty> TraverseBreathFirst()
        {
            throw new NotImplementedException();
        }

        #region IEnumerator

        public T Current
        {
            get
            {
                if (_leaf == null)
                    throw new InvalidOperationException();

                return _leaf.Value;
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_traverseMode == TreeTraverseMode.DepthFirst)
                _leaf = TraverseDepthFirst();
            else
                _leaf = TraverseBreathFirst();

            return _leaf != null && _leaf.Value != null;
        }

        public void Reset()
        {
            _leaf = null;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _tree = null;
            _leaf = null;
            _leafStates = null;
        }

        #endregion
    }
}
