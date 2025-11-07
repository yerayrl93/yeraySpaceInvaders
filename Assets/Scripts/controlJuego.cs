using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlJuego : MonoBehaviour
{
    public static controlJuego Instancia { get; private set; }
    [Header("Configuración de Escenas")]
    [SerializeField] private string nombreEscenaFinal = "Final03";

    private const float TIEMPO_INICIAL = 10f;

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
    private float tiempoTranscurrido = TIEMPO_INICIAL;
    [SerializeField] private TextMeshProUGUI textoCronometro;


    private bool juegoTerminado = false;


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
        tiempoTranscurrido = TIEMPO_INICIAL;
        if (textoCronometro != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60F);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60F);
            textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
        jugadorActual = GameObject.FindGameObjectWithTag("Jugador");
        if (jugadorActual == null)
        {
            Debug.LogError("ERROR: No se encontró el Jugador con el tag 'Jugador'. Verifica el tag y el nombre.");
        }
        // Asegúrate de que los enemigosRestantes estén inicializados correctamente en el Inspector o Start()
    }

    void Update()
    {
        // ... (resto del código)
        if (Time.timeScale > 0 && !juegoTerminado)
        {
            tiempoTranscurrido -= Time.deltaTime;
            if (tiempoTranscurrido <= 0f)
            {
                tiempoTranscurrido = 0f; // Asegura que no sea negativo
                FinDelJuego(false); // ⬅️ ¡GAME OVER por tiempo!
                return; // Salir de Update
            }
            // ... (código del cronómetro)
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

        // Esto asegura que la victoria solo se declare si tienes vidas.
        if (enemigosRestantes <= 0 && vidas > 0)
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
            FinDelJuego(false); // ⬅️ Llama a la derrota
        }
    }

    private void FinDelJuego(bool victoria)
    {
        // ⬅️ ¡BLOQUEO DE DOBLE LLAMADA! Si ya terminó, no hagas nada más.
        if (juegoTerminado) return;
        juegoTerminado = true;

        // 1. ⬅️ ¡CÓDIGO FALTANTE! Guardar la condición final AHORA.
        PlayerPrefs.SetInt("CondicionFinal", victoria ? 1 : 0);
        PlayerPrefs.SetInt("PuntuacionFinal", puntos);
        PlayerPrefs.SetFloat("TiempoFinal", TIEMPO_INICIAL - tiempoTranscurrido);
        PlayerPrefs.Save();

        // 2. Transición
        Time.timeScale = 0f;
        SceneManager.LoadScene(nombreEscenaFinal);
    }

}