using UnityEngine;

public class escudo : MonoBehaviour
{
    // Define cuántos impactos puede recibir el escudo antes de destruirse
    [SerializeField]
    private int resistencia = 3;

    [SerializeField]
    private Sprite[] spritesDano;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 🚀 CAMBIO CLAVE: Usamos OnTriggerEnter2D y el parámetro es 'Collider2D'
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // El parámetro 'collision' ahora es el Collider2D del objeto que choca
       // Debug.Log("Escudo chocado con: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);

        // El escudo debe colisionar con las balas del enemigo Y del jugador
        if (collision.gameObject.CompareTag("BalaEnemiga") ||
            collision.gameObject.CompareTag("BalaJugador"))
        {
            // 1. Reducir la resistencia
            resistencia--;

            // 2. Destruir la bala
            Destroy(collision.gameObject);

            // 3. Aplicar lógica de daño
            if (resistencia <= 0)
            {
                // El escudo ha sido destruido
                Destroy(gameObject);
            }
            else
            {
                // Si usas sprites de daño, actualiza la imagen
                AplicarDanoVisual();
            }
        }
    }

    private void AplicarDanoVisual()
    {
        if (spritesDano.Length > 0 && resistencia > 0)
        {
            int indiceDano = spritesDano.Length - resistencia;

            if (indiceDano >= 0 && indiceDano < spritesDano.Length)
            {
                spriteRenderer.sprite = spritesDano[indiceDano];
            }
        }
    }
}