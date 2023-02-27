using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11ObjectBehaviour : MonoBehaviour
    {
        public Tuio11Object TuioObject { get; private set; }

        private Transform _transform;
        private Vector2 _tuioPosition = Vector2.zero;
        private float _angle;

        public void Initialize(Tuio11Object tuio11Object)
        {
            _transform = transform;
            TuioObject = tuio11Object;
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

            _transform.position = Tuio11Manager.Instance.GetWorldPosition(_tuioPosition);
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void RemoveObject()
        {
            Destroy(gameObject);
        }
    }
}