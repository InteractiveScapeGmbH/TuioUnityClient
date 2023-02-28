using System;
using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20PointerBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Pointer TuioPointer { get; private set; }
        public override uint SessionId { get; protected set; }
        public override uint Id { get; protected set; }

        public override event Action OnUpdate;
        public override void Initialize(Tuio20Component component)
        {
            base.Initialize(component);
            TuioPointer = (Tuio20Pointer)component;
            SessionId = TuioPointer.SessionId;
            Id = TuioPointer.ComponentId;
        }

        protected override void UpdateComponent()
        {
            base.UpdateComponent();
            OnUpdate?.Invoke();
        }
    }
}