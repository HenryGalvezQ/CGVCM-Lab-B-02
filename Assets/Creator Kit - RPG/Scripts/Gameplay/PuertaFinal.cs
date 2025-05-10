using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaFinal : MonoBehaviour
{
    public float duracionAnimacion = 1.0f;
    public bool aparecer;
    // Start is called before the first frame update
    void Start()
    {
        if(aparecer){
            StartCoroutine(AumentarEscala());
        }else{
            StartCoroutine(DisminuirEscala());
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player") && aparecer)
        {
            // Llama a un método o realiza alguna acción cuando colisiona con el jugador
            SceneManager.LoadScene(1);
        }
    }


    IEnumerator AumentarEscala()
    {
        // Escala inicial del objeto
        Vector3 escalaInicial = transform.localScale;

        // Escala final deseada
        Vector3 escalaFinal = Vector3.one; // (1, 1, 1)

        // Tiempo transcurrido
        float tiempoPasado = 0f;

        // Mientras no haya alcanzado la escala final
        while (tiempoPasado < duracionAnimacion)
        {
            // Incrementa gradualmente la escala en función del tiempo
            float factor = tiempoPasado / duracionAnimacion;
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, factor);

            // Incrementa el tiempo transcurrido
            tiempoPasado += Time.deltaTime;

            // Espera al siguiente frame
            yield return null;
        }

        // Asegura que la escala final sea exactamente 1
        transform.localScale = escalaFinal;
    }

    IEnumerator DisminuirEscala()
    {
        // Escala inicial del objeto (comenzando desde 1)
        Vector3 escalaInicial = Vector3.one; // (1, 1, 1)

        // Escala final deseada (terminando en 0.1)
        Vector3 escalaFinal = new Vector3(0.1f, 0.1f, 0.1f);

        // Tiempo transcurrido
        float tiempoPasado = 0f;

        // Mientras no haya alcanzado la escala final
        while (tiempoPasado < duracionAnimacion)
        {
            // Incrementa gradualmente la escala en función del tiempo
            float factor = tiempoPasado / duracionAnimacion;
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, factor);

            // Incrementa el tiempo transcurrido
            tiempoPasado += Time.deltaTime;

            // Espera al siguiente frame
            yield return null;
        }

        // Asegura que la escala final sea exactamente 0.1
        transform.localScale = escalaFinal;
        gameObject.SetActive(false);
    }
}
