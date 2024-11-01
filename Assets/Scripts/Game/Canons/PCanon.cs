using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Serialization;
//Script basico de cañon
public class PCanon: MonoBehaviour
{
    [SerializeField] private float _shotRate;  //Cadencia de disparo
    private int _faction;                      //Faccion a al que pertenece
    [SerializeField] private GameObject _proyectile;   //Proyectil que dispara

    public bool isActive;   //Indica si el Cañon activo

    private AudioHandler _audioHandler;
    
    //Mandos
    private XRBaseController _leftHand;
    private XRBaseController _rightHand;
    
    private float _shotTimer;   //Contador de tiempo tras el ultimo disparo
    private GameObject _target; //Objetivo del cañon
    private GameObject _player;

    [SerializeField][Range(0,1)]private float _intensity;
    [SerializeField]private float _duration;

    private void Start()
    {
        _leftHand = ActionManager.Instance.leftHand.GetComponent<XRBaseController>();
        _rightHand = ActionManager.Instance.rightHand.GetComponent<XRBaseController>();
        _shotTimer = 0;
        isActive = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioHandler = gameObject.GetComponentInParent<AudioHandler>();
        Debug.Log(_player);
    }

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Play)
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
    }
    
    //Funciona para disparar
    private void Shot()
    {
        ShotHapticFeedback();
        _audioHandler.PlayClip(0);
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

    //Funcion para mandar vibracion a los mandos
    private void ShotHapticFeedback()
    {
        var distance = Vector3.Distance(_player.transform.position, transform.position);
        if (distance < 3f)
        {
            _rightHand?.SendHapticImpulse(_intensity, _duration);
            _leftHand?.SendHapticImpulse(_intensity, _duration);
        }
    }
}
