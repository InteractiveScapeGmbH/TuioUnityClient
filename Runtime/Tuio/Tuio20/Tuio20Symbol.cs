using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Symbol : Tuio20Component
    {
        private uint _tuId;
        private uint _cId;
        private string _group;
        private string _data;
        
        public Tuio20Symbol(TuioTime startTime, Tuio20Object container, uint tuId, uint cId, string group, string data) : base(startTime, container, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            _tuId = tuId;
            _cId = cId;
            _group = group;
            _data = data;
        }
        
        public uint tuId => _tuId;
        public uint cId => _cId;
        public string group => _group;
        public string data => _data;

        internal bool _hasChanged(uint tuId, uint cId, string group, string data)
        {
            return !(tuId == _tuId && cId == _cId && group == _group && data == _data);
        }

        internal void _update(TuioTime currentTime, uint tuId, uint cId, string group, string data)
        {
            _updateComponent(currentTime, 0, 0, 0, 0, 0, 0, 0, 0);
            _tuId = tuId;
            _cId = cId;
            _group = group;
            _data = data;
        }
    }
}