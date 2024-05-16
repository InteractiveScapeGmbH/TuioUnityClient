using System.Collections.Generic;
using TuioNet.Tuio20;
using TuioUnity.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace TuioUnity.Tuio20
{
    public class Tuio20Visualizer: MonoBehaviour
    {
        [SerializeField] private TuioSession _tuioSession;
        [SerializeField] private Tuio20TokenBehaviour _tuio20TokenPrefab;
        [SerializeField] private Tuio20PointerBehaviour _tuio20PointerPrefab;
        [SerializeField] private PhoneBehaviour _phonePrefab;

        private readonly Dictionary<uint, Tuio20ComponentBehaviour> _tuioBehaviours = new();

        private Tuio20Dispatcher Manager => (Tuio20Dispatcher)_tuioSession.TuioDispatcher;

        private void OnEnable()
        {
            Manager.OnObjectAdd += SpawnTuioObject;
            Manager.OnObjectRemove += DestroyTuioObject;
        }

        private void OnDisable()
        {
            Manager.OnObjectAdd -= SpawnTuioObject;
            Manager.OnObjectRemove -= DestroyTuioObject;
        }

        private void SpawnTuioObject(Tuio20Object tuioObject)
        {
            if (tuioObject.ContainsNewTuioPointer())
            {
                var pointerBehaviour = Instantiate(_tuio20PointerPrefab, transform);
                pointerBehaviour.Initialize(tuioObject);
                _tuioBehaviours.Add(tuioObject.SessionId, pointerBehaviour);
                return;
            }
            
            if (tuioObject.ContainsNewTuioToken())
            {
                var tokenBehaviour = Instantiate(_tuio20TokenPrefab, transform);
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
