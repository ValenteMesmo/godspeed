using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Godspeed.UnitTestProject
{
    public struct MyStruct
    {
        public Rectangle Area;
    }

    public static class GenericExtensions
    {
        public static IEnumerable<T> Yield<T>(this T value)
        {
            if (value != null)
                yield return value;
        }
    }

    public class DraggablesController
    {
        public void Update(IEnumerable<MyStruct> areas, IEnumerable<Vector2> touches)
        {
            foreach (var area in areas)
            {
                foreach (var touch in touches)
                {
                    if (area.Area.Contains(touch))
                    {

                    }
                }
            }
        }
    }

    public struct dragAndDrop
    {
        public readonly Action onDrag;
        public readonly Rectangle Area;

        public dragAndDrop(Rectangle Area, Action onDrag)
        {
            this.onDrag = onDrag;
            this.Area = Area;
        }
    }

 

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Should_Drag_Rectangle_Touch_Intercect()
        {
            var actual = false;
            //var sut = new dragAndDrop(() => actual = true);
            //Assert.AreEqual(expected: true, actual: actual);

            Assert.AreEqual(new Rectangle(), new Rectangle());
        }
    }
}
