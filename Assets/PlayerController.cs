using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(IMovementDetector))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private IMovementDetector _touchMovementDetector;
    private TrailRenderer _trailRenderer;
    private Collider _collider;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Vector3 _startPos;
    private bool _shouldMove;
    private Camera _mainCamera;

    private Material _materialBeforeShrink;
    private Coroutine _shrinkGrowCoroutine;

    [SerializeField] private float _xLowerBound;
    [SerializeField] private float _xUpperBound;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private LevelColors _levelColors;

    private void Awake()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _shouldMove = true;
        _mainCamera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _touchMovementDetector = GetComponent<IMovementDetector>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherRenderer = collision.gameObject.GetComponent<Renderer>();

        if (otherRenderer.sharedMaterial == _levelColors.GroundMaterial)
        {
            return;
        }

        if (otherRenderer.sharedMaterial != _renderer.sharedMaterial)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Move()
    {
        if (!_shouldMove)
        {
            return;
        }

        if (!_touchMovementDetector.IsTouchRegistered())
        {
            _startPos = _rigidbody.position;
            return;
        }

        var movementDelta = _touchMovementDetector.GetMovementDelta();
        var newPosition = new Vector3(_startPos.x + movementDelta.x,
                                      _rigidbody.position.y,
                                      _startPos.z + movementDelta.z);

        _rigidbody.position = newPosition;

        var currentPosition = _rigidbody.position;

        if (currentPosition.x > _xUpperBound)
        {
            _rigidbody.MovePosition(new Vector3(_xUpperBound, currentPosition.y, currentPosition.z));
        }

        else if (currentPosition.x < _xLowerBound)
        {
            _rigidbody.MovePosition(new Vector3(_xLowerBound, currentPosition.y, currentPosition.z));
        }
    }

    public void Shrink(float duration)
    {
        _materialBeforeShrink = _renderer.sharedMaterial;
        _renderer.sharedMaterial = _levelColors.TerinaryMaterial;

        if (_shrinkGrowCoroutine != null)
        {
            StopCoroutine(_shrinkGrowCoroutine);
        }

        _shrinkGrowCoroutine = StartCoroutine(ScalePlayerCoroutine(0.5f, duration));
    }

    public void Grow(float duration)
    {
        _renderer.sharedMaterial = _materialBeforeShrink;

        if (_shrinkGrowCoroutine != null)
        {
            StopCoroutine(_shrinkGrowCoroutine);
        }

        _shrinkGrowCoroutine = StartCoroutine(ScalePlayerCoroutine(1f, duration));
    }

    private IEnumerator ScalePlayerCoroutine(float scale, float duration)
    {
        var scaleTarget = Vector3.one * scale;
        var step = 0f;
        var durationInverse = 1f / duration;

        while (step < 1f)
        {
            step += durationInverse * Time.deltaTime;

            transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, durationInverse);

            yield return null;
        }

        transform.localScale = scaleTarget;
    }

    private void ClampPlayerWrtCamera()
    {
        var position = _rigidbody.position;
        var mainCameraPosition = _mainCamera.transform.position;

        if (position.z - mainCameraPosition.z < 5f)
        {
            _rigidbody.MovePosition(new Vector3(position.x, position.y, mainCameraPosition.z + 5f));
        }
    }

    private void ApplyVelocity()
    {
        _rigidbody.velocity = _shouldMove ? _velocity : Vector3.zero;
    }

    private void FixedUpdate()   
    {
        Move();
        ClampPlayerWrtCamera();
        ApplyVelocity();
    }
}
