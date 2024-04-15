using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PProyectile : MonoBehaviour
{
    [SerializeField] private float _speed; // Velocidad del proyectil
    [SerializeField] private float _dmg;
    [SerializeField] private float _livingTime;
    
    private int _faction;
    private float _aliveTime;

    private void Start()
    {
        _aliveTime = 0;
    }

    public PProyectile(int yourFaction)
    {
        _faction = yourFaction;
    }

    private void Update()
    {
        _aliveTime += Time.deltaTime;
        if(_aliveTime >= _livingTime ){Destroy(this);}
        
        // Mover el proyectil en la dirección hacia adelante
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        var ship = other.gameObject.GetComponent<Ship>();
        if ( ship != null)
        {
            if (ship.faction != _faction)
            {
                ship.ReceiveDMG(_dmg);
            }
        }
    }
}
