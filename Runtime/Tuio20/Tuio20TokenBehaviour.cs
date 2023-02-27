using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20TokenBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Token TuioToken { get; private set; }

        public override void Initialize(Tuio20Component component)
        {
            base.Initialize(component);
            TuioToken = (Tuio20Token)component;
        }
    }
}