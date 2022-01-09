using Tuio.Common;
using Tuio.Tuio20;
using UnityEngine;

public abstract class Tuio20ComponentBehaviour : MonoBehaviour
{
    private Transform _transform;
    protected Tuio20Component _tuio20Component;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _transform = transform;
        Random.InitState((int)_tuio20Component.sessionId);
        _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void LateUpdate()
    {
        if (_tuio20Component.state == TuioState.Removed)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 dimensions = Tuio20Manager.Instance.GetDimensions();
            _transform.position = new Vector3(dimensions.x * _tuio20Component.xPos, dimensions.y * (1-_tuio20Component.yPos), 0);
            _transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * _tuio20Component.angle);
        }
    }
}
