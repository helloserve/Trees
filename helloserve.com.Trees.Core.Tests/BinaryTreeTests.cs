using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloserve.com.Trees.Core.Tests.Base;
using System.Collections.Generic;

namespace helloserve.com.Trees.Core.Tests
{
    [TestClass]
    public class BinaryTreeSimpleObjectTests : BaseTests<SimpleObject>
    {
        [TestMethod]
        public void BinaryTree_AddOneItem()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });

            Assert.IsTrue(Tree.Leaf != null);
            Assert.IsTrue(Tree.Leaf.Leafs.Count == 0);
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddThreeStringItems_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Hello");
            Assert.IsTrue(items[2].StringValue == "Zulu");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddThreeStringItems_TraverseDepthFirst_PreOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.PreOrder);

            Assert.IsTrue(items[0].StringValue == "Hello");
            Assert.IsTrue(items[1].StringValue == "Alpha!");
            Assert.IsTrue(items[2].StringValue == "Zulu");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddThreeStringItems_TraverseDepthFirst_PostOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.PostOrder);

            Assert.IsTrue(items[0].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Zulu");
            Assert.IsTrue(items[2].StringValue == "Hello");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddThreeDateTimeItems_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, DateTime>(x => x.DateTimeValue));

            Tree.Add(new SimpleObject() { DateTimeValue = new DateTime(2015, 7, 17) });
            Tree.Add(new SimpleObject() { DateTimeValue = new DateTime(2015, 7, 16) });
            Tree.Add(new SimpleObject() { DateTimeValue = new DateTime(2015, 7, 18) });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0].DateTimeValue == new DateTime(2015, 7, 16));
            Assert.IsTrue(items[1].DateTimeValue == new DateTime(2015, 7, 17));
            Assert.IsTrue(items[2].DateTimeValue == new DateTime(2015, 7, 18));
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddThreeIntItems_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, int>(x => x.IntValue));

            Tree.Add(new SimpleObject() { IntValue = 1 });
            Tree.Add(new SimpleObject() { IntValue = -6 });
            Tree.Add(new SimpleObject() { IntValue = 10 });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0].IntValue == -6);
            Assert.IsTrue(items[1].IntValue == 1);
            Assert.IsTrue(items[2].IntValue == 10);
        }

        [TestMethod]
        public void BinaryTree_ReverseComparer_AddThreeStringItems_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, new ReverseComparer<string>()));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[2].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Hello");
            Assert.IsTrue(items[0].StringValue == "Zulu");
        }    
    }
}
