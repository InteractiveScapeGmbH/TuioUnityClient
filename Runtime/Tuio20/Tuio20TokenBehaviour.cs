using System;
using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20TokenBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Token Token { get; private set; }

        public override void Initialize(Tuio20Object tuioObject)
        {
            base.Initialize(tuioObject);
            Token = tuioObject.Token;
        }

        protected override Tuio20Component GetTransformComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Token;
        }

        public override string DebugText()
        {
            return $"ID: {Token.ComponentId} \nAngle: {Token.Angle:f2}\u00b0 \nPosition: {RectTransform.anchoredPosition}";
        }
    }
}