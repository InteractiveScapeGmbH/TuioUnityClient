using System;
using System.Collections.Generic;
using System.Linq;
using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Component : Tuio20Point
    {
        private static int MAX_PATH_LENGTH = 128; 
        protected TuioTime _currentTime;
        protected Tuio20Object _container;
        protected float _angle;
        protected float _xVel;
        protected float _yVel;
        protected float _mVel;
        protected float _aVel;
        protected float _mAcc;
        protected float _rAcc;
        protected TuioState _state;
        protected Queue<Tuio20Point> _prevPoints = new Queue<Tuio20Point>();

        public Tuio20Component(TuioTime startTime, Tuio20Object container, float xPos, float yPos, float angle,
            float xVel, float yVel, float aVel, float mAcc, float rAcc) : base(startTime, xPos, yPos)
        {
            _currentTime = startTime;
            _container = container;
            _xPos = xPos;
            _yPos = yPos;
            _angle = angle;
            _xVel = xVel;
            _yVel = yVel;
            _mVel = (float)Math.Sqrt(xVel * xVel + yVel * yVel);
            _aVel = aVel;
            _mAcc = mAcc;
            _rAcc = rAcc;
            _state = TuioState.Added;
            _prevPoints.Enqueue(new Tuio20Point(startTime, xPos, yPos));
        }
        
        public TuioTime currentTime => _currentTime;
        public Tuio20Object container => _container;
        public uint sessionId => _container.sessionId;
        public float angle => _angle;
        public float xVel => _xVel;
        public float yVel => _yVel;
        public float mVel => _mVel;
        public float aVel => _aVel;
        public float mAcc => _mAcc;
        public float rAcc => _rAcc;
        public TuioState state => _state;

        public List<Tuio20Point> GetPath()
        {
            return _prevPoints.ToList();
        }

        internal void _updateComponent(TuioTime currentTime, float xPos, float yPos, float angle,
            float xVel, float yVel, float aVel, float mAcc, float rAcc)
        {
            _currentTime = currentTime;
            _xPos = xPos;
            _yPos = yPos;
            _angle = angle;
            _xVel = xVel;
            _yVel = yVel;
            _mVel = (float)Math.Sqrt(xVel * xVel + yVel * yVel);
            _aVel = aVel;
            _mAcc = mAcc;
            _rAcc = rAcc;
            _prevPoints.Enqueue(new Tuio20Point(currentTime, xPos, yPos));
            if (_prevPoints.Count > MAX_PATH_LENGTH)
            {
                _prevPoints.Dequeue();
            }
            if (_mAcc > 0)
            {
                _state = TuioState.Accelerating;
            }
            else if (_mAcc < 0)
            {
                _state = TuioState.Decelerating;
            }
            else if (_rAcc != 0 && _state == TuioState.Stopped)
            {
                _state = TuioState.Rotating;
            }
            else 
            {
                _state = TuioState.Stopped;
            }
            _container._update(currentTime);
        }
        
        internal void _remove(TuioTime currentTime)
        {
            _currentTime = currentTime;
            _state = TuioState.Removed;
        }
    }
}