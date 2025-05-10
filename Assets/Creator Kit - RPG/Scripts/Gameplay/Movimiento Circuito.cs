using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCircuito : MonoBehaviour
{
    public Transform[] waypoints; // Array que contiene los puntos por los que el objeto debe pasar
    public float moveSpeed = 5f; // Velocidad de movimiento del objeto

    private int currentWaypointIndex = 0; // Índice del punto actual al que el objeto se está moviendo

    void Update()
    {
        // Si hay al menos un punto en el array de waypoints
        if (waypoints.Length > 0)
        {
            // Calcula la dirección hacia el punto actual
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Mueve el objeto hacia el punto actual
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Si el objeto está lo suficientemente cerca del punto actual, avanza al siguiente punto
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }
}
