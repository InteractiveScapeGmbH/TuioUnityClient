using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11CursorBehaviour : MonoBehaviour
    {
        public Tuio11Cursor TuioCursor { get; private set; }
        
        private Transform _transform;
        private Vector3 _position = Vector3.zero;
        
        public void Initialize(Tuio11Cursor cursor)
        {
            _transform = transform;
            TuioCursor = cursor;
            TuioCursor.OnUpdate += UpdateCursor;
            TuioCursor.OnRemove += RemoveCursor;
            UpdateCursor();
        }

        private void OnDestroy()
        {
            TuioCursor.OnUpdate -= UpdateCursor;
            TuioCursor.OnRemove -= RemoveCursor;
        }

        private void UpdateCursor()
        {
            Vector2 dimensions = Tuio11Manager.Instance.GetDimensions();
            _position.x = dimensions.x * TuioCursor.xPos;
            _position.y = dimensions.y * (1 - TuioCursor.yPos);
            _transform.position = _position;
        }
        
        private void RemoveCursor()
        {
            Destroy(gameObject);
        }
    }
}
