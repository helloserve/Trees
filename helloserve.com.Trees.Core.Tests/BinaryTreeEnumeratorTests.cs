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
    public class BinaryTreeEnumeratorTests : BaseTests<SimpleObject>
    {
        [TestMethod]
        public void BinaryTreeEnumerator_NoComparer_AddThreeStringItems_TraverseDepthFirst_InOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            List<string> items = new List<string>();
            foreach (var item in Tree)
            {
                items.Add(item.StringValue);
            }

            Assert.IsTrue(items[0] == "Alpha!");
            Assert.IsTrue(items[1] == "Hello");
            Assert.IsTrue(items[2] == "Zulu");
        }

        [TestMethod]
        public void BinaryTreeEnumerator_NoComparer_AddThreeStringItems_TraverseDepthFirst_PreOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, TreeTraverseMode.DepthFirst, TreeTraverseOrder.PreOrder));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            List<string> items = new List<string>();
            foreach (var item in Tree)
            {
                items.Add(item.StringValue);
            }

            Assert.IsTrue(items[0] == "Hello");
            Assert.IsTrue(items[1] == "Alpha!");
            Assert.IsTrue(items[2] == "Zulu");
        }

        [TestMethod]
        public void BinaryTreeEnumerator_NoComparer_AddThreeStringItems_TraverseDepthFirst_PostOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue, TreeTraverseMode.DepthFirst, TreeTraverseOrder.PostOrder));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            List<string> items = new List<string>();
            foreach (var item in Tree)
            {
                items.Add(item.StringValue);
            }

            Assert.IsTrue(items[0] == "Alpha!");
            Assert.IsTrue(items[1] == "Zulu");
            Assert.IsTrue(items[2] == "Hello");
        }
    }
}
