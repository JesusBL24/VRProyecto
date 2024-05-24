using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Thrash : MonoBehaviour
{
    public XRSocketInteractor lockSocket;

    private void Start()
    {
        GameManager.OnGameStateChanged += Activate;
        Activate(GameManager.Instance.State);
    }

    private void Activate(GameManager.GameState state)
    {
        if (state == GameManager.GameState.SetUnits)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        lockSocket.selectEntered.AddListener(OnKeyInserted);
    }

    private void OnDisable()
    {
        lockSocket.selectEntered.RemoveListener(OnKeyInserted);
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        // Obtener el objeto seleccionado (la llave)
        GameObject keyObject = args.interactableObject.transform.gameObject;

        // Destruir la llave
        Destroy(keyObject);
    }
}
