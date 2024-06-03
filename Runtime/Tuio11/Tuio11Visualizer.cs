using System.Collections.Generic;
using TuioNet.Tuio11;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    /// <summary>
    /// Basic example how to implement a simple visualisation of Tuio 1.1 objects by registering on the Add and Remove
    /// events to spawn and destroy UI elements for each type.
    /// </summary>
    public class Tuio11Visualizer: MonoBehaviour
    {
        [SerializeField] private TuioSession _tuioSession;
        [SerializeField] private Tuio11CursorTransform _cursorPrefab;
        [SerializeField] private Tuio11ObjectTransform _objectPrefab;
        [SerializeField] private Tuio11BlobTransform _blobPrefab;

        private readonly Dictionary<uint, Tuio11Behaviour> _tuioBehaviours = new();

        private Tuio11Dispatcher _dispatcher;

        private void Awake()
        {
            _dispatcher = (Tuio11Dispatcher)_tuioSession.TuioDispatcher;
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

        private void AddTuioCursor(Tuio11Cursor tuio11Cursor)
        {
            var tuio11CursorBehaviour = Instantiate(_cursorPrefab, transform);
            tuio11CursorBehaviour.Initialize(tuio11Cursor);
            _tuioBehaviours.Add(tuio11Cursor.SessionId,tuio11CursorBehaviour);
        }

        private void RemoveTuioCursor(Tuio11Cursor tuioCursor)
        {
            if (_tuioBehaviours.Remove(tuioCursor.SessionId, out var cursorBehaviour))
            {
                cursorBehaviour.Destroy();
            }
        }
        
        private void AddTuioObject(Tuio11Object tuioObject)
        {
            var objectBehaviour = Instantiate(_objectPrefab, transform);
            objectBehaviour.Initialize(tuioObject);
            _tuioBehaviours.Add(tuioObject.SessionId, objectBehaviour);
        }
        
        private void RemoveTuioObject(Tuio11Object tuioObject)
        {
            if (_tuioBehaviours.Remove(tuioObject.SessionId, out var objectBehaviour))
            {
                objectBehaviour.Destroy();
            }
        }

        private void AddTuioBlob(Tuio11Blob tuioBlob)
        {
            var blobBehaviour = Instantiate(_blobPrefab, transform);
            blobBehaviour.Initialize(tuioBlob);
            _tuioBehaviours.Add(tuioBlob.SessionId, blobBehaviour);
        }

        private void RemoveTuioBlob(Tuio11Blob tuioBlob)
        {
            if (_tuioBehaviours.Remove(tuioBlob.SessionId, out var blobBehaviour))
            {
                blobBehaviour.Destroy();
            }
        }
    }
}