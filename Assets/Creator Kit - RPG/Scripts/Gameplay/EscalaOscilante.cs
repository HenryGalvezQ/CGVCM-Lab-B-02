using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalaOscilante : MonoBehaviour
{
    public float velocidadCambioEscala = 0.5f; // Velocidad de cambio de escala en unidades por segundo
    public float tiempoEspera = 1.5f;

    private bool aumentandoEscala = true; // Indicador de dirección de cambio de escala

    void Start()
    {
        InvokeRepeating("CambiarDireccionCambioEscala", tiempoEspera / 2.0f, tiempoEspera);
    }

    void CambiarDireccionCambioEscala()
    {
        // Cambia la dirección del cambio de escala cada dos segundos
        aumentandoEscala = !aumentandoEscala;
    }

    void Update()
    {
        // Calcula la cantidad de cambio de escala basado en el tiempo y la velocidad
        float cambioEscala = velocidadCambioEscala * Time.deltaTime;

        // Realiza el cambio de escala
        if (aumentandoEscala)
        {
            transform.localScale += new Vector3(cambioEscala, cambioEscala, cambioEscala);
        }
        else
        {
            transform.localScale -= new Vector3(cambioEscala, cambioEscala, cambioEscala);
        }
    }
}
