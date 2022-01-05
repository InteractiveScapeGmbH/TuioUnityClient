using Tuio.Common;

namespace Tuio.Tuio11
{
    public class Tuio11Point
    {
        protected TuioTime _startTime;
        protected float _xPos;
        protected float _yPos;
        
        public Tuio11Point(TuioTime startTime, float xPos, float yPos)
        {
            _startTime = startTime;
            _xPos = xPos;
            _yPos = yPos;
        }
        
        public TuioTime startTime => _startTime;
        public float xPos => _xPos;
        public float yPos => _yPos;
    }
}