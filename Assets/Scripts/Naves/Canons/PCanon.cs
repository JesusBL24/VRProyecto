using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCanon: MonoBehaviour
{
    [SerializeField] private float _shotRate;
    [SerializeField] private int faction;
    [SerializeField] private GameObject _proyectile;

    public bool isActive;
    
    private float _shotTimer;
    private GameObject _target;

    private void Start()
    {
        _shotTimer = 0;
        isActive = false;
    }

    void Update()
    {
        if (isActive)
        {
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
        // Obtener la rotación hacia el objetivo
        Quaternion rotation = Quaternion.LookRotation(_target.transform.position - transform.position, Vector3.up);

        // Spawnear el proyectil con la rotación establecida
        Instantiate(_proyectile.gameObject, transform.position, rotation);
    }

    public void Activate(GameObject target)
    {
        isActive = true;
        _target = target;
    }

    public void Deactivate()
    {
        isActive = false;
        _target = null;
        _shotTimer = 0f;
    }
}
