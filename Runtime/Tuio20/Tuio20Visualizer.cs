using System.Collections.Generic;
using TuioNet.Tuio20;
using TuioUnity.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace TuioUnity.Tuio20
{
    public class Tuio20Visualizer: MonoBehaviour
    {
        [SerializeField] private TuioManagerBehaviour _tuioManager;
        [SerializeField] private Tuio20TokenBehaviour _tuio20TokenPrefab;
        [SerializeField] private Tuio20PointerBehaviour _tuio20PointerPrefab;
        [FormerlySerializedAs("_tuio20SymbolPrefab")] [SerializeField] private PhoneBehaviour _phonePrefab;

        
        private readonly Dictionary<uint, Tuio20PointerBehaviour> _pointer = new();
        private readonly Dictionary<uint, Tuio20TokenBehaviour> _token = new();
        private readonly Dictionary<uint, PhoneBehaviour> _symbols = new();

        private Tuio20Manager Manager => (Tuio20Manager)_tuioManager.TuioManager;

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
                _pointer.Add(tuioObject.SessionId, pointerBehaviour);
                return;
            }
            
            if (tuioObject.ContainsNewTuioToken())
            {
                var tokenBehaviour = Instantiate(_tuio20TokenPrefab, transform);
                tokenBehaviour.Initialize(tuioObject);
                _token.Add(tuioObject.SessionId, tokenBehaviour);
                return;
            }
            
            if (tuioObject.ContainsNewTuioSymbol())
            {
                var symbolBehaviour = Instantiate(_phonePrefab, transform);
                symbolBehaviour.Initialize(tuioObject);
                _symbols.Add(tuioObject.SessionId, symbolBehaviour);
                return;
            }
            
        }

        private void DestroyTuioObject(Tuio20Object tuioObject)
        {
            if (_pointer.Remove(tuioObject.SessionId, out var pointerBehaviour))
            {
                pointerBehaviour.Destroy();
            }
            
            if (_token.Remove(tuioObject.SessionId, out var tokenBehaviour))
            {
                tokenBehaviour.Destroy();
            }
            
            if (_symbols.Remove(tuioObject.SessionId, out var symbolBehaviour))
            {
                symbolBehaviour.Destroy();
            }
        }
       
    }
}
