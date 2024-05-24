using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrabCubeCreator : MonoBehaviour
{
    // Método para crear el prefab del cubo en una posición específica
    public GameObject CubePrefab;
    
    public void CreateCubePrefab()
    {
        // Instanciar cubo en la posición especificada
        GameObject cubeInstance = Instantiate(CubePrefab, transform.position, transform.rotation, transform);
        
    }
}
