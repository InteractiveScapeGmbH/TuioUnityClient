using System;
using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20TokenBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Token TuioToken { get; private set; }
        public override uint SessionId { get; protected set; }
        public override uint Id { get; protected set; }
        public override event Action OnUpdate;

        public override void Initialize(Tuio20Component component)
        {
            base.Initialize(component);
            TuioToken = (Tuio20Token)component;
            SessionId = TuioToken.SessionId;
            Id = TuioToken.CId;
        }

        protected override void UpdateComponent()
        {
            base.UpdateComponent();
            OnUpdate?.Invoke();
        }
    }
}