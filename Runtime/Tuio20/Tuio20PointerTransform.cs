using TuioNet.Tuio20;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    public class Tuio20PointerTransform : Tuio20ComponentBehaviour
    {
        public Tuio20Pointer Pointer { get; private set; }

        public override void Initialize(Tuio20Object tuioObject, RenderMode renderMode = RenderMode.ScreenSpaceOverlay)
        {
            base.Initialize(tuioObject, renderMode);
            Pointer = tuioObject.Pointer;
        }

        protected override Tuio20Component GetTransformComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Pointer;
        }

        public override string DebugText()
        {
            return $"ID: {Pointer.SessionId}";
        }
    }
}