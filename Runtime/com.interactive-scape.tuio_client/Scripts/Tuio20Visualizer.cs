using Tuio.Common;
using Tuio.Tuio20;
using UnityEngine;

public class Tuio20Visualizer: MonoBehaviour, Tuio20Listener
{
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private GameObject pointerPrefab;

    void Start()
    {
        Tuio20Manager.Instance.AddTuio20Listener(this);
    }

    public void TuioAdd(Tuio20Object tuio20Object)
    {
        if (tuio20Object.ContainsNewTuioToken())
        {
            GameObject tokenGameObject = Instantiate(tokenPrefab, transform);
            TokenBehaviour tokenBehaviour = tokenGameObject.GetComponent<TokenBehaviour>();
            tokenBehaviour.Initialize(tuio20Object.token);
        }

        if (tuio20Object.ContainsTuioPointer())
        {
            GameObject pointerGameObject = Instantiate(pointerPrefab, transform);
            PointerBehaviour pointerBehaviour = pointerGameObject.GetComponent<PointerBehaviour>();
            pointerBehaviour.Initialize(tuio20Object.pointer);
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
