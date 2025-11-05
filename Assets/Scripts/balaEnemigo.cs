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
       
        if (collision.CompareTag("Jugador"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

      
    }
}
