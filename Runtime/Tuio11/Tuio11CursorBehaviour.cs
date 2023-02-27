using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11CursorBehaviour : MonoBehaviour
    {
        public Tuio11Cursor TuioCursor { get; private set; }
        
        private Transform _transform;
        private Vector2 _tuioPosition = Vector2.zero;
        
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
            _tuioPosition.x = TuioCursor.xPos;
            _tuioPosition.y = TuioCursor.yPos;
            
            _transform.position = Tuio11Manager.Instance.GetWorldPosition(_tuioPosition);
        }
        
        private void RemoveCursor()
        {
            Destroy(gameObject);
        }
    }
}
