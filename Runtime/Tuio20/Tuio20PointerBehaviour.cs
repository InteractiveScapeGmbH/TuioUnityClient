using System;
using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20PointerBehaviour : Tuio20ComponentBehaviour
    {
        private Tuio20Pointer _pointer;

        public override void Initialize(Tuio20Object tuioObject)
        {
            base.Initialize(tuioObject);
            _pointer = tuioObject.Pointer;
        }

        protected override Tuio20Component GetComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Pointer;
        }

        public override string DebugText()
        {
            return $"ID: {_pointer.SessionId}";
        }
    }
}