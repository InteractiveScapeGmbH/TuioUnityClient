using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Object
    {
        private TuioTime _startTime;
        private TuioTime _currentTime;
        private uint _sessionId;
        private Tuio20Token _token;
        private Tuio20Pointer _pointer;
        private Tuio20Bounds _bounds;
        private Tuio20Symbol _symbol;
        private TuioState _state;

        public Tuio20Object(TuioTime startTime, uint sessionId)
        {
            _startTime = startTime;
            _currentTime = startTime;
            _sessionId = sessionId;
            _token = null;
            _pointer = null;
            _bounds = null;
            _symbol = null;
            _state = TuioState.Added;
        }
        
        public TuioTime startTime => _startTime;
        public TuioTime currentTime => _currentTime;
        public uint sessionId => _sessionId;
        public Tuio20Token token => _token;
        public Tuio20Pointer pointer => _pointer;
        public Tuio20Bounds bounds => _bounds;
        public Tuio20Symbol symbol => _symbol;
        public TuioState state => _state;

        public void SetTuioToken(Tuio20Token token)
        {
            _token = token;
            _state = TuioState.Added;
        }

        public void SetTuioPointer(Tuio20Pointer pointer)
        {
            _pointer = pointer;
            _state = TuioState.Added;
        }

        public void SetTuioBounds(Tuio20Bounds bounds)
        {
            _bounds = bounds;
            _state = TuioState.Added;
        }

        public void SetTuioSymbol(Tuio20Symbol symbol)
        {
            _symbol = symbol;
            _state = TuioState.Added;
        }
        
        public bool ContainsTuioToken()
        {
            return _token != null;
        }

        public bool ContainsTuioPointer()
        {
            return _pointer != null;
        }

        public bool ContainsTuioBounds()
        {
            return _bounds != null;
        }

        public bool ContainsTuioSymbol()
        {
            return _symbol != null;
        }
        
        public bool ContainsNewTuioToken()
        {
            return _token is {state: TuioState.Added};
        }

        public bool ContainsNewTuioPointer()
        {
            return _pointer is {state: TuioState.Added};
        }

        public bool ContainsNewTuioBounds()
        {
            return _bounds is {state: TuioState.Added};
        }

        public bool ContainsNewTuioSymbol()
        {
            return _symbol is {state: TuioState.Added};
        }
        
        internal void _update(TuioTime currentTime)
        {
            _currentTime = currentTime;
            _state = TuioState.Idle;
        }

        internal void _remove(TuioTime currentTime)
        {
            _currentTime = currentTime;
            _token?._remove(currentTime);
            _pointer?._remove(currentTime);
            _bounds?._remove(currentTime);
            _symbol?._remove(currentTime);
            _state = TuioState.Removed;
        }
    }
}