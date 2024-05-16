using System.Collections.Generic;
using TuioNet.Tuio20;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    /// <summary>
    /// Basic example how to implement a simple visualisation of Tuio 2.0 objects by registering on the Add and Remove
    /// events to spawn and destroy UI elements for each type.
    /// </summary>
    public class Tuio20Visualizer: MonoBehaviour
    {
        [SerializeField] private TuioSession _tuioSession;
        [SerializeField] private Tuio20TokenBehaviour _tokenPrefab;
        [SerializeField] private Tuio20PointerBehaviour _pointerPrefab;
        [SerializeField] private PhoneBehaviour _phonePrefab;

        private readonly Dictionary<uint, Tuio20ComponentBehaviour> _tuioBehaviours = new();

        private Tuio20Dispatcher Dispatcher => (Tuio20Dispatcher)_tuioSession.TuioDispatcher;

        private void OnEnable()
        {
            Dispatcher.OnObjectAdd += SpawnTuioObject;
            Dispatcher.OnObjectRemove += DestroyTuioObject;
        }

        private void OnDisable()
        {
            Dispatcher.OnObjectAdd -= SpawnTuioObject;
            Dispatcher.OnObjectRemove -= DestroyTuioObject;
        }

        private void SpawnTuioObject(Tuio20Object tuioObject)
        {
            if (tuioObject.ContainsNewTuioPointer())
            {
                var pointerBehaviour = Instantiate(_pointerPrefab, transform);
                pointerBehaviour.Initialize(tuioObject);
                _tuioBehaviours.Add(tuioObject.SessionId, pointerBehaviour);
                return;
            }
            
            if (tuioObject.ContainsNewTuioToken())
            {
                var tokenBehaviour = Instantiate(_tokenPrefab, transform);
                tokenBehaviour.Initialize(tuioObject);
                _tuioBehaviours.Add(tuioObject.SessionId, tokenBehaviour);
                return;
            }
            
            if (tuioObject.ContainsNewTuioSymbol())
            {
                var symbolBehaviour = Instantiate(_phonePrefab, transform);
                symbolBehaviour.Initialize(tuioObject);
                _tuioBehaviours.Add(tuioObject.SessionId, symbolBehaviour);
                return;
            }
            
        }

        private void DestroyTuioObject(Tuio20Object tuioObject)
        {
            if (_tuioBehaviours.Remove(tuioObject.SessionId, out var pointerBehaviour))
            {
                pointerBehaviour.Destroy();
            }
            
            if (_tuioBehaviours.Remove(tuioObject.SessionId, out var tokenBehaviour))
            {
                tokenBehaviour.Destroy();
            }
            
            if (_tuioBehaviours.Remove(tuioObject.SessionId, out var symbolBehaviour))
            {
                symbolBehaviour.Destroy();
            }
        }
       
    }
}
