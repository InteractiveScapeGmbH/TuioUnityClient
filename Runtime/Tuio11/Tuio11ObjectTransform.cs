using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11ObjectTransform : Tuio11Behaviour
    {
        private Tuio11Object _tuioObject;

        public override void Initialize(Tuio11Container container, RenderMode renderMode = RenderMode.ScreenSpaceOverlay)
        {
            _tuioObject = (Tuio11Object)container;
            base.Initialize(container, renderMode);
        }

        protected override void UpdateContainer()
        {
            base.UpdateContainer();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            var angle = -Mathf.Rad2Deg * _tuioObject.Angle;
            RectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public override string DebugText()
        {
            return $"ID: {_tuioObject.SymbolId} \nAngle: {(Mathf.Rad2Deg * _tuioObject.Angle):f2}\u00b0 \nPosition: {_tuioObject.Position}";
        }
    }
}