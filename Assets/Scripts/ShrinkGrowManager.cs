using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShrinkGrowManager : MonoBehaviour
{
    private bool _isBig = true;
    private TextMeshProUGUI _text;

    [SerializeField] private List<PlayerController> _playerControllers = new List<PlayerController>();
    [SerializeField] private float _duration = 1f;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ManageShrinkGrow()
    {
        _isBig = !_isBig;

        foreach (var playerController in _playerControllers)
        {
            if (_isBig)
            {
                _text.text = "-";
                playerController.Grow(_duration);
            }

            else 
            {
                _text.text = "+";
                playerController.Shrink(_duration);
            }
        }
    }
}
