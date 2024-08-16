using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11CursorTransform : Tuio11Behaviour
    {
        private Tuio11Cursor _tuioCursor;

        public override void Initialize(Tuio11Container container, RenderMode renderMode = RenderMode.ScreenSpaceOverlay)
        {
            _tuioCursor = (Tuio11Cursor)container;
            base.Initialize(container, renderMode);
        }

        public override string DebugText()
        {
            return $"ID: {_tuioCursor.SessionId}";
        }
    }
}
