using Tuio.Common;
using Tuio.Tuio11;
using UnityEngine;

public class Tuio11ObjectBehaviour : MonoBehaviour
{
    private Transform _transform;
    protected Tuio11Object _tuio11Object;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public void Initialize(Tuio11Object tuio11Object)
    {
        _tuio11Object = tuio11Object;
    }
    
    private void Start()
    {
        _transform = transform;
        Random.InitState((int)_tuio11Object.sessionId);
        _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void LateUpdate()
    {
        if (_tuio11Object.state == TuioState.Removed)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 dimensions = Tuio11Manager.Instance.GetDimensions();
            _transform.position = new Vector3(dimensions.x * _tuio11Object.xPos, dimensions.y * (1-_tuio11Object.yPos), 0);
            _transform.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * _tuio11Object.angle);
        }
    }
}