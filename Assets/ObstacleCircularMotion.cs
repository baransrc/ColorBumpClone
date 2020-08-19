using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CircularMovementAxis
{
    X,
    Y,
    Z
}

[RequireComponent(typeof(ObstacleCollision))]
[RequireComponent(typeof(Rigidbody))]
public class ObstacleCircularMotion : MonoBehaviour
{
    [SerializeField] private CircularMovementAxis _movementAxis;
    [SerializeField] private float _radiusWidth;
    [SerializeField] private float _radiusHeight;
    [SerializeField] private float _durationOfOneTurn;
    [SerializeField] private bool _reverseMode;

    private ObstacleCollision _obstacleCollision;
    private Rigidbody _rigidbody;
    private static float _twoTimesPi = Mathf.PI * 2;
    private bool _shouldMove;

    private void Awake()
    {
        _obstacleCollision = GetComponent<ObstacleCollision>();
        _rigidbody = GetComponent<Rigidbody>();
        _shouldMove = true;

        _obstacleCollision.OnCollisionOccured += StopMotion;

        StartCoroutine(CircularMovement());
    }

    private void StopMotion()
    {
        _shouldMove = false;
    }

    private IEnumerator CircularMovement()
    {
        var durationOfOneTurnInverse = 1f / _durationOfOneTurn;
        var initialPosition = _rigidbody.position;

        while (_shouldMove)
        {
            var step = 0f;
            var angle = 0f;

            while (step < 1f)
            {
                if (!_shouldMove)
                {
                    break;
                }

                step += Time.deltaTime * durationOfOneTurnInverse;
                angle = step * _twoTimesPi;
                angle = _reverseMode ? -angle : angle;

                var x = 0f;
                var y = 0f;
                var z = 0f;

                switch (_movementAxis)
                {
                    case CircularMovementAxis.X:
                        z += Mathf.Cos(angle) * _radiusWidth;
                        y += Mathf.Sin(angle) * _radiusHeight;
                        break;
                    case CircularMovementAxis.Y:
                        x += Mathf.Cos(angle) * _radiusWidth;
                        z += Mathf.Sin(angle) * _radiusHeight;
                        break;
                    case CircularMovementAxis.Z:
                        x += Mathf.Cos(angle) * _radiusWidth;
                        y += Mathf.Sin(angle) * _radiusHeight;
                        break;
                }

                _rigidbody.position = initialPosition + new Vector3(x, y, z);

                yield return null;
            }

        }
    }

    private void OnDestroy()
    {
        _obstacleCollision.OnCollisionOccured -= StopMotion;
    }
}
