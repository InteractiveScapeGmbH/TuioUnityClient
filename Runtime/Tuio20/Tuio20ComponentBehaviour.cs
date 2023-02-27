using System;
using TuioNet.Common;
using TuioNet.Tuio20;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace TuioUnity.Tuio20
{
    public abstract class Tuio20ComponentBehaviour : MonoBehaviour
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
        }

        private void OnDestroy()
        {
            Tuio20Component.OnUpdate -= UpdateComponent;
            Tuio20Component.OnRemove -= RemoveComponent;
        }

        private void UpdateComponent()
        {
            _tuioPosition.x = Tuio20Component.xPos;
            _tuioPosition.y = Tuio20Component.yPos;
            _angle = -Mathf.Rad2Deg * Tuio20Component.Angle;

            _transform.position = Tuio20Manager.Instance.GetWorldPosition(_tuioPosition);
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void RemoveComponent()
        {
            Destroy(gameObject);
        }
    }
}
