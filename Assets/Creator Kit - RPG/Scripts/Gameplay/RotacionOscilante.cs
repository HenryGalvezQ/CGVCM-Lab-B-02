using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionOscilante : MonoBehaviour
{
    public float velocidadRotacion = 30f; // Velocidad de rotación en grados por segundo
    public float tiempoEspera= 1.5f;

    private bool rotacionPositiva = true; // Indicador de dirección de rotación

    void Start()
    {
        InvokeRepeating("CambiarValorBooleano", tiempoEspera/2.0f, tiempoEspera);
    }

    void CambiarValorBooleano()
    {
        // Cambia el valor del booleano cada dos segundos
        rotacionPositiva = !rotacionPositiva;
    }

    void Update()
    {
        // Calcula el ángulo de rotación basado en el tiempo y la velocidad
        float anguloRotacion = velocidadRotacion * Time.deltaTime;        

        // Realiza la rotación
        if (rotacionPositiva)
        {
            transform.Rotate(Vector3.forward, anguloRotacion);
        }
        else
        {
            transform.Rotate(Vector3.forward, -anguloRotacion);
        }
    }
}