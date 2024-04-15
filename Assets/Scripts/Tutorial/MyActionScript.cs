using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyActionScript : MonoBehaviour
{
    private InputAction myAction;
    [SerializeField] private InputActionAsset myActionsAsset;
    void Start()
    {
        myAction= myActionsAsset.FindAction("XRI RightHand/My action");
    }
    void Update()
    {
        if (myAction.WasPerformedThisFrame())
        {
            Debug.Log("PULSADO");
        }
    }
}

