using UnityEngine;

public class controlJugador : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float limiteIzquierdo = -0.18f;
    [SerializeField] private float limiteDerecho = 15.55f;

    [Header("Disparo")]
    [SerializeField] private GameObject bala;
    [SerializeField] private Transform spot;
    [SerializeField] private float tiempoDisparo = 0.5f;
    private float tiempoTranscurrido;

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(velocidad * Time.deltaTime * Vector2.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-velocidad * Time.deltaTime, 0, 0);
        }

        
        Vector3 posicionLimitada = transform.position;

        
        posicionLimitada.x = Mathf.Clamp(posicionLimitada.x, limiteIzquierdo, limiteDerecho);
        transform.position = posicionLimitada;

        tiempoTranscurrido += Time.deltaTime;
        if (tiempoTranscurrido > tiempoDisparo && Input.GetKey(KeyCode.Space))
        {
            tiempoTranscurrido = 0;
            Instantiate(bala, spot.position, Quaternion.identity);
        }

        
    }
}