using TuioNet.Tuio11;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Tuio11
{
    /// <summary>
    /// Base class for all Tuio 1.1 MonoBehaviours. Tuio objects update their transforms themselves and provide functionality
    /// to destroy them when the object or touch gets removed from the screen.
    /// </summary>
    public abstract class Tuio11Behaviour : TuioBehaviour
    {
        private Vector2 _position = Vector2.zero;
        private Tuio11Container _container;
        
        public virtual void Initialize(Tuio11Container container)
        {
            _container = container;
            UpdateContainer();
        }

        private void Update()
        {
            UpdateContainer();
        }

        protected virtual void UpdateContainer()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _position.x = _container.Position.X;
            _position.y = _container.Position.Y;

            RectTransform.position = TuioTransform.GetScreenPosition(_position);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}