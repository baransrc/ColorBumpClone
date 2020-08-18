using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelColors : MonoBehaviour
{
    [SerializeField] private Color _primaryColor;
    [SerializeField] private Color _secondaryColor;
    [SerializeField] private Color _groundColor;
    [SerializeField] private Material _primaryMaterial;
    [SerializeField] private Material _secondaryMaterial;
    [SerializeField] private Material _groundMaterial;

    public Color GroundColor
    {
        get
        {
            return _groundColor;
        }
    }

    public Color PrimaryColor 
    {
        get 
        {
            return _primaryColor;
        } 
    }

    public Color SecondaryColor
    {
        get
        {
            return _secondaryColor;
        }
    }

    public Material PrimaryMaterial
    {
        get
        {
            return _primaryMaterial;
        }
    }

    public Material SecondaryMaterial
    {
        get
        {
            return _secondaryMaterial;
        }
    }

    public Material GroundMaterial
    {
        get
        {
            return _groundMaterial;
        }
    }


    private void Awake()
    {
        _primaryMaterial.color = PrimaryColor;
        _secondaryMaterial.color = SecondaryColor;
        _groundMaterial.color = GroundColor;
    }
}
