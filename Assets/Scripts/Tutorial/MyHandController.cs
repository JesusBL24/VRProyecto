using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MyHandController : MonoBehaviour
{
    [SerializeField] InputActionReference actionGrip;
    [SerializeField] InputActionReference actionTrigger;
    private Animator handAnimator;
    void Start()
    {
        handAnimator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        handAnimator.SetFloat("Grip", actionGrip.action.ReadValue<float>());
        handAnimator.SetFloat("Trigger", actionTrigger.action.ReadValue<float>());
    }
}
