using System;
using OSC.NET;

namespace Tuio.Common
{
    public class TuioTime
    {
        private static TuioTime _startTime;

        private readonly long _seconds;
        private readonly long _microseconds;

        public TuioTime(long seconds, long microseconds)
        {
            _seconds = seconds;
            _microseconds = microseconds;
        }

        public static TuioTime FromOscTime(OscTimeTag oscTimeTag)
        {
            return new TuioTime(oscTimeTag.SecondsSinceEpoch, oscTimeTag.FractionalSecond);
        }

        public static TuioTime operator +(TuioTime time, long microseconds)
        {
            long sec = time._seconds + microseconds / 1000000;
            long microsec = time._microseconds + microseconds % 1000000;
            return new TuioTime(sec, microsec);
        }

        public static TuioTime operator +(TuioTime timeA, TuioTime timeB)
        {
            long sec = timeA._seconds + timeB._seconds;
            long microsec = timeA._microseconds + timeB._microseconds;
            sec += microsec / 1000000;
            microsec %= 1000000;
            return new TuioTime(sec, microsec);
        }

        public static TuioTime operator -(TuioTime time, long microseconds)
        {
            long sec = time._seconds - microseconds / 1000000;
            long microsec = time._microseconds - microseconds % 1000000;
            if (microsec < 0)
            {
                microsec += 1000000;
                sec--;
            }

            return new TuioTime(sec, microsec);
        }

        public static TuioTime operator -(TuioTime timeA, TuioTime timeB)
        {
            long sec = timeA._seconds - timeB._seconds;
            long microsec = timeA._microseconds - timeB._microseconds;
            if (microsec < 0)
            {
                microsec += 1000000;
                sec--;
            }

            return new TuioTime(sec, microsec);
        }

        public TuioTime Subtract(TuioTime other)
        {
            var seconds = _seconds - other._seconds;
            var microseconds = _microseconds - other._microseconds;

            if (microseconds < 0)
            {
                microseconds += 1000000;
                seconds -= 1;
            }

            return new TuioTime(seconds, microseconds);
        }

        public long GetTotalMilliseconds()
        {
            return 1000 * (long) _seconds + _microseconds / 1000;
        }

        public static void Init()
        {
            _startTime = GetSystemTime();
        }

        public static TuioTime GetSystemTime()
        {
            OscTimeTag oscTimeTag = new OscTimeTag(DateTime.Now);
            return FromOscTime(oscTimeTag);
        }

        public static TuioTime GetCurrentTime()
        {
            return GetSystemTime().Subtract(_startTime);
        }
    }
}