using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyHandController : MonoBehaviour
{
    [SerializeField] InputActionReference actionGrip;
    [SerializeField] InputActionReference actionTrigger;
    private Animator handAnimator;
    void Start()
    {
        //actionTrigger.action.performed += TriggerPress;
        actionGrip.action.performed += GripPress;
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        handAnimator.SetFloat("Grip", actionGrip.action.ReadValue<float>());
        handAnimator.SetFloat("Trigger", actionTrigger.action.ReadValue<float>());
    }

    private void GripPress(InputAction.CallbackContext obj)
    {
        ShipsManager.Instance.MoveShip(gameObject.transform.position);
    }
}
