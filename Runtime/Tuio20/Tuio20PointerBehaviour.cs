using System;
using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20PointerBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Pointer Pointer { get; private set; }

        public override void Initialize(Tuio20Object tuioObject)
        {
            base.Initialize(tuioObject);
            Pointer = tuioObject.Pointer;
        }

        protected override Tuio20Component GetComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Pointer;
        }

        public override string DebugText()
        {
            return $"ID: {Pointer.SessionId}";
        }
    }
}