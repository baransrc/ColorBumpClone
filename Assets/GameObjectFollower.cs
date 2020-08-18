using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothingAmount;
    [SerializeField] private bool _followX;
    [SerializeField] private bool _followY;
    [SerializeField] private bool _followZ;

    private Vector3 _offset;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
        _offset = _initialPosition - _target.position;
    }

    private void FixedUpdate()
    {
        var currentPos = transform.position;
        var targetX = (_followX ? _target.position.x + _offset.x : currentPos.x);
        var targetY = (_followY ? _target.position.y + _offset.y : currentPos.y);
        var targetZ = (_followZ ? _target.position.z + _offset.z : currentPos.z);

        var smoothedTargetPosition = Vector3.Lerp(currentPos, new Vector3(targetX, targetY, targetZ), _smoothingAmount * Time.deltaTime);
        transform.position = smoothedTargetPosition;
    }

}
