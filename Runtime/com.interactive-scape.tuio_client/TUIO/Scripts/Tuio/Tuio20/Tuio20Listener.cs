using Tuio.Common;

namespace Tuio.Tuio20
{
    public interface Tuio20Listener
    {
        public void TuioAdd(Tuio20Object tuio20Object);
        
        public void TuioUpdate(Tuio20Object tuio20Object);
        
        public void TuioRemove(Tuio20Object tuio20Object);
        
        public void TuioRefresh(TuioTime tuioTime);
    }
}