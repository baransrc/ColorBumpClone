using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _materialToChangeFrom = null;
    [SerializeField] private Material _materialToChangeTo = null;

    [SerializeField] Renderer _materialToChangeFromIndicator = null;
    [SerializeField] Renderer _materialToChangeToIndicator = null;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _materialToChangeFromIndicator.sharedMaterial = _materialToChangeFrom;
        _materialToChangeToIndicator.sharedMaterial = _materialToChangeTo;
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
        if (other.tag == Tag.Player)
        {
            return;
        }

        ChangeMaterial(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Obstacle)
        {
            return;
        }

        ChangeMaterial(other);
    }
} 
