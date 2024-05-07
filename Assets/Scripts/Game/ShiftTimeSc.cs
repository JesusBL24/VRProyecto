using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTimeSc : MonoBehaviour
{
    private bool _isTimeStopped = true;
    public void ShiftTime()
    {
        _isTimeStopped = !_isTimeStopped;
        GameManager.Instance.ChangeState(_isTimeStopped == true? GameManager.GameState.SetUnits: GameManager.GameState.Play);
    }
}
