using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesvanecerSprites : MonoBehaviour
{
    public GameObject objeto;
    public Transform transform1;
    public Transform transform2;
    public Transform transform3;

    // Probabilidades de desvanecimiento para cada sprite
    public float probabilityRoom1;
    public float probabilityRoom2;
    private bool eventoActivo= true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!eventoActivo){
            return;
        }
        float randomValue = Random.value; // Valor aleatorio entre 0 y 1

        // Verificar qué sprite se desvanece basado en las probabilidades
        if (randomValue < probabilityRoom1)
        {  
            objeto.transform.position= transform1.position;
        }
        else if (randomValue < probabilityRoom1 + probabilityRoom2)
        {
            objeto.transform.position= transform2.position;

        }else{
            objeto.transform.position= transform3.position;
        }
        StartCoroutine(Mostrar(objeto));
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(eventoActivo){
            StartCoroutine(Ocultar(objeto));
        }        
    }

    public void DesactivarEvento(){
        eventoActivo= false;
    }

    IEnumerator Ocultar(GameObject objeto){
        // Reducir gradualmente la opacidad del sprite
        SpriteRenderer spriteRenderer = objeto.GetComponent<SpriteRenderer>();
        for (float alpha = 1f; alpha >= 0; alpha -= Time.deltaTime)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;
            yield return null;
        }
        objeto.SetActive(false);
    }

    IEnumerator Mostrar(GameObject objeto){
        
        objeto.SetActive(true);
        // Reducir gradualmente la opacidad del sprite
        SpriteRenderer spriteRenderer = objeto.GetComponent<SpriteRenderer>();
        for (float alpha = 0f; alpha <= 1; alpha += Time.deltaTime)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;
            yield return null;
        }
    }
}