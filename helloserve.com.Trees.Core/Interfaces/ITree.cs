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
    public interface ITree<T> : ICollection<T>
    {
        /// <summary>
        /// Gets and sets the default traverse mode for this instance. Note that not all orders are applicable to all modes. ArgumentExceptions will be thrown when a mix of mode and order is not applicable.
        /// </summary>
        TreeTraverseMode DefaultTraverseMode { get; set; }

        /// <summary>
        /// Gets and sets the default traverse order for this instance. Note that not all orders are applicable to all modes. ArgumentExceptions will be thrown when a mix of mode and order is not applicable.
        /// </summary>
        TreeTraverseOrder DefaultTraverseOrder { get; set; }

        /// <summary>
        /// The root leaf of the tree.
        /// </summary>
        ILeaf<T> Leaf { get; set; }

        /// <summary>
        /// Adds a range of items to the tree.
        /// </summary>
        /// <param name="collection"></param>
        void AddRange(IEnumerable<T> collection);

        /// <summary>
        /// Traverses the tree and returns the values stored at each node in the specified order.
        /// </summary>
        /// <param name="mode">The traverse mode to use. If null, the default traverse mode will be used.</param>
        /// <param name="order">The traverse order to use. If null, the default traverse order will be used. Some orders are not applicable to some modes. ArgumentExceptions will be thrown when a mix of mode and order is not applicable.</param>
        /// <returns>The list of values stored at the nodes.</returns>
        IList<T> Traverse(TreeTraverseMode? mode, TreeTraverseOrder? order);

        /// <summary>
        /// Finds a node in the tree.
        /// </summary>
        /// <param name="key">The key value to use during the search.</param>
        /// <returns>Returns the item stored at the node, or null.</returns>
        T Find(object key);
    }
}
