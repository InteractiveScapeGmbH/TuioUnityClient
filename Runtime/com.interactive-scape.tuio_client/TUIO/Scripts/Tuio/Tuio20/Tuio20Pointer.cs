using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Pointer : Tuio20Component
    {
        private uint _tuId;
        private uint _cId;
        private float _shear;
        private float _radius;
        private float _press;
        private float _pVel;
        private float _pAcc;
        
        public Tuio20Pointer(TuioTime startTime, Tuio20Object container, uint tuId, uint cId, float xPos, float yPos, float angle, float shear, float radius, float press, float xVel, float yVel, float pVel, float mAcc, float pAcc) : base(startTime, container, xPos, yPos, angle, xVel, yVel, 0, mAcc, 0)
        {
            _tuId = tuId;
            _cId = cId;
            _shear = shear;
            _radius = radius;
            _press = press;
            _pVel = pVel;
            _pAcc = pAcc;
        }
        
        public uint tuId => _tuId;
        public uint cId => _cId;
        public float shear => _shear;
        public float radius => _radius;
        public float press => _press;
        public float pVel => _pVel;
        public float pAcc => _pAcc;

        internal bool _hasChanged(uint tuId, uint cId, float xPos, float yPos, float angle, float shear, float radius, float press, float xVel, float yVel, float pVel, float mAcc, float pAcc)
        {
            return !(tuId == _tuId && cId == _cId && xPos == _xPos && yPos == _yPos && angle == _angle && shear == _shear &&radius == _radius &&press == _press &&
                     xVel == _xVel && yVel == _yVel && pVel == _pVel && mAcc == _mAcc && pAcc == _pAcc);
        }

        internal void _update(TuioTime currentTime, uint tuId, uint cId, float xPos, float yPos, float angle, float shear, float radius, float press, float xVel, float yVel, float pVel, float mAcc, float pAcc)
        {
            _updateComponent(currentTime, xPos, yPos, angle, xVel, yVel, 0, mAcc, 0);
            _tuId = tuId;
            _cId = cId;
            _shear = shear;
            _radius = radius;
            _press = press;
            _pVel = pVel;
            _pAcc = pAcc;
        }
    }
}