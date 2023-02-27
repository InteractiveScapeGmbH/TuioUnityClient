using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20PointerBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Pointer TuioPointer { get; private set; }

        public override void Initialize(Tuio20Component component)
        {
            base.Initialize(component);
            TuioPointer = (Tuio20Pointer)component;
        }
    }
}