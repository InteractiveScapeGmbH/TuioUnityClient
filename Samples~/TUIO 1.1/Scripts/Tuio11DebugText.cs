using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tuio11DebugText : MonoBehaviour
{
    [SerializeField] private Tuio11ObjectBehaviour _tuioBehaviour;

    [SerializeField] private TMP_Text _id;
    [SerializeField] private TMP_Text _angle;
    [SerializeField] private TMP_Text _position;
    [SerializeField] private TMP_Text _time;

    private void Start()
    {
        var color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _tuioBehaviour.SetColor(color);
        SetTextColor(color);
    }
    
    private void SetTextColor(Color color)
    {
        _id.color = color;
        _angle.color = color;
        _position.color = color;
        _time.color = color;
    }

    private void LateUpdate()
    {  
        SetAngle(_tuioBehaviour.Angle);
        SetPosition(_tuioBehaviour.ScreenPosition);
        SetId(_tuioBehaviour.Id);
        SetTime(_tuioBehaviour.MilliSeconds);
    }

    private void SetId(uint id)
    {
        _id.text = $"ID: {id}";
    }

    private void SetAngle(float angle)
    {
        _angle.text = $"{angle:f2} \u00ba";
    }

    private void SetPosition(Vector2 positon)
    {
        _position.text = $"x: {positon.x:f2}, \ny: {positon.y:f2}";
    }

    private void SetTime(long milliseconds)
    {
        var seconds = milliseconds * 0.001f;
        _time.text = $"{milliseconds:n0} ms";
    }
    
    
}
