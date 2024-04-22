using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        var gb = GameObject.Find("UI Camera");
        _camera = gb.GetComponent<Camera>();
        _slider.value = currentValue / maxValue;
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + _offset;
    }
}
