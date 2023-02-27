using TuioNet.Common;
using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11Visualizer: MonoBehaviour, Tuio11Listener
    {
        [SerializeField] private Tuio11CursorBehaviour _tuio11CursorPrefab;
        [SerializeField] private Tuio11ObjectBehaviour _tuio11ObjectPrefab;

        void Start()
        {
            Tuio11Manager.Instance.AddTuio11Listener(this);
        }

        public void AddTuioObject(Tuio11Object tuio11Object)
        {
            var tuio11ObjectBehaviour = Instantiate(_tuio11ObjectPrefab, transform);
            tuio11ObjectBehaviour.Initialize(tuio11Object);
        }

        public void UpdateTuioObject(Tuio11Object tuio11Object)
        {
        }

        public void RemoveTuioObject(Tuio11Object tuio11Object)
        {
        }

        public void AddTuioCursor(Tuio11Cursor tuio11Cursor)
        {
            var tuio11CursorBehaviour = Instantiate(_tuio11CursorPrefab, transform);
            tuio11CursorBehaviour.Initialize(tuio11Cursor);
        }

        public void UpdateTuioCursor(Tuio11Cursor tuio11Cursor)
        {
        }

        public void RemoveTuioCursor(Tuio11Cursor tuio11Cursor)
        {
            
        }

        public void AddTuioBlob(Tuio11Blob tuio11Blob)
        {
        }

        public void UpdateTuioBlob(Tuio11Blob tuio11Blob)
        {
        }

        public void RemoveTuioBlob(Tuio11Blob tuio11Blob)
        {
        }

        public void Refresh(TuioTime tuioTime)
        {
        }
    }
}