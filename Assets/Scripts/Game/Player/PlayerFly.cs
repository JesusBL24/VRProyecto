using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    [SerializeField]private float speed = 3.0f; // Velocidad de movimiento vertical

    // Update is called once per frame
    void FixedUpdate()
    {
        // Obtener la entrada de teclado para moverse hacia arriba y abajo
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular el desplazamiento vertical
        float verticalMovement = verticalInput * speed * Time.deltaTime;

        // Aplicar el desplazamiento vertical al personaje
        transform.Translate(0.0f, verticalMovement, 0.0f);
    }
}
