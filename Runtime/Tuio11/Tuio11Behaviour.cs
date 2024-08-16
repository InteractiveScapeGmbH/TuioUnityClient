using System;
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
        private Func<Vector2, Vector2> _getPosition;
        
        public virtual void Initialize(Tuio11Container container, RenderMode renderMode)
        {
            _container = container;
            _getPosition = renderMode switch
            {
                RenderMode.WorldSpace => TuioTransform.GetWorldPosition,
                RenderMode.ScreenSpaceCamera => TuioTransform.GetWorldPosition,
                RenderMode.ScreenSpaceOverlay => TuioTransform.GetScreenPosition,
            };
            UpdateContainer();
        }

        protected virtual void Update()
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

            RectTransform.position = _getPosition.Invoke(_position);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}