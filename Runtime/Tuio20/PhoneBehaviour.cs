using TuioNet.Tuio20;
using TuioUnity.Common;
using TuioUnity.Utils;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    public class PhoneBehaviour : Tuio20ComponentBehaviour
    {
        private Tuio20Bounds _bounds;
        private Tuio20Symbol _symbol;
        
        public override void Initialize(Tuio20Object tuioObject)
        {
            base.Initialize(tuioObject);
            _bounds = tuioObject.Bounds;
            _symbol = tuioObject.Symbol;
            SetupSize();
        }

        protected override Tuio20Component GetComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Bounds;
        }

        private void SetupSize()
        {
            RectTransform.sizeDelta = TuioTransform.GetScreenSpaceSize(_bounds.Size.ToUnity());
        }

        public override string DebugText()
        {
            return $"ID: {_symbol.Data} \nAngle: {_bounds.Angle:f2}\u00b0 \nPosition: {RectTransform.anchoredPosition} \nSize: {_bounds.Size}";
        }
    }
}