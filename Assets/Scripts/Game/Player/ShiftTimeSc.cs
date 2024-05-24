using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftTimeSc : MonoBehaviour
{
    private bool _isTimeStopped = true;
    [SerializeField] private Image _button;

    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameChange;
    }

    public void ShiftTime()
    {
        _isTimeStopped = !_isTimeStopped;
        GameManager.Instance.ChangeState(_isTimeStopped == true? GameManager.GameState.SetUnits: GameManager.GameState.Play);
    }

    private void OnGameChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Play)
        {
            _button.color = Color.green;
        }
        else
        {
            _button.color = Color.yellow;
        }
    }
}
