using TuioNet.Common;
using TuioNet.Tuio11;
using UnityEngine;

public class Tuio11CursorBehaviour : MonoBehaviour
{
    private Transform _transform;
    protected Tuio11Cursor _cursor;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public void Initialize(Tuio11Cursor cursor)
    {
        _cursor = cursor;
    }
    
    private void Start()
    {
        _transform = transform;
        Random.InitState((int)_cursor.SessionId);
        _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void LateUpdate()
    {
        if (_cursor.State == TuioState.Removed)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 dimensions = Tuio11Manager.Instance.GetDimensions();
            _transform.position = new Vector3(dimensions.x * _cursor.xPos, dimensions.y * (1-_cursor.yPos), 0);
        }
    }
}