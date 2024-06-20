using System.Collections.Generic;
using TuioNet.Tuio11;
using TuioUnity.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace TuioUnity.Tuio11
{
    /// <summary>
    /// Basic example how to implement a simple visualisation of Tuio 1.1 objects by registering on the Add and Remove
    /// events to spawn and destroy UI elements for each type.
    /// </summary>
    public class Tuio11Visualizer: MonoBehaviour
    {
        [FormerlySerializedAs("_tuioSession")] [SerializeField] private TuioSessionBehaviour _tuioSessionBehaviour;
        [SerializeField] private Tuio11CursorTransform _cursorPrefab;
        [SerializeField] private Tuio11ObjectTransform _objectPrefab;
        [SerializeField] private Tuio11BlobTransform _blobPrefab;

        private readonly Dictionary<uint, Tuio11Behaviour> _tuioBehaviours = new();

        private Tuio11Dispatcher _dispatcher;

        private void Start()
        {
            _dispatcher = (Tuio11Dispatcher)_tuioSessionBehaviour.TuioDispatcher;
        }

        private void OnEnable()
        {
            _dispatcher.OnCursorAdd += AddTuioCursor;
            _dispatcher.OnCursorRemove += RemoveTuioCursor;

            _dispatcher.OnObjectAdd += AddTuioObject;
            _dispatcher.OnObjectRemove += RemoveTuioObject;

            _dispatcher.OnBlobAdd += AddTuioBlob;
            _dispatcher.OnBlobRemove += RemoveTuioBlob;
        }

        private void OnDisable()
        {
            _dispatcher.OnCursorAdd -= AddTuioCursor;
            _dispatcher.OnCursorRemove -= RemoveTuioCursor;
            
            _dispatcher.OnObjectAdd -= AddTuioObject;
            _dispatcher.OnObjectRemove -= RemoveTuioObject;
            
            _dispatcher.OnBlobAdd -= AddTuioBlob;
            _dispatcher.OnBlobRemove -= RemoveTuioBlob;
        }

        private void AddTuioCursor(object sender, Tuio11Cursor tuioCursor)
        {
            var tuio11CursorBehaviour = Instantiate(_cursorPrefab, transform);
            tuio11CursorBehaviour.Initialize(tuioCursor);
            _tuioBehaviours.Add(tuioCursor.SessionId,tuio11CursorBehaviour);
        }

        private void RemoveTuioCursor(object sender, Tuio11Cursor tuioCursor)
        {
            if (_tuioBehaviours.Remove(tuioCursor.SessionId, out var cursorBehaviour))
            {
                cursorBehaviour.Destroy();
            }
        }
        
        private void AddTuioObject(object sender, Tuio11Object tuioObject)
        {
            var objectBehaviour = Instantiate(_objectPrefab, transform);
            objectBehaviour.Initialize(tuioObject);
            _tuioBehaviours.Add(tuioObject.SessionId, objectBehaviour);
        }
        
        private void RemoveTuioObject(object sender, Tuio11Object tuioObject)
        {
            if (_tuioBehaviours.Remove(tuioObject.SessionId, out var objectBehaviour))
            {
                objectBehaviour.Destroy();
            }
        }

        private void AddTuioBlob(object sender, Tuio11Blob tuioBlob)
        {
            var blobBehaviour = Instantiate(_blobPrefab, transform);
            blobBehaviour.Initialize(tuioBlob);
            _tuioBehaviours.Add(tuioBlob.SessionId, blobBehaviour);
        }

        private void RemoveTuioBlob(object sender, Tuio11Blob tuioBlob)
        {
            if (_tuioBehaviours.Remove(tuioBlob.SessionId, out var blobBehaviour))
            {
                blobBehaviour.Destroy();
            }
        }
    }
}