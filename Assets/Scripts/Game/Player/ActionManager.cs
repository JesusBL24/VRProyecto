using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionManager : MonoBehaviour
{
    //Atributos necesarios para gestioanr el juego
    [SerializeField] private InputActionAsset _InputActions;
    
    [SerializeField] private GameObject _leftHand;
    private TrailRenderer _leftHandTrail;
    [SerializeField] private GameObject _rightHand;
    private TrailRenderer _rightHandTrail;

    [SerializeField] private PlayerFly _playerFly;  //Script para que el movimiento vertical del jugador

    [SerializeField] private Mivry _gestureDetector;

    private TrailHandler _tailRenderGesture;
    
    //Mis inputActions
    private InputAction _moveShipActionR;
    private InputAction _moveShipActionL;
    
    private InputAction _changeTypeMovement;

    private InputAction _leftActivateRecognition;
    private InputAction _rigthActivateRecognition;
    
    private void Start()
    {
        //Asociar inputActions
        _moveShipActionR= _InputActions.FindAction("XRI RightHand Interaction/MoveShip");
        _moveShipActionL= _InputActions.FindAction("XRI LeftHand Interaction/MoveShip");
        _changeTypeMovement = _InputActions.FindAction("XRI LeftHand Locomotion/ChangeMovement");
        _leftActivateRecognition = _InputActions.FindAction("XRI LeftHand Interaction/ActivateRecognition");
        _rigthActivateRecognition = _InputActions.FindAction("XRI RightHand Interaction/ActivateRecognition");
        
        //Asociar callbacks
        _moveShipActionR.performed += context => MoveShipR();
        _moveShipActionL.performed += context => MoveShipL();
        _changeTypeMovement.performed += context => ChangeMovement();
        _leftActivateRecognition.performed += context => StartLeftRecognition(context);
        _rigthActivateRecognition.performed += context => StartRightRecognition(context);

        _tailRenderGesture = gameObject.GetComponent<TrailHandler>();

        _leftHandTrail = _leftHand.gameObject.GetComponent<TrailRenderer>();
        Debug.Log(_leftHandTrail);
        _rightHandTrail = _rightHand.gameObject.GetComponent<TrailRenderer>();
    }
    
    //Funciones para mover las naves hasta loa posición de la mano
    private void MoveShipR()
    {
        //Debug.Log("_RightHand");
        ShipsManager.Instance.selectedAllyShip.MoveToNavPoint(_rightHand.transform);
    }
    
    private void MoveShipL()
    {
        //Debug.Log("_leftHand");
        ShipsManager.Instance.selectedAllyShip.MoveToNavPoint(_leftHand.transform);
    }

    //Funcion para cambiar el tipo de movimiento entre vertical y horizontal
    private void ChangeMovement()
    {
        //Debug.Log("Cambio");
        _playerFly.enabled =  !_playerFly.enabled;
    }

    //Función que llama a 
    private void StartRightRecognition(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        
        _tailRenderGesture.ActivateTrail(_rightHandTrail);
        _gestureDetector.OnInputAction_RightTrigger(context);
    }

    private void StartLeftRecognition(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        _tailRenderGesture.ActivateTrail(_leftHandTrail);
        _gestureDetector.OnInputAction_LeftTrigger(context);
    }

    //Funcion que gestiona los callbacks del reconocimiento de patrones
    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {
        var id = gestureCompletionData.gestureID;
        _tailRenderGesture.DeactivateTrail();
        if (id< 0)
        {
            string errorMessage = GestureRecognition.getErrorMessage(id);
            return;
        }
        else
        {
            Debug.Log(id + ", " + gestureCompletionData.similarity);
            if (id == 1)
            {
                ChangeMovement();
            }
            else if (id == 2)
            {
                if (ShipsManager.Instance.selectedEnemyShip != null)
                {
                    foreach (var ship in ShipsManager.Instance._allyShips)
                    {
                        ship.Attack(ShipsManager.Instance.selectedEnemyShip.gameObject);
                    }
                }
            }
        }
    }
}
