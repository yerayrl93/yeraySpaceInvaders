using UnityEngine;

public class vidaEnemigo : MonoBehaviour
{
    private controlEnemigo controladorFlota;

    void Start()
    {
        // 1. Obtener la referencia al controlador de flota (el objeto que tiene controlEnemigo.cs)
        controladorFlota = FindFirstObjectByType<controlEnemigo>();
    }

    // Se llama cuando algo entra en el Trigger (BoxCollider2D) de este enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 2. Comprobar si fue una bala del jugador
        if (other.CompareTag("BalaJugador")) // Asegúrate de que tu bala del jugador tiene este Tag
        {
            Destroy(other.gameObject); // Destruye la bala del jugador

            // 3. Notificar al controlador de flota que este enemigo ha muerto
            if (controladorFlota != null)
            {
                controladorFlota.EnemigoDestruido(transform);
                // Esto también llama a controlJuego.EnemigoDestruido()
            }

            // 4. Destruir este enemigo
            Destroy(gameObject);
        }
    }
}