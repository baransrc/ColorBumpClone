﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : ObstacleMotion
{
    [SerializeField] private bool _rotateOnX;
    [SerializeField] private bool _rotateOnY;
    [SerializeField] private bool _rotateOnZ;
    [SerializeField] private float _timeToRotate360;


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
                RotationEuler = rotation;

                yield return null;
            }
        }
    }

}
