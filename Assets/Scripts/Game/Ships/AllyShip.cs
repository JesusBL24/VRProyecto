using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AllyShip : Ship
{
    private LineRenderer _lineRenderer;
    private GameObject _navPoint;
    void Start()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        faction = 0;
        ShipsManager.Instance.allyShips.Add(this);
        _navPoint = null;
        base.Start();
    }
    public void MoveToNavPoint(Transform newPositionTransform)
    {
        if(_navPoint != null){Destroy(_navPoint); }
        
        var navPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
        navPoint.GetComponent<BoxCollider>().enabled = false;
        navPoint.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        navPoint.transform.position = newPositionTransform.position;
        navPoint.transform.localScale = new Vector3(0.2f, 0.2f,0.2f);
        
        _navPoint = navPoint;
        
        _lineRenderer.enabled = true;
        StablishLineRenderer();
        
        _isTravellingToNavPoint = true;
        _isMovingToTarget = false;
        //_attackTarget = null;
    }

    private void StablishLineRenderer()
    {
        _lineRenderer.SetPosition(0,transform.position);
        _lineRenderer.SetPosition(1,_navPoint.transform.position);
    }
    public void OnReleaseGRab()
    {
        StablishLineRenderer();
    }

    private void EndMovementToNavPoint()
    {
        // La nave ha llegado al destino
        Debug.Log("La nave ha llegado al destino.");
        Destroy(_navPoint);
        _navPoint = null;
        _isTravellingToNavPoint = false;
        _lineRenderer.enabled = false;
    }
    
    protected void FixedUpdate()
    {
        if (GameManager.Instance.State == GameManager.GameState.Play)
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
                    EndMovementToNavPoint();
                }
            }
        
            base.FixedUpdate();
        }
    }

    protected void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Play)
        {
            base.Update();
            if (_isAttacking)
            {
                _lineRenderer.SetPosition(0,gameObject.transform.position);
                _lineRenderer.SetPosition(1,_attackTarget.transform.position);
            }
            else if (_isTravellingToNavPoint)
            {
                _lineRenderer.SetPosition(0,gameObject.transform.position);
            }
        }
    }
    protected override void DestroyShip()
    {
        GameManager.OnGameStateChanged -= GrabActivation;
        ShipsManager.Instance.allyShips.Remove(this);
        if(_navPoint != null){Destroy(_navPoint);}
        Destroy(this.gameObject);
    }

    public void Attack(GameObject target)
    {
        base.Attack(target);
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0,transform.position  );
        _lineRenderer.SetPosition(1,target.transform.position  );
        Destroy(_navPoint);
        _navPoint = null;
    }
}
