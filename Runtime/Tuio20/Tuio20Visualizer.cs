using TuioNet.Common;
using TuioNet.Tuio20;
using UnityEngine;
using UnityEngine.Serialization;

namespace TuioUnity.Tuio20
{
    public class Tuio20Visualizer: MonoBehaviour, Tuio20Listener
    {
        [SerializeField] private Tuio20TokenBehaviour _tuio20TokenPrefab;
        [SerializeField] private Tuio20PointerBehaviour _tuio20PointerPrefab;

        void Start()
        {
            Tuio20Manager.Instance.AddTuio20Listener(this);
        }

        public void TuioAdd(Tuio20Object tuio20Object)
        {
            if (tuio20Object.ContainsNewTuioToken())
            {
                var tuio20TokenBehaviour = Instantiate(_tuio20TokenPrefab, transform);
                tuio20TokenBehaviour.Initialize(tuio20Object.Token);
            }

            if (tuio20Object.ContainsNewTuioPointer())
            {
                var tuio20PointerBehaviour = Instantiate(_tuio20PointerPrefab, transform);
                tuio20PointerBehaviour.Initialize(tuio20Object.Pointer);
            }
        }

        public void TuioUpdate(Tuio20Object tuio20Object)
        {
        }

        public void TuioRemove(Tuio20Object tuio20Object)
        {
        }

        public void TuioRefresh(TuioTime tuioTime)
        {
        }
    }
}
