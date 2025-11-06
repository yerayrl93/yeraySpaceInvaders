using UnityEngine;

public class balaEnemigo : MonoBehaviour
{

    [SerializeField]
    private float velocidad = 20f;
    [SerializeField]
    private float tiempoBala= 3f;

    public void AutoDestruccion()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private void Awake()
    {
        Invoke("AutoDestruccion", tiempoBala);

    }


    void Update()
    {
        transform.Translate(velocidad * Time.deltaTime * Vector2.down);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // El cuerpo del Jugador debe tener un Collider2D y el Tag "Jugador".
        if (collision.CompareTag("Jugador"))
        {
            // 🛑 ¡CRUCIAL! Notificar el golpe al Singleton
            if (controlJuego.Instancia != null)
            {
                controlJuego.Instancia.PerderVida();
            }

            // ❌ ELIMINAR ESTA LÍNEA: Destroy(collision.gameObject);
            // Esto destruye al jugador, sin importar sus vidas.

            // Destruir la bala (gameObject), que es lo correcto para que desaparezca
            Destroy(gameObject);
        }

        // 💡 NOTA: Puedes añadir aquí más lógica para colisiones con límites, etc.
    }
}
