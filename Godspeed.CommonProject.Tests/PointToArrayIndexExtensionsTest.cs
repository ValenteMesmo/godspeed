using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace Godspeed.CommonProject.Tests
{
    [TestClass]
    public class PointToArrayIndexExtensionsTest
    {
        [TestMethod]
        public void FirstPosition()
        {
            var sut = new Point()
            {
                X = 0,
                Y = 0
            };
            var actual = sut.ToArrayIndex(100);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void SecondPosition()
        {
            var sut = new Point()
            {
                X = 1,
                Y = 0
            };

            var actual = sut.ToArrayIndex(100);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void ThirdPosition()
        {
            var sut = new Point()
            {
                X = 2,
                Y = 0
            };

            var actual = sut.ToArrayIndex(100);

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void LastIndexOnFirstRow()
        {
            var sut = new Point()
            {
                X = 99,
                Y = 0
            };

            var actual = sut.ToArrayIndex(100);

            Assert.AreEqual(99, actual);
        }

        [TestMethod]
        public void FirstIndexOnSecondRow()
        {
            var sut = new Point()
            {
                X = 0,
                Y = 1
            };

            var actual = sut.ToArrayIndex(100);

            Assert.AreEqual(100, actual);
        }

        [TestMethod]
        public void RevertFirstIndex()
        {
            var sut = 0;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void RevertSecondIndex()
        {
            var sut = 1;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(1, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void RevertThirdIndex()
        {
            var sut = 2;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(2, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void RevertLastIndexFromFirstRow()
        {
            var sut = 99;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(99, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void RevertFirstIndexFromSecondRow()
        {
            var sut = 100;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(1, actual.Y);
        }

        [TestMethod]
        public void RevertSecondIndexFromSecondRow()
        {
            var sut = 101;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(1, actual.X);
            Assert.AreEqual(1, actual.Y);
        }

        [TestMethod]
        public void RevertThirdIndexFromSecondRow()
        {
            var sut = 102;

            var actual = sut.FromArrayIndexToPoint(100);

            Assert.AreEqual(2, actual.X);
            Assert.AreEqual(1, actual.Y);
        }
    }
}
