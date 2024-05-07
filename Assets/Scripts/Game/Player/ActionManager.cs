using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionManager : ASingleton<ActionManager>
{
    //Atributos necesarios para gestioanr el juego
    public InputActionAsset _InputActions;
    
    public GameObject leftHand;
    private TrailRenderer _leftHandTrail;
    public GameObject rightHand;
    private TrailRenderer _rightHandTrail;
    
    private TrailHandler _trailHandler;  //Script que maneja los trails de las manos
    [SerializeField] private Mivry _gestureDetector;
    private bool _isLeftHand;

    [SerializeField] private PlayerFly _playerFly;  //Script para que el movimiento vertical del jugador
    
    
    [SerializeField] private XRInteractorLineVisual _tpRayLineVisual;
    [SerializeField] private GameObject _rightRayInteractor;
    [SerializeField] private GameObject _leftRayInteractor;
    
    //Mis inputActions
    [HideInInspector]public InputAction _activateRayR;
    [HideInInspector]public InputAction _activateRayL;
    
    [HideInInspector]public InputAction _leftActivateRecognition;
    [HideInInspector]public InputAction _rigthActivateRecognition;
    
    [HideInInspector]public InputAction _rightSelect;
    [HideInInspector]public InputAction _leftSelect;
    
    
    public InputAction _activateTeleport;
    
    private void Awake()
    {

        base.Awake();
        
        //Asociar inputActions
        _activateRayR= _InputActions.FindAction("XRI RightHand Interaction/ActivateRayInteractor");
        _activateRayL= _InputActions.FindAction("XRI LeftHand Interaction/ActivateRayInteractor");
        
        _leftActivateRecognition = _InputActions.FindAction("XRI LeftHand Interaction/ActivateRecognition");
        _rigthActivateRecognition = _InputActions.FindAction("XRI RightHand Interaction/ActivateRecognition");

        _rightSelect = _InputActions.FindAction("XRI RightHand Interaction/Select");
        _leftSelect = _InputActions.FindAction("XRI LeftHand Interaction/Select");
        
        _activateTeleport = _InputActions.FindAction("XRI RightHand Locomotion/ActivateTeleport");
        
        
        //Asociar callbacks
        _activateRayR.performed += context => rayInteractorsHandler(context);
        _activateRayR.canceled += context => rayInteractorsHandler(context);
        _activateRayL.performed += context => rayInteractorsHandler(context);
        _activateRayL.canceled += context => rayInteractorsHandler(context);
        
        _leftActivateRecognition.performed += context => StartLeftRecognition(context);
        _rigthActivateRecognition.performed += context => StartRightRecognition(context);
        
        _activateTeleport.performed += context => TeleportationInteractorHandler(context);
        _activateTeleport.canceled += context => TeleportationInteractorHandler(context);

        //Obtener hadler de trails y los respectivos trails
        _trailHandler = gameObject.GetComponent<TrailHandler>();
        _leftHandTrail = leftHand.gameObject.GetComponent<TrailRenderer>();
        _rightHandTrail = rightHand.gameObject.GetComponent<TrailRenderer>();
    }

    private void rayInteractorsHandler(InputAction.CallbackContext context)
    {
        if (context.action == _activateRayL)
        {
            if (context.canceled)
            {
                _leftRayInteractor.SetActive(false);
            }
            else if (context.performed)
            {
                _leftRayInteractor.SetActive(true);
            }
        }
        else if (context.action == _activateRayR)
        {
            if (context.canceled)
            {
                _rightRayInteractor.SetActive(false);
            }
            else if (context.performed)
            {
                _rightRayInteractor.SetActive(true);
            }
        }
    }

    private void TeleportationInteractorHandler(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _tpRayLineVisual.enabled = false;
        }
        else if (context.performed)
        {
            _tpRayLineVisual.enabled = true;
        }
    }
    
    //*********RECONOCIMIENTO DE PATRONES Y SUS FUNCIONES ASOCIADAS*****************************

    //Función que activa el reconocimiento de patrones de la mano derecha
    private void StartRightRecognition(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        
        _trailHandler.ActivateTrail(_rightHandTrail);
        _gestureDetector.OnInputAction_RightTrigger(context);
        _isLeftHand = false;
    }

    //Función que activa el reconocimiento de patrones de la mano izquierda
    private void StartLeftRecognition(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        _trailHandler.ActivateTrail(_leftHandTrail);
        _gestureDetector.OnInputAction_LeftTrigger(context);
        _isLeftHand = true;
    }

    //Funcion que gestiona los callbacks del reconocimiento de patrones
    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {
        var id = gestureCompletionData.gestureID;
        _trailHandler.DeactivateTrail();
        if (id< 0)
        {
            string errorMessage = GestureRecognition.getErrorMessage(id);
            return;
        }
        else
        {
            Debug.Log(id + ", " + gestureCompletionData.similarity);
            if (id == 0)
            {
                ChangeMovement(id);
            }
            else if (id == 1)
            {
                ChangeMovement(id);
            }
            else if (id == 2)
            {
                ShipsManager.Instance.AllShipsAttack();
            }
            
            else if (id == 3)
            {
                MoveShip();
            }
        }
    }
    
    //Funcion para mover la nave a la posicion de la mano
    private void MoveShip()
    {
        ShipsManager.Instance.MoveShip(_isLeftHand ? leftHand.transform : rightHand.transform);
    }
    
    //Funcion para cambiar el tipo de movimiento entre vertical y horizontal
    private void ChangeMovement(int index)
    {
        if (index == 1)
        {
            _playerFly.enabled = true;
        }
        else
        {
            _playerFly.enabled =  false;
        }
    }
}
