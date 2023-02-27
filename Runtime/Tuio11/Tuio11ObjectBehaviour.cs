using TuioNet.Tuio11;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    public class Tuio11ObjectBehaviour : MonoBehaviour
    {
        public Tuio11Object TuioObject { get; private set; }

        private Transform _transform;
        private Vector3 _position = Vector3.zero;
        private float _angle;

        public void Initialize(Tuio11Object tuio11Object)
        {
            _transform = transform;
            TuioObject = tuio11Object;
            TuioObject.OnUpdate += UpdateObject;
            TuioObject.OnRemove += RemoveObject;
        }

        private void OnDestroy()
        {
            TuioObject.OnUpdate -= UpdateObject;
            TuioObject.OnRemove -= RemoveObject;
        }
        
        private void UpdateObject()
        {
            Vector2 dimensions = Tuio11Manager.Instance.GetDimensions();
            _position.x = dimensions.x * TuioObject.xPos;
            _position.y = dimensions.y * (1 - TuioObject.yPos);
            _angle = -Mathf.Rad2Deg * TuioObject.Angle;
            _transform.position = _position;
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void RemoveObject()
        {
            Destroy(gameObject);
        }
    }
}