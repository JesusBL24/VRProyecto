using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Ship : MonoBehaviour
{
    //Atributos generales
    [HideInInspector]public int faction;
    
    [SerializeField] protected FloatingHealthBar _healthBar;
    [SerializeField] protected float _maxHealth;
    protected float _actualHealth;
    
    [SerializeField] protected string _shipType;

    protected AudioEffector _speaker;
    
    //Atributos de movimiento
    [SerializeField] protected float _speed;
    protected GameObject _navPoint;
    [SerializeField] protected float _rotationSpeed;
    protected bool _isTravellingToNavPoint = false;
    protected bool _isMovingToTarget = false;
    
    //Atributos de ataque
    protected GameObject _attackTarget;
    protected bool _isAttacking = false;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected Material _attackRangeMaterial;
    protected GameObject _attackRangeSphere;
    [SerializeField] protected List<PCanon> canons;
    
    //Atributos de interaccion
    protected XRSimpleInteractable _interactable;

    public string GetShipType()
    {
        return _shipType;
    }

    public float GetHeatlh()
    {
        return _maxHealth;
    }

    protected void Start()
    {
        _actualHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_actualHealth,_maxHealth);
        _interactable = GetComponent<XRSimpleInteractable>();
        _navPoint = null;
        
        //Creación de una esfera que indica el attackRange
        // Crea una esfera primitiva
        _attackRangeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Establece el radio deseado
        _attackRangeSphere.transform.localScale = new Vector3(_attackRange * 2, _attackRange * 2, _attackRange * 2); // El radio se duplica en todas las dimensiones

        // Coloca el objeto esfera en la posición del GameObject que tiene este script
        _attackRangeSphere.transform.position = transform.position;
        _attackRangeSphere.transform.parent = transform;
        
        Destroy(_attackRangeSphere.GetComponent<Collider>()); //Destruye el collider
        _attackRangeSphere.GetComponent<MeshRenderer>().material = _attackRangeMaterial; //Asocia el material 
        _attackRangeSphere.SetActive(false);

        _speaker = gameObject.GetComponent<AudioEffector>();
    }
    

    protected void FixedUpdate()
    {
        if (_isMovingToTarget)
        {
            var bodyPosition = transform.position;
            var targetPosition = _attackTarget.transform.position;
            // Calcula la dirección hacia el punto destino
            Vector3 direccion = (targetPosition - bodyPosition).normalized;
            
            // Calcula la velocidad de desplazamiento en este frame
            float velocidadFrame = _speed * Time.deltaTime;

            // Calcula la nueva posición de la nave
            Vector3 nuevaPosicion = bodyPosition + direccion * velocidadFrame;

            // Mueve la nave hacia la nueva posición
            transform.position = nuevaPosicion;
        }
        if (_attackTarget != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_attackTarget.transform.position - transform.position , Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    protected void Update()
    {
        var hasTarget = (_attackTarget != null);
        //Si tiene un enemigo objetivo...
        if (hasTarget)
        {
            float dist = Vector3.Distance(_attackTarget.transform.position, transform.position);
            //... y está a rango...
            if (dist <= _attackRange)
            {
                if (_isMovingToTarget) { _isMovingToTarget = false;}
                //...y no ha empezado a disparar, dispara
                if(!_isAttacking){Fire();}
            }
            //... y no está a rango...
            else
            {
                if (!_isMovingToTarget && !_isTravellingToNavPoint) { _isMovingToTarget = true;}
                //...y está disparando, deja de disparar
                if(_isAttacking){StopFire();}
            }
        }
        else if (!hasTarget && _isAttacking)
        {
            StopFire();
        }
        
    }

    //Función apra seleccionar la nave
    public void SelectShip()
    {
        ShipsManager.Instance.SelectShip(this);
        Debug.Log("NaveSeleccionada");
    }

    //Función de recibir daño
    public float ReceiveDMG(float dmg)
    {
        _actualHealth -= dmg;
        _healthBar.UpdateHealthBar(_actualHealth,_maxHealth);
        
        if(_actualHealth <= 0){DestroyShip();}
        return _maxHealth;
    }

    protected abstract void DestroyShip();
    
    //Selecciona al enemigo a atacar
    public void Attack(GameObject target)
    {
        _attackTarget = target;
        _isTravellingToNavPoint = false;
    }

    //Función para disparar al enemigo cuando está a rango
    
    public void Fire()
    {
        _isAttacking = true;
        foreach (var canon in canons)
        {
            canon.Activate(_attackTarget);
        }
    }

    //Función para parar de disparar
    public void StopFire()
    {
        _isAttacking = false;
        foreach (var canon in canons)
        {
            canon.Deactivate();
        }
    }
    
    //Función de activación de la esfera de rango de ataque
    public void ActivateAttackRangeSphere(bool activation)
    {
        _attackRangeSphere.SetActive(activation);
    }
    
    //Dibujo de gizmos para ver el rango de ataque en el editor
    private void OnDrawGizmos()
    {
        // Establecer el color del gizmo
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.2f);

        // Dibujar la esfera en la posición del objeto con el radio del rango de ataque
        Gizmos.DrawSphere(transform.position, _attackRange);
    }
}
