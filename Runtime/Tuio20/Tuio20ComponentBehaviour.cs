using TuioNet.Tuio20;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    /// <summary>
    /// Base class for all Tuio 2.0 MonoBehaviours. Tuio objects update their transforms themselves and provide functionality
    /// to destroy them when the object or touch gets removed from the screen.
    /// </summary>
    public abstract class Tuio20ComponentBehaviour : TuioBehaviour
    {
        private Tuio20Component _transformComponent;
        
        private Vector2 _tuioPosition = Vector2.zero;
        private float _angle;

        public virtual void Initialize(Tuio20Object tuioObject)
        {
            _transformComponent = GetTransformComponent(tuioObject);
            UpdateComponent();
        }

        protected abstract Tuio20Component GetTransformComponent(Tuio20Object tuioObject);

        private void Update()
        {
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            _tuioPosition.x = _transformComponent.Position.X;
            _tuioPosition.y = _transformComponent.Position.Y;
            _angle = -Mathf.Rad2Deg * _transformComponent.Angle;

            RectTransform.position = TuioTransform.GetScreenPosition(_tuioPosition);
            RectTransform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
