using System;
using Tuio.Common;

namespace Tuio.Tuio11
{
    public class Tuio11Cursor : Tuio11Container
    {
        protected uint _cursorId;
        
        public Tuio11Cursor(TuioTime startTime, uint sessionId, uint cursorId, float xPos, float yPos, float xSpeed, float ySpeed, float motionAccel) : base(startTime, sessionId, xPos, yPos, xSpeed, ySpeed, motionAccel)
        {
            _cursorId = cursorId;
        }
        
        public uint cursorId => _cursorId;

        internal bool _hasChanged(float xPos, float yPos, float xSpeed, float ySpeed, float motionAccel)
        {
            return !(xPos == _xPos && yPos == _yPos && xSpeed == _xSpeed && ySpeed == _ySpeed && motionAccel == _motionAccel);
        }

        internal void _update(TuioTime currentTime, float xPos, float yPos, float xSpeed, float ySpeed, float motionAccel)
        {
            var isCalculateSpeeds = (xPos != _xPos && xSpeed == 0) || (yPos != _yPos && ySpeed == 0);
            _updateContainer(currentTime, xPos, yPos, xSpeed, ySpeed, motionAccel, isCalculateSpeeds);
        }
    }
}