using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLinearMotion : ObstacleMotion
{
    [SerializeField] private MovementAxis _movementAxis;
    [SerializeField] private float _radius;
    [SerializeField] private float _durationOfOneTurn;
    [SerializeField] private bool _reverseMode;

    private Vector3 _initialPoint;
    private Vector3 _minPoint;
    private Vector3 _maxPoint;

    private void Start()
    {
        CalculatePoints();
        
        StartCoroutine(Move());
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
        _initialPoint = Position;
        
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

    protected override IEnumerator Move()
    {
        while (!ShouldMove)
        {
            yield return null;
        }

        var durationOfOneTurnInverse = 1f / _durationOfOneTurn;
        var direction = !_reverseMode;

        while (ShouldMove)
        {
            var step = 0f;
            var position = Position;

            while (step < 1f)
            {
                if (!ShouldMove)
                {
                    break;
                }

                step += durationOfOneTurnInverse * Time.deltaTime;

                if (direction)
                {
                    Position = Vector3.Lerp(position, _maxPoint, step);
                }
                
                else
                {
                    Position = Vector3.Lerp(position, _minPoint, step);
                }

                yield return null;
            }

            direction = !direction;
        }
    }
}
