using System;
using TuioNet.Common;
using TuioNet.Tuio20;
using TuioUnity.Common;

namespace TuioUnity.Tuio20
{
    /// <summary>
    /// This is a wrapper class around the underlying Tuio.Net implementation. It makes it possible to register on the
    /// Add, Update, Remove and Refresh events to get notified about changes in the current frame. 
    /// </summary>
    public class Tuio20Dispatcher : ITuioDispatcher
    {
        private Tuio20Processor _processor;
        
        /// <summary>
        /// Event gets triggered when Tuio 2.0 Object is added.
        /// </summary>
        public event Action<Tuio20Object> OnObjectAdd;
        
        /// <summary>
        /// Event gets triggered when Tuio 2.0 Object is updated.
        /// </summary>
        public event Action<Tuio20Object> OnObjectUpdate;
        
        /// <summary>
        /// Event gets triggered when Tuio 2.0 Object is removed.
        /// </summary>
        public event Action<Tuio20Object> OnObjectRemove;
        
        /// <summary>
        /// This event gets triggered at the end of the current frame after all tuio messages were processed and it
        /// provides the current TuioTime. This event is useful to handle all updates contained in one TUIO frame together.
        /// </summary>
        public event Action<TuioTime> OnRefresh; 

        private void AddObject(Tuio20Object tuioObject)
        {
            OnObjectAdd?.Invoke(tuioObject);
        }
        
        private void UpdateObject(Tuio20Object tuioObject)
        {
            OnObjectUpdate?.Invoke(tuioObject);
        }
        
        private void RemoveObject(Tuio20Object tuioObject)
        {
            OnObjectRemove?.Invoke(tuioObject);
        }

        private void Refresh(TuioTime tuioTime)
        {
            OnRefresh?.Invoke(tuioTime);
        }

        public void SetupProcessor(TuioClient tuioClient)
        {
            _processor = new Tuio20Processor(tuioClient);
        }

        public void RegisterCallbacks()
        {
            _processor.OnObjectAdded += AddObject;
            _processor.OnObjectUpdated += UpdateObject;
            _processor.OnObjectRemoved += RemoveObject;
            _processor.OnRefreshed += Refresh;
        }

        public void UnregisterCallbacks()
        {
            _processor.OnObjectAdded -= AddObject;
            _processor.OnObjectUpdated -= UpdateObject;
            _processor.OnObjectRemoved -= RemoveObject;
            _processor.OnRefreshed -= Refresh;
        }
    }
}
