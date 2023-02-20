using NUnit.Framework;
using Tuio.Common;

namespace Tests.Runtime
{
    public class TuioTimeTests
    {
        [Test]
        public void AddTwoTuioTimes()
        {
            var timeA = new TuioTime(1,15);
            var timeB = new TuioTime(2, 10);
            var result = timeA + timeB;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 25);
        }

        [Test]
        public void AddTwoTuioTimesWithOverflowMicroseconds()
        {
            var timeA = new TuioTime(2, 999999);
            var timeB = new TuioTime(0, 3);
            var result = timeA + timeB;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 2);
        }

        [Test]
        public void AddNegativeTuioTime()
        {
            var timeA = new TuioTime(3, 5);
            var timeB = new TuioTime(-2, -3);
            var result = timeA + timeB;
            Assert.AreEqual(result.Seconds, 1);
            Assert.AreEqual(result.Microseconds, 2);
        }

        [Test]
        public void AddMicroseconds()
        {
            var time = new TuioTime(3, 20);
            long microsec = 500;
            var result = time + microsec;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 520);
        }

        [Test]
        public void AddMicrosecondsWithOverflow()
        {
            var time = new TuioTime(2, 999999);
            var microsec = 3;
            var result = time + microsec;
            Assert.AreEqual(result.Seconds, 3);
            Assert.AreEqual(result.Microseconds, 2);
        }

        [Test]
        public void AddNegativeMicrosecondsWithOverflow()
        {
            var time = new TuioTime(2, 2);
            var microsec = -3;
            var result = time + microsec;
            Assert.AreEqual(result.Seconds, 1);
            Assert.AreEqual(result.Microseconds, 999999);
        }
    }
}