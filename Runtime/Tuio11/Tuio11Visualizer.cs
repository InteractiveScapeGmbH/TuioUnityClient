using System.Collections.Generic;
using TuioNet.Tuio11;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11Visualizer: MonoBehaviour
    {
        [SerializeField] private TuioManagerBehaviour _tuioManagerBehaviour;
        [SerializeField] private Tuio11CursorBehaviour _tuio11CursorPrefab;
        [SerializeField] private Tuio11ObjectBehaviour _tuio11ObjectPrefab;
        [SerializeField] private Tuio11BlobBehaviour _tuio11BlobPrefab;

        private readonly Dictionary<uint, Tuio11Behaviour> _tuioObjects = new();

        private Tuio11Manager _manager;

        private void Awake()
        {
            _manager = (Tuio11Manager)_tuioManagerBehaviour.TuioManager;
        }

        private void OnEnable()
        {
            _manager.OnCursorAdd += AddTuioCursor;
            _manager.OnCursorRemove += RemoveTuioCursor;

            _manager.OnObjectAdd += AddTuioObject;
            _manager.OnObjectRemove += RemoveTuioObject;

            _manager.OnBlobAdd += AddTuioBlob;
            _manager.OnBlobRemove += RemoveTuioBlob;
        }

        private void OnDisable()
        {
            _manager.OnCursorAdd -= AddTuioCursor;
            _manager.OnCursorRemove -= RemoveTuioCursor;
            
            _manager.OnObjectAdd -= AddTuioObject;
            _manager.OnObjectRemove -= RemoveTuioObject;
            
            _manager.OnBlobAdd -= AddTuioBlob;
            _manager.OnBlobRemove -= RemoveTuioBlob;
        }

        private void AddTuioCursor(Tuio11Cursor tuio11Cursor)
        {
            var tuio11CursorBehaviour = Instantiate(_tuio11CursorPrefab, transform);
            tuio11CursorBehaviour.Initialize(tuio11Cursor);
            _tuioObjects.Add(tuio11Cursor.SessionId,tuio11CursorBehaviour);
        }

        private void RemoveTuioCursor(Tuio11Cursor tuioCursor)
        {
            if (_tuioObjects.Remove(tuioCursor.SessionId, out var cursorBehaviour))
            {
                cursorBehaviour.Destroy();
            }
        }
        
        private void AddTuioObject(Tuio11Object tuioObject)
        {
            var objectBehaviour = Instantiate(_tuio11ObjectPrefab, transform);
            objectBehaviour.Initialize(tuioObject);
            _tuioObjects.Add(tuioObject.SessionId, objectBehaviour);
        }
        
        private void RemoveTuioObject(Tuio11Object tuioObject)
        {
            if (_tuioObjects.Remove(tuioObject.SessionId, out var objectBehaviour))
            {
                objectBehaviour.Destroy();
            }
        }

        private void AddTuioBlob(Tuio11Blob tuioBlob)
        {
            var blobBehaviour = Instantiate(_tuio11BlobPrefab, transform);
            blobBehaviour.Initialize(tuioBlob);
            _tuioObjects.Add(tuioBlob.SessionId, blobBehaviour);
        }

        private void RemoveTuioBlob(Tuio11Blob tuioBlob)
        {
            if (_tuioObjects.Remove(tuioBlob.SessionId, out var blobBehaviour))
            {
                blobBehaviour.Destroy();
            }
        }
    }
}