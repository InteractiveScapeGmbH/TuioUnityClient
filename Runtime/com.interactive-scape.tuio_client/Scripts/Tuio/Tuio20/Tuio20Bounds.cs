using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Bounds: Tuio20Component
    {
        private float _width;
        private float _height;
        private float _area;
        
        public Tuio20Bounds(TuioTime startTime, Tuio20Object container, float xPos, float yPos, float angle, float width, float height, float area, float xVel, float yVel, float aVel, float mAcc, float rAcc) : base(startTime, container, xPos, yPos, angle, xVel, yVel, aVel, mAcc, rAcc)
        {
            _width = width;
            _height = height;
            _area = area;
        }
        
        public float width => _width;
        public float height => _height;
        public float area => _area;

        internal bool _hasChanged(float xPos, float yPos, float angle, float width, float height, float area, float xVel, float yVel, float aVel, float mAcc, float rAcc)
        {
            return !(xPos == _xPos && yPos == _yPos && angle == _angle && width == _width && height == _height && area == _area &&
                     xVel == _xVel && yVel == _yVel && aVel == _aVel && mAcc == _mAcc && rAcc == _rAcc);
        }

        internal void _update(TuioTime currentTime, float xPos, float yPos, float angle, float width, float height, float area, float xVel, float yVel, float aVel, float mAcc, float rAcc)
        {
            _updateComponent(currentTime, xPos, yPos, angle, xVel, yVel, aVel, mAcc, rAcc);
            _width = width;
            _height = height;
            _area = area;
        }
    }
}