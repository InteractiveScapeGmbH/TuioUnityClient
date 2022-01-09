using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuio11CameraBehaviour : MonoBehaviour
{
    private Transform _transform;
    private Camera _camera;
    
    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector2 dimensions = Tuio11Manager.Instance.GetDimensions();
        if (dimensions != Vector2.zero)
        {
            _transform.position = new Vector3(0.5f * dimensions.x, 0.5f * dimensions.y, -10);
            float tuioAspect = dimensions.x / dimensions.y;
            float cameraAspect = _camera.aspect;
            _camera.orthographicSize = tuioAspect > cameraAspect ? 0.5f * dimensions.y : 0.5f * dimensions.x / cameraAspect;
        }
    }
}
