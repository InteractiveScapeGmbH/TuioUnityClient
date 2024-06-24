using TuioNet.Tuio11;

namespace TuioUnity.Tuio11
{
    public class Tuio11CursorTransform : Tuio11Behaviour
    {
        private Tuio11Cursor _tuioCursor;

        public override void Initialize(Tuio11Container container)
        {
            _tuioCursor = (Tuio11Cursor)container;
            base.Initialize(container);
        }

        public override string DebugText()
        {
            return $"ID: {_tuioCursor.SessionId}";
        }
    }
}
