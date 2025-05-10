using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisoManzana : MonoBehaviour
{
    [SerializeField] DesvanecerSprites evento1;
    [SerializeField] DesvanecerSprites evento2;
    [SerializeField] DesvanecerSprites evento3;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        evento1.DesactivarEvento();
        evento2.DesactivarEvento();
        evento3.DesactivarEvento();
    }
}
