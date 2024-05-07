using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PProyectile : MonoBehaviour
{
    [SerializeField] private float _speed; // Velocidad del proyectil
    [SerializeField] private float _dmg;
    [SerializeField] private float _livingTime;
    
    public int faction;
    private float _aliveTime;

    private void Start()
    {
        _aliveTime = 0;
    }

    public PProyectile(int yourFaction)
    {
        faction = yourFaction;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameManager.GameState.Play)
        {
            _aliveTime += Time.fixedDeltaTime;
            if(_aliveTime >= _livingTime ){Destroy(this);}
        
            // Mover el proyectil en la dirección hacia adelante
            transform.Translate(Vector3.forward * (_speed * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var ship = other.gameObject.GetComponent<Ship>();
        if ( ship != null)
        {
            ship.ReceiveDMG(_dmg);
            Destroy(this.gameObject);
        }
    }
}
