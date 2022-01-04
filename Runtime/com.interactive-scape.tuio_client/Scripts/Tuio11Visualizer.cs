using Tuio.Common;
using Tuio.Tuio11;
using UnityEngine;

public class Tuio11Visualizer: MonoBehaviour, Tuio11Listener
{
    [SerializeField] private GameObject cursorPrefab;

    void Start()
    {
        Tuio11Manager.Instance.AddTuio11Listener(this);
    }

    public void AddTuioObject(Tuio11Object tuio11Object)
    {
    }

    public void UpdateTuioObject(Tuio11Object tuio11Object)
    {
    }

    public void RemoveTuioObject(Tuio11Object tuio11Object)
    {
    }

    public void AddTuioCursor(Tuio11Cursor tuio11Cursor)
    {
        GameObject cursorGameObject = Instantiate(cursorPrefab, transform);
        CursorBehaviour cursorBehaviour = cursorGameObject.GetComponent<CursorBehaviour>();
        cursorBehaviour.Initialize(tuio11Cursor);
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