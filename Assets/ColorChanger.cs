using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;
    private MeshRenderer _meshRenderer;

    private void InitializeVariables()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void ChangeColor()
    {
        _meshRenderer.material.SetColor("_Color", _color);
    }

    private void Awake()
    {
        InitializeVariables();
        ChangeColor();
    }
}
