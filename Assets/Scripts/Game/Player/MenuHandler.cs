using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Script para manejar el menu de la muñeca
public class MenuHandler : MonoBehaviour
{
    //Menu de nave seleccionadas
    [SerializeField] private GameObject _selectedShipsPanel;
    private bool _isActive = false;
    private bool _flagToInteract = true;
    [SerializeField] private TextMeshProUGUI _textoAliado;
    [SerializeField] private TextMeshProUGUI _textoHPAliado;
    [SerializeField] private TextMeshProUGUI _textoEnemigo;
    [SerializeField] private TextMeshProUGUI _textoHPEnemigo;

    private void Update()
    {
        if (ShipsManager.Instance.selectedAllyShip != null)
        {
            _textoAliado.text = ShipsManager.Instance.selectedAllyShip?.name;
            _textoHPAliado.text = "HP: " + ShipsManager.Instance.selectedAllyShip?.GetActualHeatlh();
        }
        else
        {
            _textoAliado.text = "Nave aliada sin seleccionar";
            _textoHPAliado.text = "";
        }

        if (ShipsManager.Instance.selectedEnemyShip != null)
        {
            _textoEnemigo.text = ShipsManager.Instance.selectedEnemyShip?.name;
            _textoHPEnemigo.text = "HP: " +  ShipsManager.Instance.selectedEnemyShip?.GetActualHeatlh();
        }
        else
        {
            _textoEnemigo.text = "Nave enemiga sin seleccionar";
            _textoHPEnemigo.text = "";
        }
        
    }


    public void ActivateSelectedShipsPanel()
    {
        if (_flagToInteract)
        {
            _flagToInteract = false;
            Debug.Log("Hola");
            Invoke("RebootCounter", 0.5f);
            _isActive = !_isActive;
            _selectedShipsPanel.SetActive(_isActive);
        }
    }

    private void RebootCounter()
    {
        _flagToInteract = true;
    }
}
