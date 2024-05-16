using System;
using TuioNet.Common;
using TuioNet.Tuio11;
using TuioUnity.Common;

namespace TuioUnity.Tuio11
{
    public class Tuio11Manager : ITuioManager
    {

        private Tuio11Processor _processor;
        
        public event Action<Tuio11Cursor> OnCursorAdd;
        public event Action<Tuio11Cursor> OnCursorUpdate;
        public event Action<Tuio11Cursor> OnCursorRemove;

        public event Action<Tuio11Object> OnObjectAdd;
        public event Action<Tuio11Object> OnObjectUpdate;
        public event Action<Tuio11Object> OnObjectRemove;
        
        public event Action<Tuio11Blob> OnBlobAdd;
        public event Action<Tuio11Blob> OnBlobUpdate;
        public event Action<Tuio11Blob> OnBlobRemove;

        public event Action<TuioTime> OnRefresh;
        
        
        private void AddCursor(Tuio11Cursor tuioCursor)
        {
            OnCursorAdd?.Invoke(tuioCursor);
        }

        private void RemoveCursor(Tuio11Cursor tuioCursor)
        {
            OnCursorRemove?.Invoke(tuioCursor);
        }

        private void UpdateCursor(Tuio11Cursor tuioCursor)
        {
            OnCursorUpdate?.Invoke(tuioCursor);
        }

        private void AddObject(Tuio11Object tuioObject)
        {
            OnObjectAdd?.Invoke(tuioObject);
        }

        private void UpdateObject(Tuio11Object tuioObject)
        {
            OnObjectUpdate?.Invoke(tuioObject);
        }

        private void RemoveObject(Tuio11Object tuioObject)
        {
            OnObjectRemove?.Invoke(tuioObject);
        }

        private void AddBlob(Tuio11Blob tuioBlob)
        {
            OnBlobAdd?.Invoke(tuioBlob);
        }
        
        private void UpdateBlob(Tuio11Blob tuioBlob)
        {
            OnBlobUpdate?.Invoke(tuioBlob);
        }
        
        private void RemoveBlob(Tuio11Blob tuioBlob)
        {
            OnBlobRemove?.Invoke(tuioBlob);
        }

        private void Refresh(TuioTime tuioTime)
        {
            OnRefresh?.Invoke(tuioTime);
        }

        public void SetupProcessor(TuioClient tuioClient)
        {
            _processor = new Tuio11Processor(tuioClient);
        }

        public void RegisterCallbacks()
        {
            _processor.OnCursorAdded += AddCursor;
            _processor.OnCursorUpdated += UpdateCursor;
            _processor.OnCursorRemoved += RemoveCursor;

            _processor.OnObjectAdded += AddObject;
            _processor.OnObjectUpdated += UpdateObject;
            _processor.OnObjectRemoved += RemoveObject;

            _processor.OnBlobAdded += AddBlob;
            _processor.OnBlobUpdated += UpdateBlob;
            _processor.OnBlobRemoved += RemoveBlob;

            _processor.OnRefreshed += Refresh;
        }

        public void UnregisterCallbacks()
        {
            _processor.OnCursorAdded -= AddCursor;
            _processor.OnCursorUpdated -= UpdateCursor;
            _processor.OnCursorRemoved -= RemoveCursor;
            
            _processor.OnObjectAdded -= AddObject;
            _processor.OnObjectUpdated -= UpdateObject;
            _processor.OnObjectRemoved -= RemoveObject;

            _processor.OnBlobAdded -= AddBlob;
            _processor.OnBlobUpdated -= UpdateBlob;
            _processor.OnBlobRemoved -= RemoveBlob;

            _processor.OnRefreshed -= Refresh;
        }
    }
}
