using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]private float _speed;
    [SerializeField]private GameObject _navPoint;
    [SerializeField]private GameObject _Body;
    
    //Variables para calculos de posicion y movimiento
    private Vector3 _bodyPosition;
    private Vector3 _navPosition;
    
    public int faction;
    
    [SerializeField] private float _health;
    [SerializeField] private string _shipType;
    [SerializeField] private List<PCanon> canons;

    public string GetShipType() { return _shipType;}
    
    private void Start()
    {
        _navPoint.SetActive(false);
        canons.Append(gameObject.GetComponent<PCanon>());
    }

    public void Move(Vector3 newNavPoint)
    {
        //Debug.Log("Nave: Movement");
        _navPoint.SetActive(true);
        _navPoint.transform.position = newNavPoint;
    }

    public void SeleccionarNave()
    {
        ShipsManager.Instance.SeleccionarNave(this);
        Debug.Log("NaveSeleccionada");
    }

    void FixedUpdate()
    {
        if (_navPoint.activeInHierarchy)
        {
            _bodyPosition = _Body.transform.position;
            _navPosition = _navPoint.transform.position;
            // Calcula la dirección hacia el punto destino
            Vector3 direccion = (_navPosition - _bodyPosition).normalized;

            // Calcula la distancia al punto destino
            float distancia = Vector3.Distance(_bodyPosition, _navPosition);

            // Si la distancia es mayor que un umbral pequeño, sigue moviéndote
            if (distancia > 0.1f)
            {
                // Calcula la velocidad de desplazamiento en este frame
                float velocidadFrame = _speed * Time.deltaTime;

                // Calcula la nueva posición de la nave
                Vector3 nuevaPosicion = _bodyPosition + direccion * velocidadFrame;

                // Mueve la nave hacia la nueva posición
                _Body.transform.position = nuevaPosicion;
            }
            else
            {
                // La nave ha llegado al destino
                Debug.Log("La nave ha llegado al destino.");
                _navPoint.SetActive(false);
            }
        }
    }

    public float ReceiveDMG(float dmg)
    {
        _health -= dmg;

        Debug.Log("Impacto");
        return _health;
    }

    public void Attack(GameObject target)
    {
        foreach (var canon in canons)
        {
            canon.Activate(target);
        }
    }
}
