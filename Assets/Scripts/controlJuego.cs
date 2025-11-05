using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena


public class controlJuego : MonoBehaviour
{
    // Patrón SINGLETON: Permite acceder a esta instancia desde cualquier otro script.
    public static controlJuego Instancia { get; private set; }

    [Header("Estado del Juego")]
    [SerializeField]
    private int puntos = 0;
    [SerializeField]
    private int vidas = 3;

    // Propiedades de solo lectura para acceder desde otros scripts (ej. UI)
    public int Puntos => puntos;
    public int Vidas => vidas;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    // Inicialización del Singleton
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            // Si ya existe otra instancia, destrúyete.
            Destroy(gameObject);
        }
        else
        {
            // Establece esta como la única instancia
            Instancia = this;
            // Mantiene el controlador vivo al cambiar de escena (si es necesario)
            DontDestroyOnLoad(gameObject);
        }
    }

    // --- Métodos de Puntuación ---
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "SCORE: " + puntos;
        }
        Debug.Log("Puntuación actual: " + puntos);
        // * Aquí iría la lógica para actualizar un texto en la interfaz de usuario (UI) *
    }

    // --- Métodos de Vida ---
    public void PerderVida()
    {
        vidas--;
        Debug.Log("Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            FinDelJuego();
        }
        // * Aquí iría la lógica para actualizar el contador o iconos de vida en la UI *
    }

    private void FinDelJuego()
    {
        Debug.Log("¡JUEGO TERMINADO!");
        // Aquí puedes implementar lo que sucede al morir:
        // Por ejemplo, cargar una escena de "Game Over" o reiniciar el nivel.
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}