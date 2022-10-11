using System;
using OSC.NET;

namespace Tuio.Common
{
    public class TuioTime
    {
        private static TuioTime _startTime;

        private long _seconds;
        private long _milliseconds;

        public TuioTime(long seconds, long milliseconds)
        {
            _seconds = seconds;
            _milliseconds = milliseconds;
        }

        public static TuioTime FromOscTime(OscTimeTag oscTimeTag)
        {
            return new TuioTime(oscTimeTag.SecondsSinceEpoch, oscTimeTag.FractionalSecond);
        }

        public TuioTime Subtract(TuioTime other)
        {
            var seconds = _seconds - other._seconds;
            var milliseconds = _milliseconds - other._milliseconds;

            if (milliseconds < 0)
            {
                milliseconds += 1000;
                seconds -= 1;
            }

            return new TuioTime(seconds, milliseconds);
        }

        public long GetTotalMilliseconds()
        {
            return 1000 * _seconds + _milliseconds;
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