﻿using TuioNet.Tuio20;
using TuioUnity.Common;
using TuioUnity.Utils;

namespace TuioUnity.Tuio20
{
    public class ScapeXMobileBehaviour : Tuio20ComponentBehaviour
    {
        public Tuio20Bounds Bounds { get; private set; }
        public Tuio20Symbol Symbol { get; private set; }

        public override void Initialize(Tuio20Object tuioObject)
        {
            base.Initialize(tuioObject);
            Bounds = tuioObject.Bounds;
            Symbol = tuioObject.Symbol;
            SetupSize();
        }

        protected override Tuio20Component GetComponent(Tuio20Object tuioObject)
        {
            return tuioObject.Bounds;
        }

        private void SetupSize()
        {
            RectTransform.sizeDelta = TuioTransform.GetScreenSpaceSize(Bounds.Size.ToUnity());
        }

        public override string DebugText()
        {
            return $"ID: {Symbol.Data} \nAngle: {Bounds.Angle:f2}\u00b0 \nPosition: {RectTransform.anchoredPosition} \nSize: {Bounds.Size}";
        }
    }
}