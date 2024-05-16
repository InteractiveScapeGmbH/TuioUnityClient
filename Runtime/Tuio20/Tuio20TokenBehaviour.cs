using System;
using TuioNet.Tuio20;

namespace TuioUnity.Tuio20
{
    public class Tuio20TokenBehaviour : Tuio20ComponentBehaviour
    {
        private Tuio20Token _token;

        public override void Initialize(Tuio20Object tuioObject)
        {
            base.Initialize(tuioObject);
            _token = tuioObject.Token;
        }

        protected override Tuio20Component GetComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Token;
        }

        public override string DebugText()
        {
            return $"ID: {_token.ComponentId} \nAngle: {_token.Angle:f2}\u00b0 \nPosition: {RectTransform.anchoredPosition}";
        }
    }
}