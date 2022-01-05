using System;
using Tuio.Common;

namespace Tuio.Tuio11
{
    public class Tuio11Object : Tuio11Container
    {
        protected uint _symbolId;
        protected float _angle;
        protected float _rotationSpeed;
        protected float _rotationAccel;
        
        public Tuio11Object(TuioTime startTime, uint sessionId, uint symbolId, float xPos, float yPos, float angle, float xSpeed, float ySpeed, float rotationSpeed, float motionAccel, float rotationAccel) : base(startTime, sessionId, xPos, yPos, xSpeed, ySpeed, motionAccel)
        {
            _symbolId = symbolId;
            _angle = angle;
            _rotationSpeed = rotationSpeed;
            _rotationAccel = rotationAccel;
        }
        
        public uint symbolId => _symbolId;
        public float angle => _angle;
        public float rotationSpeed => _rotationSpeed;
        public float rotationAccel => _rotationAccel;

        internal bool _hasChanged(float xPos, float yPos, float angle, float xSpeed, float ySpeed, float rotationSpeed, float motionAccel, float rotationAccel)
        {
            return !(xPos == _xPos && yPos == _yPos && angle == _angle && xSpeed == _xSpeed && ySpeed == _ySpeed &&
                     rotationSpeed == _rotationSpeed && motionAccel == _motionAccel && rotationAccel == _rotationAccel);
        }

        internal void _update(TuioTime currentTime, float xPos, float yPos, float angle,
            float xSpeed, float ySpeed, float rotationSpeed, float motionAccel, float rotationAccel)
        {
            var lastPoint = _prevPoints[_prevPoints.Count - 1];
            var isCalculateSpeeds = (xPos != _xPos && xSpeed == 0) || (yPos != _yPos && ySpeed == 0);
            _updateContainer(currentTime, xPos, yPos, xSpeed, ySpeed, motionAccel, isCalculateSpeeds);

            var isCalculateRotation = angle != _angle && rotationSpeed == 0;
            if(isCalculateRotation)
            {
                var dt = currentTime.Subtract(lastPoint.startTime).GetTotalMilliseconds() / 1000.0f;
                if (dt > 0)
                {
                    var lastAngle = _angle;
                    var lastRotationSpeed = _rotationSpeed;
                    var da = (angle - lastAngle) / (2 * (float)Math.PI);
                    if (da > 0.5f)
                    {
                        da -= 1;
                    } 
                    else if (da <= -0.5f)
                    {
                        da += 1;
                    }
                    _rotationSpeed = da / dt;
                    _rotationAccel = (_rotationSpeed - lastRotationSpeed) / dt;
                }
            }
            else
            {
                _rotationSpeed = rotationSpeed;
                _rotationAccel = rotationAccel;
            }
            _angle = angle;

            if (_state != TuioState.Stopped && _rotationAccel != 0)
            {
                _state = TuioState.Rotating;
            }
        }
    }
}