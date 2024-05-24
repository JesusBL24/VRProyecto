using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrabDeployment : MonoBehaviour
{ 
    [SerializeField]private GameObject shipPrefab; // Prefab de la nave
    private ShipGrabCubeCreator _creator; //creator de la navae

    private GameObject _grabbedCube; // Referencia al cubo agarrado

    // Método para asociar los atributos

    public void Start()
    {
        _creator = GetComponentInParent<ShipGrabCubeCreator>();
    }

    // Método llamado cuando el cubo es agarrado
    public void OnGrab()
    {
        _grabbedCube = gameObject; // Guardar referencia al cubo agarrado
        
        _creator.CreateCubePrefab(); // Crear cubo nuevo
        
    }

    // Método llamado cuando el cubo es soltado
    public void OnRelease()
    {
        if (_grabbedCube != null)
        {
            // Obtener la posición en la que se soltó el cubo
            Vector3 releasePosition = transform.position;

            // Instanciar la nave en la posición de soltado
            Instantiate(shipPrefab, releasePosition, Quaternion.identity);

            // Eliminar el cubo original
            Destroy(_grabbedCube);
        }
    }
}
