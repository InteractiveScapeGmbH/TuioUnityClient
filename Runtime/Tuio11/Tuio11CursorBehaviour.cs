using TuioNet.Common;
using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11CursorBehaviour : MonoBehaviour
    {
        private Transform _transform;
        private Tuio11Cursor _cursor;
        private Vector3 _position = Vector3.zero;
        
        public void Initialize(Tuio11Cursor cursor)
        {
            _cursor = cursor;
        }
        
        private void Start()
        {
            _transform = transform;
        }

        public void UpdatePosition()
        {
            
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
                _position.x = dimensions.x * _cursor.xPos;
                _position.y = dimensions.y * (1 - _cursor.yPos);
                _transform.position = _position;
            }
        }
    }
}
