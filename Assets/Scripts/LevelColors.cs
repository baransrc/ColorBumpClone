using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelColors : MonoBehaviour
{
    [SerializeField] private Color _primaryColor = new Color();
    [SerializeField] private Color _secondaryColor = new Color();
    [SerializeField] private Color _terinaryColor = new Color();
    [SerializeField] private Color _groundColor = new Color();

    [SerializeField] private Material _primaryMaterial = null;
    [SerializeField] private Material _secondaryMaterial = null;
    [SerializeField] private Material _terinaryMaterial = null;
    [SerializeField] private Material _groundMaterial = null;

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

    public Color TerinaryColor
    {
        get
        {
            return _terinaryColor;
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

    public Material TerinaryMaterial
    {
        get
        {
            return _terinaryMaterial;
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
        _terinaryMaterial.color = TerinaryColor;
    }
}
