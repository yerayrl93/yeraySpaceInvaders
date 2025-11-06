using UnityEngine;

public class controlEnemigo : MonoBehaviour
{
    // en este script cremaos el blocke de enemigos en estructura
    //usamos serializable para que sea visible en el inspector 
    [System.Serializable]
    public struct tipoEnemigo
    {
        public string nombre;
        public Sprite[] imagen;
        public int puntos;
        public int filas;
    }

    [Header("Invocacion")]
    [SerializeField] private tipoEnemigo[] enemigos;
    [SerializeField] private int columnasTotales;
    [SerializeField] private float xEspacio, yEspacio;
    [SerializeField] private Transform puntoDeInvocacion;
    private float minX;

    [Header("Movimiento")]
    // [SerializeField] private float velocidad = 10f;
    [SerializeField] private float tiempoEntreMovimientos = 2f;
    private float temporizadorMovimiento;
    [SerializeField] private float maximoX;

    [Header("Disparo Enemigo")]
    [SerializeField] private GameObject prefabBalaEnemigo;
    [SerializeField] private float tiempoEntreDisparos = 1.0f; // Frecuencia de disparo
    private float temporizadorDisparo;

    private Transform[,] posEnemigos;
    private int filas;
    private bool semueveAlaDerecha = true;
    private float maxX;
    private float posicionActualX;
    private float incrementoY;
    private float incrementoX;


    //la magia dela invocacion
    void Start()
    {
        minX = puntoDeInvocacion.position.x;
        GameObject escuadron = new GameObject { name = "Escuadron" };
       

        Vector2 posicionActual = puntoDeInvocacion.position;


        foreach (var tipoEnemigo in enemigos)
        {
            filas += tipoEnemigo.filas;
        }
        maxX = minX + maximoX * xEspacio * columnasTotales;
        posicionActualX = minX;
        posEnemigos = new Transform[filas, columnasTotales];
        int filasTotales = 0;

        //foreach va a  recorrer todos nuestros tipos de enemigos
        foreach (var tipoEnemigo in enemigos)
        {
            var nombreEnemigo = tipoEnemigo.nombre.Trim();
            for (int x = 0; x < tipoEnemigo.filas; x++)
            {
                for (int y = 0; y < columnasTotales; y++)
                {
                    var enemigo = new GameObject { name = nombreEnemigo };
                    enemigo.transform.position = posicionActual;
                    enemigo.transform.localScale = new Vector3(2f, 2f, 0f);
                    enemigo.transform.SetParent(escuadron.transform);
                    // Dentro del loop de invocación en controlEnemigo.cs:
                    // ...
                    enemigo.transform.SetParent(escuadron.transform);
                    // Añadir el script de vida al enemigo
                    enemigo.AddComponent<vidaEnemigo>(); // <--- ¡Añadir esta línea!
                                                         // ...
                    SpriteRenderer imagen = enemigo.AddComponent<SpriteRenderer>();
                    imagen.sprite = tipoEnemigo.imagen[y % tipoEnemigo.imagen.Length];
                    imagen.sortingLayerName = "Default";
                    imagen.sortingOrder = 10;
                    enemigo.tag = "Enemigo";
                    posEnemigos[filasTotales, y] = enemigo.transform;
                    posicionActual.x += xEspacio;

                    //boxcollider2d

                    BoxCollider2D boxCollider = enemigo.AddComponent<BoxCollider2D>();
                    boxCollider.size = new Vector2(0.3f, 0.3f);

                    //rigidbody2d

                    Rigidbody2D rb = enemigo.AddComponent <Rigidbody2D>();
                    rb.gravityScale = 0;
                    rb.bodyType = RigidbodyType2D.Kinematic;


                }
                posicionActual.x = minX;
                posicionActual.y -= yEspacio;

                filasTotales++;
            }
        }
    }
    void Update()
    {
        temporizadorMovimiento += Time.deltaTime;
        if (temporizadorMovimiento >= tiempoEntreMovimientos)
        {
            temporizadorMovimiento = 0f;
            if (semueveAlaDerecha)
            {
                posicionActualX += xEspacio; 
                if (posicionActualX < maxX)
                {
                    Mover(xEspacio, 0); 
                }
                else
                {
                    CambioDireccion(); 
                }
            }
            else 
            {
                posicionActualX -= xEspacio;
                if (posicionActualX > minX)
                {
                    Mover(-xEspacio, 0); 
                }
                else
                {
                    CambioDireccion(); 
                }
            }
        }
        temporizadorDisparo += Time.deltaTime;
        if (temporizadorDisparo >= tiempoEntreDisparos)
        {
            temporizadorDisparo = 0f;
            DispararAleatorio(); // ¡Llamada a la función de disparo!
        }
    }
    private void DispararAleatorio()
    {
        int columna = Random.Range(0, columnasTotales);
        Transform enemigoSeleccionado = null;

        for (int i = filas - 1; i >= 0; i--) {
            if (posEnemigos[i, columna] != null)
            {
                enemigoSeleccionado = posEnemigos[i, columna];
                break;
            }
        }
        if (enemigoSeleccionado != null)
        {
            GameObject nuevaBala = Instantiate(prefabBalaEnemigo, enemigoSeleccionado.position, Quaternion.identity);

           // Debug.Log("bala instanciada en: " + nuevaBala.transform.position + " Nombre: " + nuevaBala.name);
        }
    }
    private void Mover (float x, float y )
    {
        for ( int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnasTotales; j++)
            {
                if (posEnemigos[i, j] != null) // se verifica si el enemigo existe antes de moverlo
                {
                    posEnemigos[i, j].Translate(x, y, 0);
                }
            }
        }
    }
    private void CambioDireccion()
    {
        semueveAlaDerecha = !semueveAlaDerecha;
        Mover(0, -yEspacio);
    }
    public void EnemigoDestruido(Transform enemigoDestruido)
    {
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnasTotales; j++)
            {
            
                if (posEnemigos[i, j] == enemigoDestruido)
                {
                    posEnemigos[i, j] = null;
        
                    if (controlJuego.Instancia != null)
                    {
                        controlJuego.Instancia.EnemigoDestruido();
                      
                    }
                    return;
                }
            }
        }
    }
}
