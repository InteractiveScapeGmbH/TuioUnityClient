using System;
using System.Collections.Generic;
using TuioNet.Tuio20;
using TuioUnity.Common;
using TuioUnity.Tuio20.Sxm;
using UnityEngine;
using UnityEngine.Serialization;

namespace TuioUnity.Tuio20
{
    /// <summary>
    /// Basic example how to implement a simple visualisation of Tuio 2.0 objects by registering on the Add and Remove
    /// events to spawn and destroy UI elements for each type.
    /// </summary>
    public class Tuio20Visualizer: MonoBehaviour
    {
        [SerializeField] private TuioSessionBehaviour _tuioSessionBehaviour;
        [SerializeField] private Tuio20TokenTransform _tokenPrefab;
        [SerializeField] private Tuio20PointerTransform _pointerPrefab;
        [SerializeField] private ScapeXMobileTransform _scapeXMobilePrefab;

        private readonly Dictionary<uint, Tuio20ComponentBehaviour> _tuioBehaviours = new();

        private Tuio20Dispatcher Dispatcher => (Tuio20Dispatcher)_tuioSessionBehaviour.TuioDispatcher;

        private void OnEnable()
        {
            try
            {
                Dispatcher.OnObjectAdd += SpawnTuioObject;
                Dispatcher.OnObjectRemove += DestroyTuioObject;
            }
            catch (InvalidCastException exception)
            {
                Debug.LogError($"[Tuio Client] Check the TUIO-Version on the TuioSession object. {exception.Message}");
            }
        }

        private void OnDisable()
        {
            try
            {
                Dispatcher.OnObjectAdd -= SpawnTuioObject;
                Dispatcher.OnObjectRemove -= DestroyTuioObject;
            }
            catch (InvalidCastException exception)
            {
                Debug.LogError($"[Tuio Client] Check the TUIO-Version on the TuioSession object. {exception.Message}");
            }
        }

        private void SpawnTuioObject(object sender, Tuio20Object tuioObject)
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
                var symbolBehaviour = Instantiate(_scapeXMobilePrefab, transform);
                symbolBehaviour.Initialize(tuioObject);
                _tuioBehaviours.Add(tuioObject.SessionId, symbolBehaviour);
                return;
            }
            
        }

        private void DestroyTuioObject(object sender, Tuio20Object tuioObject)
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
