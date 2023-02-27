using System;
using TuioNet.Tuio11;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11CursorBehaviour : TuioBehaviour
    {
        public Tuio11Cursor TuioCursor { get; private set; }
        
        private Transform _transform;
        private Vector2 _tuioPosition = Vector2.zero;
        public override uint SessionId { get; protected set; }
        public override uint Id { get; protected set; }
        public override event Action OnUpdate;
        
        public void Initialize(Tuio11Cursor cursor)
        {
            _transform = transform;
            TuioCursor = cursor;
            SessionId = TuioCursor.SessionId;
            Id = TuioCursor.CursorId;
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
            _tuioPosition.x = TuioCursor.Position.X;
            _tuioPosition.y = TuioCursor.Position.Y;
            
            _transform.position = Tuio11Manager.Instance.GetScreenPosition(_tuioPosition);
            OnUpdate?.Invoke();
        }
        
        private void RemoveCursor()
        {
            Destroy(gameObject);
        }

       

        
    }
}
