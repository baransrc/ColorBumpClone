using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDetector : MonoBehaviour, IMovementDetector
{
    private Vector3 _movementDelta;
    private Vector3 _firstTouchPosition;
    private Vector3 _lastTouchPosition;
    private bool _registeredTouch;
    private Camera _mainCamera;

    private void Awake()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _firstTouchPosition = Vector2.zero;
        _lastTouchPosition = _firstTouchPosition;
        _mainCamera = Camera.main;
    }

    public Vector3 GetMovementDelta()
    {
        return _movementDelta;
    }

    private void HandleTouch()
    {
        var plane = new Plane(Vector3.up, 0);
        var distance = 0f;
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) // If user starts registering touch.
        {
            if (plane.Raycast(ray, out distance))
            {
                _firstTouchPosition = ray.GetPoint(distance);
            }

            _registeredTouch = true;
        }

        if (Input.GetMouseButton(0)) // If user keeps registering touch.
        {
            if (plane.Raycast(ray, out distance))
            {
                _lastTouchPosition = ray.GetPoint(distance);
            }
        }

        if (Input.GetMouseButtonUp(0)) // If user stops registering touch.
        {
            _firstTouchPosition = Vector2.zero;
            _lastTouchPosition = Vector2.zero;
            
            _registeredTouch = false;
        }

        _movementDelta = _lastTouchPosition - _firstTouchPosition;
    }

    public bool IsTouchRegistered()
    {
        return _registeredTouch;
    }

    private void Update()
    {
        HandleTouch();
    }
}
