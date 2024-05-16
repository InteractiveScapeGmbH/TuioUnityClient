using System;
using TuioNet.Common;
using TuioNet.Tuio20;
using TuioUnity.Common;

namespace TuioUnity.Tuio20
{
    public class Tuio20Manager : ITuioManager
    {
        private Tuio20Processor _processor;
        public event Action<Tuio20Object> OnObjectAdd;
        public event Action<Tuio20Object> OnObjectUpdate;
        public event Action<Tuio20Object> OnObjectRemove;
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
