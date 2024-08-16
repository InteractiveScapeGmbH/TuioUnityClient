using TuioNet.Tuio20;
using TuioUnity.Common;
using TuioUnity.Utils;
using UnityEngine;

namespace TuioUnity.Tuio20.Sxm
{
    public class ScapeXMobileTransform : Tuio20ComponentBehaviour
    {
        public Tuio20Bounds Bounds { get; private set; }
        public Tuio20Symbol Symbol { get; private set; }

        public override void Initialize(Tuio20Object tuioObject, RenderMode renderMode = RenderMode.ScreenSpaceOverlay)
        {
            base.Initialize(tuioObject, renderMode);
            Bounds = tuioObject.Bounds;
            Symbol = tuioObject.Symbol;
            SetupSize();
        }

        protected override Tuio20Component GetTransformComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Bounds;
        }

        private void SetupSize()
        {
            RectTransform.sizeDelta = TuioTransform.GetScreenSpaceSize(Bounds.Size.ToUnity());
        }

        public override string DebugText()
        {
            return $"ID: {Symbol.Data} \nAngle: {(Mathf.Rad2Deg * Bounds.Angle):f2}\u00b0 \nPosition: {RectTransform.anchoredPosition} \nSize: {Bounds.Size}";
        }
    }
}