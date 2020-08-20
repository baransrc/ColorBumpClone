using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCircularMotion : ObstacleMotion
{
    [SerializeField] private MovementAxis _movementAxis = MovementAxis.X;
    [SerializeField] private float _radiusWidth = 1f;
    [SerializeField] private float _radiusHeight = 1f;
    [SerializeField] private float _durationOfOneTurn = 1f;
    [SerializeField] private bool _reverseMode = false;

    private static float _twoTimesPi = Mathf.PI * 2;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, Mathf.Max(_radiusHeight, _radiusHeight));
    }

    protected override IEnumerator Move()
    {
        while (!ShouldMove)
        {
            yield return null;
        }

        var durationOfOneTurnInverse = 1f / _durationOfOneTurn;
        var initialPosition = Position;

        while (ShouldMove)
        {
            var step = 0f;
            

            while (step < 1f)
            {
                if (!ShouldMove)
                {
                    break;
                }

                step += Time.deltaTime * durationOfOneTurnInverse;
                
                var angle = step * _twoTimesPi;
                angle = _reverseMode ? -angle : angle;

                var x = 0f;
                var y = 0f;
                var z = 0f;

                switch (_movementAxis)
                {
                    case MovementAxis.X:
                        z += Mathf.Cos(angle) * _radiusWidth;
                        y += Mathf.Sin(angle) * _radiusHeight;
                        break;
                    case MovementAxis.Y:
                        x += Mathf.Cos(angle) * _radiusWidth;
                        z += Mathf.Sin(angle) * _radiusHeight;
                        break;
                    case MovementAxis.Z:
                        x += Mathf.Cos(angle) * _radiusWidth;
                        y += Mathf.Sin(angle) * _radiusHeight;
                        break;
                }

                Position = initialPosition + new Vector3(x, y, z);

                yield return null;
            }

        }
    }
}
