using helloserve.com.Trees.Core.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Tests
{
    [TestClass]
    public class SimpleBinaryTreeStringTests : BaseTests<string>
    {
        [TestMethod]
        public void SimpleBinaryTree_NoComparer_AddThreeStringItems_TraverseDepthFirst_InOrder()
        {
            Setup(new SimpleBinaryTree<string>());

            Tree.Add("Hello");
            Tree.Add("Alpha!");
            Tree.Add("Zulu");

            IList<string> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0] == "Alpha!");
            Assert.IsTrue(items[1] == "Hello");
            Assert.IsTrue(items[2] == "Zulu");
        }

        [TestMethod]
        public void SimpleBinaryTree_ReverseComparer_AddThreeStringItems_TraverseDepthFirst_InOrder()
        {
            Setup(new SimpleBinaryTree<string>(new ReverseComparer<string>()));

            Tree.Add("Hello");
            Tree.Add("Alpha!");
            Tree.Add("Zulu");

            IList<string> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[2] == "Alpha!");
            Assert.IsTrue(items[1] == "Hello");
            Assert.IsTrue(items[0] == "Zulu");
        }
    }

    [TestClass]
    public class SimpleBinaryTreeIntTests : BaseTests<int>
    {
        [TestMethod]
        public void SimpleBinaryTree_NoComparer_AddRangeIntItems_TraverseDepthFirst_InOrder()
        {
            Setup(new SimpleBinaryTree<int>());

            Random random = new Random();
            List<int> values = new List<int>();
            for (int i = 0; i < 1500; i++)
            {
                values.Add(random.Next(int.MaxValue));
            }

            Tree.AddRange(values);

            IList<int> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items.Count == 1500);

            bool inOrder = true;

            for (int i = 1; i < items.Count; i++)
            {
                inOrder = inOrder && (items[i].CompareTo(items[i - 1]) >= 0);
            }

            Assert.IsTrue(inOrder);
        }
    }
}
