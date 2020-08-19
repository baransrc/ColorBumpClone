using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleCollision))]
[RequireComponent(typeof(Rigidbody))]
public abstract class ObstacleMotion : MonoBehaviour
{
    protected Vector3 Position 
    { 
        get
        {
            return _rigidbody.position;
        }
        set
        {
            _rigidbody.MovePosition(value);
        }
    }

    protected Vector3 RotationEuler
    {
        get
        {
            return _rigidbody.rotation.eulerAngles;
        }
        set
        {
            _rigidbody.MoveRotation(Quaternion.Euler(value));
        }
    }

    public bool ShouldMove { get; set; }

    private Rigidbody _rigidbody;
    private ObstacleCollision _obstacleCollision;
    private Camera _camera;
    private Collider _collider;

    private bool _collisionOccured;
    

    private void Awake()
    {
        _obstacleCollision = GetComponent<ObstacleCollision>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = Camera.main;
        
        ShouldMove = false;
        _collisionOccured = false;

        _obstacleCollision.OnCollisionOccured += StopMotion;

        StartCoroutine(CheckMovementAvailability());
    }  
    
    private IEnumerator CheckMovementAvailability()
    {
        while (!_collisionOccured)
        {
            ShouldMove = IsVisible();

            ApplyRigidbodyConstraints(!ShouldMove, !ShouldMove);

            yield return null;
        }

        ShouldMove = false;
    }

    private bool IsVisible()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        
        if (GeometryUtility.TestPlanesAABB(planes, _collider.bounds))
        {
            return true;
        }

        return false;
    }

    private void ApplyRigidbodyConstraints(bool fixRotation, bool fixPosition)
    {
        var bitField = fixRotation ? RigidbodyConstraints.FreezePosition : 0;
        bitField = fixPosition ? bitField | RigidbodyConstraints.FreezeRotation : bitField;

        _rigidbody.constraints = bitField;
    }

    private void StopMotion()
    {
        ShouldMove = false;
        _collisionOccured = true;
    }

    abstract protected IEnumerator Move();

    private void OnDestroy()
    {
        _obstacleCollision.OnCollisionOccured -= StopMotion;
    }
}
