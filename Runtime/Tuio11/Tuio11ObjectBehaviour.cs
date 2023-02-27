using System;
using TuioNet.Tuio11;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11ObjectBehaviour : TuioBehaviour
    {
        public Tuio11Object TuioObject { get; private set; }

        private Transform _transform;
        private Vector2 _tuioPosition = Vector2.zero;
        private float _angle;
        public override uint SessionId { get; protected set; }
        public override uint Id { get; protected set; }
        public override event Action OnUpdate;


        public void Initialize(Tuio11Object tuio11Object)
        {
            _transform = transform;
            TuioObject = tuio11Object;
            SessionId = TuioObject.SessionId;
            Id = TuioObject.SymbolId;
            TuioObject.OnUpdate += UpdateObject;
            TuioObject.OnRemove += RemoveObject;
            UpdateObject();
        }

        private void OnDestroy()
        {
            TuioObject.OnUpdate -= UpdateObject;
            TuioObject.OnRemove -= RemoveObject;
        }
        
        private void UpdateObject()
        {
            _tuioPosition.x = TuioObject.xPos;
            _tuioPosition.y = TuioObject.yPos;
            _angle = -Mathf.Rad2Deg * TuioObject.Angle;

            _transform.position = Tuio11Manager.Instance.GetScreenPosition(_tuioPosition);
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
            OnUpdate?.Invoke();
        }

        private void RemoveObject()
        {
            Destroy(gameObject);
        }

    }
}