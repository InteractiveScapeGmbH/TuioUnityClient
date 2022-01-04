using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Token : Tuio20Component
    {
        private uint _tuId;
        private uint _cId;
        
        public Tuio20Token(TuioTime startTime, Tuio20Object container, uint tuId, uint cId, float xPos, float yPos, float angle, float xVel, float yVel, float aVel, float mAcc, float rAcc) : base(startTime, container, xPos, yPos, angle, xVel, yVel, aVel, mAcc, rAcc)
        {
            _tuId = tuId;
            _cId = cId;
        }
        
        public uint tuId => _tuId;
        public uint cId => _cId;

        internal bool _hasChanged(uint tuId, uint cId, float xPos, float yPos, float angle, float xVel, float yVel,
            float aVel, float mAcc, float rAcc)
        {
            return !(tuId == _tuId && cId == _cId && xPos == _xPos && yPos == _yPos && angle == _angle &&
                     xVel == _xVel && yVel == _yVel && aVel == _aVel && mAcc == _mAcc && rAcc == _rAcc);
        }

        internal void _update(TuioTime currentTime, uint tuId, uint cId, float xPos, float yPos, float angle,
            float xVel, float yVel,
            float aVel, float mAcc, float rAcc)
        {
            _updateComponent(currentTime, xPos, yPos, angle, xVel, yVel, aVel, mAcc, rAcc);
            _tuId = tuId;
            _cId = cId;
        }
    }
}