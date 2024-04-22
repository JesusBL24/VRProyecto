using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShip : Ship
{
    void Start()
    {
        faction = 0;
        ShipsManager.Instance._allyShips.Add(this);
        base.Start();
    }
    public void MoveToNavPoint(Transform newPositionTransform)
    {
        _speaker.PlayFlying();
        if(_navPoint != null){Destroy(_navPoint); }
        
        var navPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
        navPoint.transform.position = newPositionTransform.position;
        navPoint.transform.localScale = new Vector3(0.2f, 0.2f,0.2f);
        
        _navPoint = navPoint;
        _isTravellingToNavPoint = true;
        _isMovingToTarget = false;
        //_attackTarget = null;
    }

    protected void FixedUpdate()
    {
        if (_isTravellingToNavPoint)
        {
            var _bodyPosition = transform.position;
            var _navPosition = _navPoint.transform.position;
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
                transform.position = nuevaPosicion;
            }
            else
            {
                // La nave ha llegado al destino
                Debug.Log("La nave ha llegado al destino.");
                Destroy(_navPoint);
                _navPoint = null;
                _isTravellingToNavPoint = false;
            }
        }
        
        base.FixedUpdate();
        
    }

    protected override void DestroyShip()
    {
        Destroy(this.gameObject);
        ShipsManager.Instance._allyShips.Remove(this);
    }

    public void Attack(GameObject target)
    {
        base.Attack(target);
        Destroy(_navPoint);
        _navPoint = null;
    }
}
