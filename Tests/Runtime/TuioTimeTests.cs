using NUnit.Framework;
using Tuio.Common;

namespace Tests.Runtime
{
    public class TuioTimeTests
    {
        [Test]
        public void Add_Two_TuioTimes()
        {
            var timeA = new TuioTime(1,15);
            var timeB = new TuioTime(2, 10);
            var result = timeA + timeB;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 25);
        }

        [Test]
        public void Add_Two_TuioTimes_With_Overflowing_Microseconds()
        {
            var timeA = new TuioTime(2, 999999);
            var timeB = new TuioTime(0, 3);
            var result = timeA + timeB;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 2);
        }

        [Test]
        public void Add_Negative_TuioTime()
        {
            var timeA = new TuioTime(3, 5);
            var timeB = new TuioTime(-2, -3);
            var result = timeA + timeB;
            Assert.AreEqual(result.Seconds, 1);
            Assert.AreEqual(result.Microseconds, 2);
        }

        [Test]
        public void Add_Microseconds()
        {
            var time = new TuioTime(3, 20);
            long microsec = 500;
            var result = time + microsec;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 520);
        }

        [Test]
        public void Add_Microseconds_With_Overflow()
        {
            var time = new TuioTime(2, 999999);
            var microsec = 3;
            var result = time + microsec;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 2);
        }

        [Test]
        public void Add_Negative_Microseconds_With_Overflow()
        {
            var time = new TuioTime(2, 2);
            var microsec = -3;
            var result = time + microsec;
            Assert.AreEqual(result.Seconds, 1);
            Assert.AreEqual(result.Microseconds, 999999);
        }

        [Test]
        public void Subtract_Two_TuioTimes()
        {
            var timeA = new TuioTime(3, 25);
            var timeB = new TuioTime(2, 12);
            var result = timeA - timeB;
            Assert.AreEqual(result.Seconds, 1);
            Assert.AreEqual(result.Microseconds, 13);
        }

        [Test]
        public void Subtract_Two_TuioTimes_With_Overflow()
        {
            var timeA = new TuioTime(3, 3);
            var timeB = new TuioTime(1, 4);
            var result = timeA - timeB;
            Assert.AreEqual(result.Seconds, 1);
            Assert.AreEqual(result.Microseconds, 999999);
        }

        [Test]
        public void Subtract_Microseconds()
        {
            var time = new TuioTime(3, 10);
            var microseconds = 5;
            var result = time - microseconds;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 5);
        }

        [Test]
        public void Subtract_Microseconds_With_Overflow()
        {
            var time = new TuioTime(3, 10);
            var microseconds = 11;
            var result = time - microseconds;
            Assert.AreEqual(result.Seconds, 2);
            Assert.AreEqual(result.Microseconds, 999999);
        }
    }
}