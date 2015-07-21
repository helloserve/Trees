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
            Assert.IsTrue(Tree.Leaf.Leafs == null);
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

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_RemoveMiddle_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            SimpleObject obj = new SimpleObject() { StringValue = "Charlie" };

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(obj);
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            Tree.Remove(obj);

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Bravo");            
            Assert.IsTrue(items[2].StringValue == "Delta");
            Assert.IsTrue(items[3].StringValue == "Hello");
            Assert.IsTrue(items[4].StringValue == "Zulu");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_RemoveMiddle_TraverseDepthFirst_PreOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            SimpleObject obj = new SimpleObject() { StringValue = "Charlie" };

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(obj);
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            Tree.Remove(obj);

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.PreOrder);

            Assert.IsTrue(items[0].StringValue == "Hello");
            Assert.IsTrue(items[1].StringValue == "Bravo");
            Assert.IsTrue(items[2].StringValue == "Alpha!");
            Assert.IsTrue(items[3].StringValue == "Delta");
            Assert.IsTrue(items[4].StringValue == "Zulu");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_RemoveHead_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            SimpleObject obj = new SimpleObject() { StringValue = "Hello" };

            Tree.Add(obj);
            Tree.Add(new SimpleObject() { StringValue = "Charlie" });
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            Tree.Remove(obj);

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Bravo");
            Assert.IsTrue(items[2].StringValue == "Charlie");
            Assert.IsTrue(items[3].StringValue == "Delta");
            Assert.IsTrue(items[4].StringValue == "Zulu");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_ContainsHead()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            SimpleObject obj = new SimpleObject() { StringValue = "Hello" };

            Tree.Add(obj);
            Tree.Add(new SimpleObject() { StringValue = "Charlie" });
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            Assert.IsTrue(Tree.Contains(obj));
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_ContainsLeaf()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            SimpleObject obj = new SimpleObject() { StringValue = "Delta" };

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Charlie" });
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(obj);

            Assert.IsTrue(Tree.Contains(obj));
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_Clear()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Charlie" });
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            Tree.Clear();
            IList<SimpleObject> items = Tree.Traverse(null, null);

            Assert.IsTrue(items.Count == 0);
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_CopyTo_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Charlie" });
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            SimpleObject[] items = new SimpleObject[6];
            Tree.CopyTo(items, 0);

            Assert.IsTrue(items[0].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Bravo");
            Assert.IsTrue(items[2].StringValue == "Charlie");
            Assert.IsTrue(items[3].StringValue == "Delta");
            Assert.IsTrue(items[4].StringValue == "Hello");
            Assert.IsTrue(items[5].StringValue == "Zulu");
        }

        [TestMethod]
        public void BinaryTree_NoComparer_AddSixStringItems_CopyTo_PreOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, TreeTraverseMode.DepthFirst, TreeTraverseOrder.PreOrder));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Charlie" });
            Tree.Add(new SimpleObject() { StringValue = "Bravo" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });
            Tree.Add(new SimpleObject() { StringValue = "Delta" });

            SimpleObject[] items = new SimpleObject[6];
            Tree.CopyTo(items, 0);

            Assert.IsTrue(items[0].StringValue == "Hello");
            Assert.IsTrue(items[1].StringValue == "Charlie");
            Assert.IsTrue(items[2].StringValue == "Bravo");
            Assert.IsTrue(items[3].StringValue == "Alpha!");
            Assert.IsTrue(items[4].StringValue == "Delta");
            Assert.IsTrue(items[5].StringValue == "Zulu");
        }
    }
}
