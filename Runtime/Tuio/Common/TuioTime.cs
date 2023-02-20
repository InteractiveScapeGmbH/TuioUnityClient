using System;
using OSC.NET;

namespace Tuio.Common
{
    public class TuioTime
    {
        /// <summary>
        /// The start time of the session.
        /// </summary>
        public static TuioTime StartTime { get; private set; }

        /// <summary>
        /// The time since session started in seconds.
        /// </summary>
        public long Seconds { get; private set; }
        
        /// <summary>
        /// Time fraction in microseconds.
        /// </summary>
        public long Microseconds { get; private set; }

        private const long MicrosecondsPerSecond = 1000000;
        
        public TuioTime(long seconds, long microseconds)
        {
            Seconds = seconds;
            Microseconds = microseconds;
        }

        public static TuioTime FromOscTime(OscTimeTag oscTimeTag)
        {
            return new TuioTime(oscTimeTag.SecondsSinceEpoch, oscTimeTag.FractionalSecond);
        }

        /// <summary>
        /// Sums the provided time value represented in total microseconds to the base TuioTime.
        /// </summary>
        /// <param name="time">The base TuioTime.</param>
        /// <param name="microseconds">The total time to add in microseconds.</param>
        /// <returns>The sum of this TuioTime with the provided time in microseconds.</returns>
        public static TuioTime operator +(TuioTime time, long microseconds)
        {
            long sumOfMicroseconds = time.Microseconds + microseconds;
            long sec = sumOfMicroseconds < 0 ? time.Seconds - 1 : time.Seconds + sumOfMicroseconds / MicrosecondsPerSecond;
            long microsec = (sumOfMicroseconds + MicrosecondsPerSecond) % MicrosecondsPerSecond;
            return new TuioTime(sec, microsec);
        }

        /// <summary>
        /// Sums the provided TuioTime to the base TuioTime.
        /// </summary>
        /// <param name="timeA">The base TuioTime.</param>
        /// <param name="timeB">The TuioTime to add.</param>
        /// <returns>Sum of this TuioTime with the provided TuioTime.</returns>
        public static TuioTime operator +(TuioTime timeA, TuioTime timeB)
        {
            long sec = timeA.Seconds + timeB.Seconds;
            long microsec = timeA.Microseconds + timeB.Microseconds;
            sec += microsec / MicrosecondsPerSecond;
            microsec %= MicrosecondsPerSecond;
            return new TuioTime(sec, microsec);
        }
        
        /// <summary>
        /// Subtracts the provided time represented in microseconds from the base TuioTime.
        /// </summary>
        /// <param name="time">The base TuioTime.</param>
        /// <param name="microseconds">The total time to subtract in microseconds.</param>
        /// <returns>The subtraction result of this TuioTime minus the provided time in microseconds.</returns>
        public static TuioTime operator -(TuioTime time, long microseconds)
        {
            long sec = time.Seconds - microseconds / MicrosecondsPerSecond;
            long microsec = time.Microseconds - microseconds % MicrosecondsPerSecond;
            if (microsec < 0)
            {
                microsec += MicrosecondsPerSecond;
                sec--;
            }

            return new TuioTime(sec, microsec);
        }

        /// <summary>
        /// Subtracts the provided TuioTime from the base TuioTime.
        /// </summary>
        /// <param name="timeA">The base TuioTime.</param>
        /// <param name="timeB">The TuioTime to subtract.</param>
        /// <returns>The subtraction result of this TuioTime minus the provided TuioTime.</returns>
        public static TuioTime operator -(TuioTime timeA, TuioTime timeB)
        {
            long sec = timeA.Seconds - timeB.Seconds;
            long microsec = timeA.Microseconds - timeB.Microseconds;
            if (microsec < 0)
            {
                microsec += MicrosecondsPerSecond;
                sec--;
            }

            return new TuioTime(sec, microsec);
        }
        
        /// <summary>
        /// Check for equality of this TuioTime and a given one.
        /// </summary>
        /// <param name="time">The TuioTime to compare</param>
        /// <returns>True if the TuioTime are equal in seconds and microseconds.</returns>
        public bool Equals(TuioTime time)
        {
            return (Seconds == time.Seconds) && (Microseconds == time.Microseconds);
        }
        
        /// <summary>
        /// Compare two TuioTimes for equality.
        /// </summary>
        /// <param name="timeA">The first TuioTime.</param>
        /// <param name="timeB">The second TuioTime.</param>
        /// <returns>True if both TuioTimes are equal in seconds and microseconds.</returns>
        public static bool operator ==(TuioTime timeA, TuioTime timeB)
        {
            return timeA.Equals(timeB);
        }
    
        /// <summary>
        /// Compare two TuioTimes for inequality.
        /// </summary>
        /// <param name="timeA">The first TuioTime.</param>
        /// <param name="timeB">The second TuioTime.</param>
        /// <returns>True if both TuioTimes are unequal in seconds or microseconds.</returns>
        public static bool operator !=(TuioTime timeA, TuioTime timeB)
        {
            return !(timeA == timeB);
        }

        public TuioTime Subtract(TuioTime other)
        {
            var seconds = Seconds - other.Seconds;
            var microseconds = Microseconds - other.Microseconds;

            if (microseconds < 0)
            {
                microseconds += MicrosecondsPerSecond;
                seconds -= 1;
            }

            return new TuioTime(seconds, microseconds);
        }
        
        /// <summary>
        /// Returns the total TuioTime in milliseconds.
        /// </summary>
        /// <returns>The total TuioTime in milliseconds.</returns>
        public long GetTotalMilliseconds()
        {
            return 1000 * (long) Seconds + Microseconds / 1000;
        }

        /// <summary>
        /// Globally resets the Tuio session time.
        /// </summary>
        public static void Init()
        {
            StartTime = GetSystemTime();
        }

        public static TuioTime GetSystemTime()
        {
            OscTimeTag oscTimeTag = new OscTimeTag(DateTime.Now);
            return FromOscTime(oscTimeTag);
        }

        public static TuioTime GetCurrentTime()
        {
            return GetSystemTime() -StartTime;
        }
    }
}