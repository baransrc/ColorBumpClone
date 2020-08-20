using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleCollision : MonoBehaviour
{
    private Collider _collider = null;

    public delegate void CollisionHandler();
    public event CollisionHandler OnCollisionOccured;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != Tag.Ground)
        {
            OnCollisionOccured?.Invoke();
        }
    }
}
