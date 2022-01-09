using Tuio.Common;
using Tuio.Tuio20;
using UnityEngine;

public class Tuio20Visualizer: MonoBehaviour, Tuio20Listener
{
    [SerializeField] private GameObject tuio20TokenPrefab;
    [SerializeField] private GameObject tuio20PointerPrefab;

    void Start()
    {
        Tuio20Manager.Instance.AddTuio20Listener(this);
    }

    public void TuioAdd(Tuio20Object tuio20Object)
    {
        if (tuio20Object.ContainsNewTuioToken())
        {
            var tuio20TokenGameObject = Instantiate(tuio20TokenPrefab, transform);
            var tuio20TokenBehaviour = tuio20TokenGameObject.GetComponent<Tuio20TokenBehaviour>();
            tuio20TokenBehaviour.Initialize(tuio20Object.token);
        }

        if (tuio20Object.ContainsNewTuioPointer())
        {
            var tuio20PointerGameObject = Instantiate(tuio20PointerPrefab, transform);
            var tuio20PointerBehaviour = tuio20PointerGameObject.GetComponent<Tuio20PointerBehaviour>();
            tuio20PointerBehaviour.Initialize(tuio20Object.pointer);
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
