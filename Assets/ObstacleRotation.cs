using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ObstacleCollision))]
public class ObstacleRotation : MonoBehaviour
{
    [SerializeField] private bool _rotateOnX;
    [SerializeField] private bool _rotateOnY;
    [SerializeField] private bool _rotateOnZ;
    [SerializeField] private float _timeToRotate360;

    private Rigidbody _rigidbody;
    private ObstacleCollision _obstacleCollision;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _obstacleCollision = GetComponent<ObstacleCollision>();

        _obstacleCollision.OnCollisionOccured += StopRotation;

        StartCoroutine(Rotate());
    }

    public void StopRotation()
    {
        _rotateOnX = false;
        _rotateOnY = false;
        _rotateOnZ = false;
    }

    private IEnumerator Rotate()
    {
        while (_rotateOnX || _rotateOnY || _rotateOnZ)
        {
            var step = 0f;
            var rotation = _rigidbody.rotation.eulerAngles;

            while (step <= 1f)
            {
                step += Time.deltaTime / _timeToRotate360;
                rotation = new Vector3(_rotateOnX ? 180f : 0f, _rotateOnY ? 180f : 0f, _rotateOnZ ? 180f : 0f) * step;
                _rigidbody.MoveRotation(Quaternion.Euler(rotation));

                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        _obstacleCollision.OnCollisionOccured -= StopRotation;
    }
}
