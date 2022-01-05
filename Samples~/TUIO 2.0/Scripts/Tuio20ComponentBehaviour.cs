using Tuio.Common;
using Tuio.Tuio20;
using UnityEngine;

public abstract class Tuio20ComponentBehaviour : MonoBehaviour
{
    private Transform _transform;
    protected Tuio20Component _component;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _transform = transform;
        Random.InitState((int)_component.sessionId);
        _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void LateUpdate()
    {
        if (_component.state == TuioState.Removed)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 dimensions = Tuio20Manager.Instance.GetDimensions();
            _transform.position = new Vector3(dimensions.x * _component.xPos, dimensions.y * (1-_component.yPos), 0);
            _transform.eulerAngles = new Vector3(0, 0, _component.angle);
        }
    }
}
