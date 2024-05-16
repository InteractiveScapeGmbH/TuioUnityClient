using System;
using TuioNet.Common;
using TuioNet.Tuio11;
using TuioUnity.Common;

namespace TuioUnity.Tuio11
{
    /// <summary>
    /// This is a wrapper class around the underlying Tuio.Net implementation. It makes it possible to register on the
    /// Add, Update, Remove and Refresh events to get notified about changes in the current frame. 
    /// </summary>
    public class Tuio11Dispatcher : ITuioDispatcher
    {
        private Tuio11Processor _processor;
        
        /// <summary>
        /// Event gets triggered when a new TUIO 1.1 cursor was recognized in this frame.
        /// </summary>
        public event Action<Tuio11Cursor> OnCursorAdd;
        
        /// <summary>
        /// Event gets triggered when a known TUIO 1.1 cursor was updated in this frame.
        /// </summary>
        public event Action<Tuio11Cursor> OnCursorUpdate;
        
        /// <summary>
        /// Event gets triggered when a TUIO 1.1 cursor was removed in this frame.
        /// </summary>
        public event Action<Tuio11Cursor> OnCursorRemove;

        /// <summary>
        /// Event gets triggered when a new TUIO 1.1 object was recognized in this frame.
        /// </summary>
        public event Action<Tuio11Object> OnObjectAdd;
        
        /// <summary>
        /// Event gets triggered when a known TUIO 1.1 object was updated in this frame.
        /// </summary>
        public event Action<Tuio11Object> OnObjectUpdate;
        
        /// <summary>
        /// Event gets triggered when a TUIO 1.1 object was removed in this frame.
        /// </summary>
        public event Action<Tuio11Object> OnObjectRemove;
        
        /// <summary>
        /// Event gets triggered when a new TUIO 1.1 blob was recognized in this frame.
        /// </summary>
        public event Action<Tuio11Blob> OnBlobAdd;
        
        /// <summary>
        /// Event gets triggered when a known TUIO 1.1 blob was updated in this frame.
        /// </summary>
        public event Action<Tuio11Blob> OnBlobUpdate;
        
        /// <summary>
        /// Event gets triggered when a TUIO 1.1 blob was removed in this frame.
        /// </summary>
        public event Action<Tuio11Blob> OnBlobRemove;

        /// <summary>
        /// This event gets triggered at the end of the current frame after all tuio messages were processed and it
        /// provides the current TuioTime. This event is useful to handle all updates contained in one TUIO frame together.
        /// </summary>
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
