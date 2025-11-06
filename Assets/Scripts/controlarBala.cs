using UnityEngine;

public class controlarBala : MonoBehaviour
{

    [SerializeField]
    private float velocidad = 15f;
    [SerializeField]
    private float tiempoVida = 3f;

    public void AutoDestruccion()
    {
        gameObject.SetActive(false);    
        Destroy(gameObject);
    }
    private void Awake()
    {
        Destroy(gameObject, tiempoVida);
    }
    void Update()
    {
        transform.Translate(velocidad * Time.deltaTime * Vector2.up);   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            // Solo destruye la bala.
            // El enemigo (vidaEnemigo.cs) manejará su animación y destrucción.
            controlJuego.Instancia.SumarPuntos(10);
            Destroy(gameObject);
        }
    }
}
