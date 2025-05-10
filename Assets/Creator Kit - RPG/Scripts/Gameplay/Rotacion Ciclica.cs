using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCiclica : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotación del objeto en grados por segundo
    public float rotationTimeChange = 5f;

    private bool rotateClockwise = true; // Indica si el objeto debe rotar en sentido horario o antihorario

    void Start()
    {
        // Inicia la llamada a la función ChangeRotationDirection cada 5 segundos
        InvokeRepeating("ChangeRotationDirection", 0f, rotationTimeChange);
    }

    void Update()
    {
        // Calcula el ángulo de rotación por frame basado en la velocidad de rotación y el tiempo transcurrido
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Si el objeto debe rotar en sentido horario, rota en esa dirección
        if (rotateClockwise)
        {
            transform.Rotate(Vector3.forward, rotationAmount);
        }
        // Si no, rota en sentido antihorario
        else
        {
            transform.Rotate(Vector3.forward, -rotationAmount);
        }
    }

    void ChangeRotationDirection()
    {
        // Genera un número aleatorio entre 0 y 1
        float randomValue = Random.value;

        // Si el número aleatorio es mayor o igual a 0.5, cambia la dirección de rotación
        if (randomValue >= 0.5f)
        {
            rotateClockwise = !rotateClockwise;
        }
    }
}
