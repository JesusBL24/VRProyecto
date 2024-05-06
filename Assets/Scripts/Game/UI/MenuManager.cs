using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script para manejar los diferentes menus que están en la misma escena, parte, etc.
public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image[] _menus;  //Todos los menus que se quieren manejar
    private int _actualmenuIndex = -1;        //Menu actualmenta activado
    
    //Activa el menu indicado y desactiva el anterior
    public void ActivateMenu(int index)
    {
        _menus[index].gameObject.SetActive(true);
        if(_actualmenuIndex != -1){_menus[_actualmenuIndex].gameObject.SetActive(false);}
        _actualmenuIndex = index;
    }
    
    //Desactiva el menu indicado y pone el indice de menú actual a ninguno, se usa para cuando no se quiere
    //ningun menú activado
    public void DeactivateMenu(int index)
    {
        _menus[index].gameObject.SetActive(false);
        _actualmenuIndex = -1;
    }
    
    //Desactiva y reactiva el menu indicado de forma dinámica
    public void ActivateOrDeactivateMenu(int index)
    {
        if (!_menus[index].gameObject.activeSelf)
        {
            if(_actualmenuIndex != -1){_menus[_actualmenuIndex].gameObject.SetActive(false);}
            _menus[index].gameObject.SetActive(true);
            _actualmenuIndex = index;
        }
        else
        {
            if(_actualmenuIndex != -1){_menus[_actualmenuIndex].gameObject.SetActive(false);}
            _menus[index].gameObject.SetActive(false);
            _actualmenuIndex = -1;
        }
    }
}
