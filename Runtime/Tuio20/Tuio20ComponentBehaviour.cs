using TuioNet.Tuio20;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    public abstract class Tuio20ComponentBehaviour : TuioBehaviour
    {
        public Tuio20Component Tuio20Component { get; protected set; }
        
        private Transform _transform;
        private Vector2 _tuioPosition = Vector2.zero;
        private float _angle;

        public virtual void Initialize(Tuio20Component component)
        {
            _transform = transform;
            Tuio20Component = component;
            Tuio20Component.OnUpdate += UpdateComponent;
            Tuio20Component.OnRemove += RemoveComponent;
            UpdateComponent();
        }

        private void OnDestroy()
        {
            Tuio20Component.OnUpdate -= UpdateComponent;
            Tuio20Component.OnRemove -= RemoveComponent;
        }

        protected virtual void UpdateComponent()
        {
            _tuioPosition.x = Tuio20Component.Position.X;
            _tuioPosition.y = Tuio20Component.Position.Y;
            _angle = -Mathf.Rad2Deg * Tuio20Component.Angle;

            _transform.position = Tuio20Manager.Instance.GetScreenPosition(_tuioPosition);
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void RemoveComponent()
        {
            Destroy(gameObject);
        }
    }
}
