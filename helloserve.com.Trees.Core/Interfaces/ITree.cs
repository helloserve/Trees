using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Interfaces
{
    /// <summary>
    /// A common interface for a tree implementation.
    /// </summary>
    /// <typeparam name="T">The type of the value that will be stored at each node</typeparam>
    public interface ITree<T>
    {
        /// <summary>
        /// The root leaf of the tree.
        /// </summary>
        ILeaf<T> Leaf { get; set; }

        /// <summary>
        /// Adds an item to the tree.
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Adds a range of items to the tree.
        /// </summary>
        /// <param name="collection"></param>
        void AddRange(IEnumerable<T> collection);

        /// <summary>
        /// Traverses the tree and returns the values stored at each node in the specified order.
        /// </summary>
        /// <param name="mode">The traverse mode to use</param>
        /// <param name="order">The traverse order to use. Some orders are not applicable to some modes.</param>
        /// <returns>The list of values stored at the nodes.</returns>
        IList<T> Traverse(TreeTraverseMode mode, TreeTraverseOrder order);
    }
}
