using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DiagramGridControl;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestSortedList
    {
        [TestMethod]
        public void ConstructorOfSortedListCreatesListOfItemsSortedByValue()
        {
            SortedList<int> list = new SortedList<int> { 5, 4, 7, 1 };
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(4, list[1]);
            Assert.AreEqual(5, list[2]);
            Assert.AreEqual(7, list[3]);
        }

        [TestMethod]
        public void ConstructorOfSortedListRemovesDuplicateEntries()
        {
            SortedList<int> list = new SortedList<int> { 5, 4, 7, 4, 1, 5 };
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(4, list[1]);
            Assert.AreEqual(5, list[2]);
            Assert.AreEqual(7, list[3]);
        }

        class ReverseComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y - x;
            }
        }

        [TestMethod]
        public void ConstructorOfSortedListWithCustomIComparerCreatesListOfItemsSortedByValue()
        {
            SortedList<int> list = new SortedList<int>(new List<int>{ 5, 4, 7, 1 }, new ReverseComparer());
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(7, list[0]);
            Assert.AreEqual(5, list[1]);
            Assert.AreEqual(4, list[2]);
            Assert.AreEqual(1, list[3]);
        }

        [TestMethod]
        public void ConstructorOfSortedListWithCustomComparisonCreatesListOfItemsSortedByValue()
        {
            SortedList<int> list = new SortedList<int>(new List<int> { 5, 4, 7, 1 }, (x, y) => y - x);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(7, list[0]);
            Assert.AreEqual(5, list[1]);
            Assert.AreEqual(4, list[2]);
            Assert.AreEqual(1, list[3]);
        }
    }
}
