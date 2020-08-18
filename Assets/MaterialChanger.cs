using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _materialToChangeFrom;
    [SerializeField] private Material _materialToChangeTo;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void ChangeMaterial(Collider other)
    {
        var otherRenderer = other.gameObject.GetComponent<Renderer>();

        if (otherRenderer.sharedMaterial == _materialToChangeFrom)
        {
            otherRenderer.sharedMaterial = _materialToChangeTo;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }

        ChangeMaterial(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            return;
        }

        ChangeMaterial(other);
    }
} 
