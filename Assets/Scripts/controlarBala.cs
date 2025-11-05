using UnityEngine;

public class controlarBala : MonoBehaviour
{

    [SerializeField]
    private float velocidad = 15f;
    [SerializeField]
    private float tiempoVida = 3f;
    private controlEnemigo enemigoManager;

    public void AutoDestruccion()
    {
        gameObject.SetActive(false);    
        Destroy(gameObject);
    }
    private void Awake()
    {
        Invoke("AutoDestruccion", tiempoVida);

    }
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(velocidad * Time.deltaTime * Vector2.up);   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemigo"))
        {
            if (enemigoManager != null)
            {
                enemigoManager.EnemigoDestruido(collision.transform);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.CompareTag("Enemigo"))
        {
            controlJuego.Instancia.SumarPuntos(10);
            Destroy (collision.gameObject);
            Destroy (gameObject);   
        }
    }
}
