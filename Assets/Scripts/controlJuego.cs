using TMPro;

using UnityEngine;

using UnityEngine.SceneManagement; 

public class controlJuego : MonoBehaviour

{

    public static controlJuego Instancia { get; private set; }
    [Header("Configuración de Escenas")]
    [SerializeField] private string nombreEscenaFinal = "Final03";

    [Header("Estado del Juego")]
    [SerializeField]
    private int puntos = 0;
    [SerializeField]
    private int vidas = 3;
    [SerializeField]
    private int enemigosRestantes;
    private GameObject jugadorActual;
    public int Puntos => puntos;
    public int Vidas => vidas;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    private float tiempoTranscurrido = 0f;
    [SerializeField] private TextMeshProUGUI textoCronometro;


    private void Awake()

    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()

    {

        Time.timeScale = 1f;
        jugadorActual = GameObject.FindGameObjectWithTag("Jugador");
        if (jugadorActual == null)
        {
            Debug.LogError("ERROR: No se encontró el Jugador con el tag 'Jugador'. Verifica el tag y el nombre.");
        }
    }
    void Update()
    {
        
        if (Time.timeScale > 0)
        {
            tiempoTranscurrido += Time.deltaTime;

            if (textoCronometro != null)
            {
               
                int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60F);
                int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60F); 
                textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            }
        }
    }
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "SCORE: " + puntos;
        }
    }
    public void EnemigoDestruido()

    {
        enemigosRestantes--;
        if (enemigosRestantes <= 0)
        {
            FinDelJuego(true);
        }
    }
    public void PerderVida()

    {
        vidas--;
        Debug.Log("Vidas restantes: " + vidas);
        if (vidas <= 0)
        {
       
            if (jugadorActual != null)
            {
               
                Destroy(jugadorActual);
            }

            FinDelJuego(false); 
        }
    }
    private void FinDelJuego(bool victoria)

    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(nombreEscenaFinal);
    }

}