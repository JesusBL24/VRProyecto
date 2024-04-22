using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _InputActions;
    
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    [SerializeField] private PlayerFly _playerFly;
    
    
    private InputAction _moveShipActionR;
    private InputAction _moveShipActionL;
    
    private InputAction _changeTypeMovement;
    
    private void Start()
    {
        _moveShipActionR= _InputActions.FindAction("XRI RightHand Interaction/MoveShip");
        _moveShipActionL= _InputActions.FindAction("XRI LeftHand Interaction/MoveShip");
        _changeTypeMovement = _InputActions.FindAction("XRI LeftHand Locomotion/ChangeMovement");
        
        _moveShipActionR.performed += context => MoveShipR();
        _moveShipActionL.performed += context => MoveShipL();
        _changeTypeMovement.performed += context => ChangeMovement();
        Debug.Log(_changeTypeMovement);
    }
    
    private void MoveShipR()
    {
        Debug.Log("_RightHand");
        ShipsManager.Instance.SelectedAllyShip.MoveToNavPoint(_rightHand);
    }
    
    private void MoveShipL()
    {
        Debug.Log("_leftHand");
        ShipsManager.Instance.SelectedAllyShip.MoveToNavPoint(_leftHand);
    }

    private void ChangeMovement()
    {
        Debug.Log("Cambio");
        _playerFly.enabled =  !_playerFly.enabled;
    }
    
}
