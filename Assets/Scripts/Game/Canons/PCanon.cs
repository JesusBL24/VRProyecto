using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Serialization;

public class PCanon: MonoBehaviour
{
    [SerializeField] private float _shotRate;  //Cadencia de disparo
    private int _faction;                      //Faccion a al que pertenece
    [SerializeField] private GameObject _proyectile;   //Proyectil que dispara

    public bool isActive;   //Indica si el Cañon activo

    [SerializeField] private AudioSource _audioSource;
    
    [SerializeField] private XRBaseControllerInteractor _leftHand;
    [FormerlySerializedAs("_RightHand")] [SerializeField] private XRBaseControllerInteractor _rightHand;
    
    private float _shotTimer;   //Contador de tiempo tras el ultimo disparo
    private GameObject _target; //Objetivo del cañon
    private GameObject _player;

    private void Start()
    {
        _shotTimer = 0;
        isActive = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(_player);
    }

    void Update()
    {
        //El cañon solo dispara si está activo
        if (isActive)
        {
            //Si no hay target, desactivar
            if(_target == null ){Deactivate();}
            else
            {
                // Actualizar el temporizador
                _shotTimer += Time.deltaTime;

                // Verificar si se puede disparar según la cadencia
                if (_shotTimer >= _shotRate)
                {
                    // Disparar
                    Shot();

                    // Reiniciar el temporizador
                    _shotTimer = 0f;
                }
            }
        }
    }
    private void Shot()
    {
        ShotHapticFeedback();
        // Obtener la rotación hacia el objetivo
        Quaternion rotation = Quaternion.LookRotation(_target.transform.position - transform.position, Vector3.up);
        
        // Spawnear el proyectil con la rotación establecida
        var proyectileScript = Instantiate(_proyectile, transform.position, rotation).GetComponent<PProyectile>();
        proyectileScript.faction = _faction;
    }

    //Funcion para activar el cañon
    public void Activate(GameObject target)
    {
        isActive = true;
        _target = target;
    }

    //Funcion para desactivar el cañon
    public void Deactivate()
    {
        isActive = false;
        _target = null;
        _shotTimer = 0f;
    }

    private void ShotHapticFeedback()
    {
        var distance = Vector3.Distance(_player.transform.position, transform.position);
        if (distance < 3f)
        {
            _rightHand?.SendHapticImpulse(0.3f, 0.5f);
            _leftHand?.SendHapticImpulse(0.3f, 0.5f);
        }
    }
}
