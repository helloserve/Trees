using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Tests.Base
{
    public class BaseTests<T>
    {
        public ITree<T> Tree;

        public void Setup(ITree<T> tree)
        {
            Tree = tree;
        }

        public void TearDown()
        {

        }

        internal class ReverseComparer<T> : IComparer<T>
            where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return -1 * x.CompareTo(y);
            }
        }
    }
}
