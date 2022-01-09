using Tuio.Common;
using Tuio.Tuio11;
using UnityEngine;

public class Tuio11Visualizer: MonoBehaviour, Tuio11Listener
{
    [SerializeField] private GameObject tuio11CursorPrefab;
    [SerializeField] private GameObject tuio11ObjectPrefab;

    void Start()
    {
        Tuio11Manager.Instance.AddTuio11Listener(this);
    }

    public void AddTuioObject(Tuio11Object tuio11Object)
    {
        var tuio11ObjectGameObject = Instantiate(tuio11ObjectPrefab, transform);
        var tuio11ObjectBehaviour = tuio11ObjectGameObject.GetComponent<Tuio11ObjectBehaviour>();
        tuio11ObjectBehaviour.Initialize(tuio11Object);
    }

    public void UpdateTuioObject(Tuio11Object tuio11Object)
    {
    }

    public void RemoveTuioObject(Tuio11Object tuio11Object)
    {
    }

    public void AddTuioCursor(Tuio11Cursor tuio11Cursor)
    {
        var tuio11CursorGameObject = Instantiate(tuio11CursorPrefab, transform);
        var tuio11CursorBehaviour = tuio11CursorGameObject.GetComponent<Tuio11CursorBehaviour>();
        tuio11CursorBehaviour.Initialize(tuio11Cursor);
    }

    public void UpdateTuioCursor(Tuio11Cursor tuio11Cursor)
    {
    }

    public void RemoveTuioCursor(Tuio11Cursor tuio11Cursor)
    {
    }

    public void AddTuioBlob(Tuio11Blob tuio11Blob)
    {
    }

    public void UpdateTuioBlob(Tuio11Blob tuio11Blob)
    {
    }

    public void RemoveTuioBlob(Tuio11Blob tuio11Blob)
    {
    }

    public void Refresh(TuioTime tuioTime)
    {
    }
}