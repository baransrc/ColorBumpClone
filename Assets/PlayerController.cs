using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(IMovementDetector))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private IMovementDetector _touchMovementDetector;
    private SphereCollider _collider;
    private Rigidbody _rigidbody;
    private Vector3 _startPos;

    [SerializeField] float _xLowerBound;
    [SerializeField] float _xUpperBound;
    [SerializeField] Vector3 _velocity;

    private void Awake()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _touchMovementDetector = GetComponent<IMovementDetector>();
    }

    private void Move()
    {
        if (!_touchMovementDetector.IsTouchRegistered())
        {
            _startPos = transform.position;
            return;
        }

        var movementDelta = _touchMovementDetector.GetMovementDelta();
        var newPosition = new Vector3(_startPos.x + movementDelta.x,
                                      transform.position.y,
                                      _startPos.z + movementDelta.z);

        transform.position = newPosition;

        var currentPosition = transform.position;

        if (currentPosition.x > _xUpperBound)
        {
            transform.position = new Vector3(_xUpperBound, currentPosition.y, currentPosition.z);
        }

        else if (currentPosition.x < _xLowerBound)
        {
            transform.position = new Vector3(_xLowerBound, currentPosition.y, currentPosition.z);
        }
    }

    private void ApplyVelocity()
    {
        _rigidbody.velocity = _velocity;
    }

    private void Update()   
    {
        Move();
        ApplyVelocity();
    }
}
