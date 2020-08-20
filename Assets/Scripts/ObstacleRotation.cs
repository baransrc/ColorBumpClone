using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : ObstacleMotion
{
    [SerializeField] private bool _rotateOnX = false;
    [SerializeField] private bool _rotateOnY = false;
    [SerializeField] private bool _rotateOnZ = false;
    [SerializeField] private float _timeToRotate360 = 1f;
    [SerializeField] private bool _isReverse = false;


    private void Start()
    {
        StartCoroutine(Move());
    }

    protected override IEnumerator Move()
    {
        while (!ShouldMove)
        {
            yield return null;
        }

        while (ShouldMove)
        {
            var step = 0f;

            while (step <= 1f)
            {
                if (!ShouldMove)
                {
                    break;
                }

                step += Time.deltaTime / _timeToRotate360;
                var rotation = new Vector3(_rotateOnX ? 180f : 0f, _rotateOnY ? 180f : 0f, _rotateOnZ ? 180f : 0f) * step;
                RotationEuler = !_isReverse ? rotation : -rotation;

                yield return null;
            }
        }
    }

}
