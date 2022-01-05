using System;
using System.Collections.Generic;
using System.Linq;
using Tuio.Common;

namespace Tuio.Tuio11
{
    public class Tuio11Container : Tuio11Point
    {
        private static int MAX_PATH_LENGTH = 128;
            
        protected TuioTime _currentTime;
        protected uint _sessionId;
        protected float _xSpeed;
        protected float _ySpeed;
        protected float _motionSpeed;
        protected float _motionAccel;
        protected TuioState _state;
        protected List<Tuio11Point> _prevPoints = new List<Tuio11Point>();
        
        public Tuio11Container(TuioTime startTime, uint sessionId, float xPos, float yPos, float xSpeed, float ySpeed, float motionAccel) : base(startTime, xPos, yPos)
        {
            _currentTime = startTime;
            _sessionId = sessionId;
            _xSpeed = xSpeed;
            _ySpeed = ySpeed;
            _motionSpeed = (float)Math.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);
            _prevPoints.Add(new Tuio11Point(currentTime, xPos, yPos));
        }
        
        public TuioTime currentTime => _currentTime;
        public uint sessionId => _sessionId;
        public float xSpeed => _xSpeed;
        public float ySpeed => _ySpeed;
        public float motionSpeed => _motionSpeed;
        public float motionAccel => _motionAccel;
        public TuioState state => _state;

        public List<Tuio11Point> GetPath()
        {
            return _prevPoints.ToList();
        }

        internal void _updateContainer(TuioTime currentTime, float xPos, float yPos, float xSpeed, float ySpeed, float motionAccel, bool isCalculateSpeeds)
        {
            var lastPoint = _prevPoints[_prevPoints.Count - 1];
            _xPos = xPos;
            _yPos = yPos;
            if (isCalculateSpeeds)
            {
                var dt = currentTime.Subtract(lastPoint.startTime).GetTotalMilliseconds() / 1000.0f;
                var dx = xPos - lastPoint.xPos;
                var dy = yPos - lastPoint.yPos;
                var dist = (float)Math.Sqrt(dx * dx + dy * dy);
                var lastMotionSpeed = motionSpeed;
                if(dt > 0)
                {
                    _xSpeed = dx / dt;
                    _ySpeed = dy / dt;
                    _motionSpeed = dist / dt;
                    _motionAccel = (_motionSpeed - lastMotionSpeed) / dt;
                }
            }
            else
            {
                _xSpeed = xSpeed;
                _ySpeed = ySpeed;
                _motionSpeed = (float)Math.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);
                _motionAccel = motionAccel;
            }

            _currentTime = currentTime;
            _prevPoints.Add(new Tuio11Point(currentTime, xPos, yPos));
            if (_prevPoints.Count > MAX_PATH_LENGTH)
            {
                _prevPoints.RemoveAt(0);
            }

            if (_motionAccel > 0)
            {
                _state = TuioState.Accelerating;
            } 
            else if (_motionAccel < 0)
            {
                _state = TuioState.Decelerating;
            }
            else
            {
                _state = TuioState.Stopped;
            }
        }

        internal void _remove()
        {
            _state = TuioState.Removed;
        }
    }
}