using TuioNet.Common;
using TuioNet.Tuio11;
using UnityEngine;
using UnityEngine.UI;

public class Tuio11ObjectBehaviour : MonoBehaviour
{
    private Transform _transform;
    protected Tuio11Object _tuio11Object;
    
    [SerializeField] private Image _image;

    private Vector2 _screenDimensions = new Vector2(Screen.width, Screen.height);
    private RectTransform _rectTransform;
    private RectTransform _imageRectTransform;

    public uint Id { get; private set; }
    public Vector2 ScreenPosition { get; private set; }
    public float Angle { get; private set; }
    public long MilliSeconds { get; private set; }
    
    public void Initialize(Tuio11Object tuio11Object)
    {
        _tuio11Object = tuio11Object;
        Id = _tuio11Object.SymbolId;
    }
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _imageRectTransform = _image.GetComponent<RectTransform>();
        Random.InitState((int)_tuio11Object.SessionId);
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    void LateUpdate()
    {
        if (_tuio11Object.State == TuioState.Removed)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 halfNormalizedPosition = new Vector2(_tuio11Object.xPos - 0.5f, -_tuio11Object.yPos + 0.5f);
            ScreenPosition = new Vector2(halfNormalizedPosition.x * _screenDimensions.x,
                halfNormalizedPosition.y * _screenDimensions.y);
            
            _rectTransform.anchoredPosition = ScreenPosition;
            Angle = -Mathf.Rad2Deg * _tuio11Object.Angle;
            _imageRectTransform.rotation = Quaternion.Euler(0, 0, Angle);
            MilliSeconds = _tuio11Object.CurrentTime.GetTotalMilliseconds();
        }
    }
}