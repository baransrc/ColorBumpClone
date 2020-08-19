using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleCollision))]
[RequireComponent(typeof(Rigidbody))]
public class ObstacleLinearMotion : MonoBehaviour
{
    [SerializeField] private MovementAxis _movementAxis;
    [SerializeField] private float _radius;
    [SerializeField] private float _durationOfOneTurn;
    [SerializeField] private bool _reverseMode;

    private ObstacleCollision _obstacleCollision;
    private Rigidbody _rigidbody;
    private bool _shouldMove;

    private Vector3 _initialPoint;
    private Vector3 _minPoint;
    private Vector3 _maxPoint;

    private void Awake()
    {
        _obstacleCollision = GetComponent<ObstacleCollision>();
        _rigidbody = GetComponent<Rigidbody>();
        _shouldMove = true;

        _obstacleCollision.OnCollisionOccured += StopMotion;

        CalculatePoints();

        StartCoroutine(Movement());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        var wireSize = new Vector3(0.5f, 0.5f, 0.5f);

        switch (_movementAxis)
        {
            case MovementAxis.X:
                wireSize.x = _radius * 2f + transform.localScale.x;
                break;

            case MovementAxis.Y:
                wireSize.y = _radius * 2f + transform.localScale.y;
                break;

            case MovementAxis.Z:
                wireSize.z = _radius * 2f + transform.localScale.z;
                break;
        }

        Gizmos.DrawWireCube(transform.position, wireSize);
    }

    private void CalculatePoints()
    {
        _initialPoint = _rigidbody.position;
        
        switch (_movementAxis)
        {
            case MovementAxis.X:
                _minPoint = _initialPoint - Vector3.right * _radius;
                _maxPoint = _initialPoint + Vector3.right * _radius;
                break;

            case MovementAxis.Y:
                _minPoint = _initialPoint - Vector3.up * _radius;
                _maxPoint = _initialPoint + Vector3.up * _radius;
                break;

            case MovementAxis.Z:
                _minPoint = _initialPoint - Vector3.forward * _radius;
                _maxPoint = _initialPoint + Vector3.forward * _radius;
                break;
        }
    }

    private void StopMotion()
    {
        _shouldMove = false;
    }

    private IEnumerator Movement()
    {
        var durationOfOneTurnInverse = 1f / _durationOfOneTurn;
        var initialPosition = _rigidbody.position;
        var direction = !_reverseMode;

        while (_shouldMove)
        {
            var step = 0f;
            var position = _rigidbody.position;

            while (step < 1f)
            {
                if (!_shouldMove)
                {
                    break;
                }

                step += durationOfOneTurnInverse * Time.deltaTime;

                if (direction)
                {
                    _rigidbody.transform.position = Vector3.Lerp(position, _maxPoint, step);
                }
                
                else
                {
                    _rigidbody.transform.position = Vector3.Lerp(position, _minPoint, step);
                }

                yield return null;
            }

            direction = !direction;
        }
    }

    private void OnDestroy()
    {
        _obstacleCollision.OnCollisionOccured -= StopMotion;
    }
}
