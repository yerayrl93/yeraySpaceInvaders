using UnityEngine;

public class vidaEnemigo : MonoBehaviour
{
    private controlEnemigo controladorFlota;
    private bool destruido = false;

    void Start()
    {
        // 1. Obtener la referencia al controlador de flota (el objeto que tiene controlEnemigo.cs)
        controladorFlota = FindFirstObjectByType<controlEnemigo>();
    }

    // Se llama cuando algo entra en el Trigger (BoxCollider2D) de este enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destruido) return;

        if (other.CompareTag("BalaJugador"))
        {
            destruido = true;
            Destroy(other.gameObject);

            var anim = GetComponent<animacion>();
            if (anim != null)
            {
                anim.MostrarDestruccion();
            }

            if (controladorFlota != null)
            {
                controladorFlota.EnemigoDestruido(transform);
            }

            // 🕒 Esperar para que el sprite se vea
            StartCoroutine(DestruirDespuesDe(0.3f));
        }
    }
    private System.Collections.IEnumerator DestruirDespuesDe(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Destroy(gameObject);
    }
}