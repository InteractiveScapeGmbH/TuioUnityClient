using Tuio.Common;

namespace Tuio.Tuio11
{
    public interface Tuio11Listener
    {
        public void AddTuioObject(Tuio11Object tuio11Object);
        
        public void UpdateTuioObject(Tuio11Object tuio11Object);
        
        public void RemoveTuioObject(Tuio11Object tuio11Object);
        
        public void AddTuioCursor(Tuio11Cursor tuio11Cursor);
        
        public void UpdateTuioCursor(Tuio11Cursor tuio11Cursor);
        
        public void RemoveTuioCursor(Tuio11Cursor tuio11Cursor);
        
        public void AddTuioBlob(Tuio11Blob tuio11Blob);
        
        public void UpdateTuioBlob(Tuio11Blob tuio11Blob);
        
        public void RemoveTuioBlob(Tuio11Blob tuio11Blob);
        
        public void Refresh(TuioTime tuioTime);
    }
}