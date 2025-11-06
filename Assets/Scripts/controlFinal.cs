using UnityEngine;
using TMPro; // Asegúrate de incluir esta librería para TextMeshPro
using UnityEngine.SceneManagement;

public class ControladorFinal : MonoBehaviour
{
    // Asigna estos TextMeshProUGUI desde el Inspector de Unity
    [SerializeField] private TextMeshProUGUI textoResultado;
    [SerializeField] private TextMeshProUGUI textoPuntuacionFinal;
    [SerializeField] private TextMeshProUGUI textoTiempoFinal;
    private const string escenaJuego = "Juego02";
    private const string escenaInicio = "Inicio1";
    void Start()
    {
        // 1. Recuperar los datos guardados
        int condicion = PlayerPrefs.GetInt("CondicionFinal", 0); // 0 es el valor por defecto (perdió)
        int puntuacion = PlayerPrefs.GetInt("PuntuacionFinal", 0); // 0 es el valor por defecto
        float tiempo = PlayerPrefs.GetFloat("TiempoFinal", 0f);
        // 2. Determinar y mostrar el resultado
        if (condicion == 1)
        {
            // El jugador ganó (victoria == true)
            textoResultado.text = "¡YOU WIN!";
            textoResultado.color = Color.green; // O el color que desees
        }
        else
        {
            // El jugador perdió (victoria == false)
            textoResultado.text = "GAME OVER";
            textoResultado.color = Color.red; // O el color que desees
        }

        // 3. Mostrar la puntuación final
        textoPuntuacionFinal.text = "SCORE: " + puntuacion;

        int minutos = Mathf.FloorToInt(tiempo / 60F);
        int segundos = Mathf.FloorToInt(tiempo % 60F);

        textoTiempoFinal.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
    public void JugarDeNuevo()
    {
        // MUY IMPORTANTE: Reactivar el tiempo antes de cargar la escena.
        Time.timeScale = 1f;

        // Destruir el objeto de control si persiste entre escenas, para evitar duplicados.
        if (controlJuego.Instancia != null)
        {
            Destroy(controlJuego.Instancia.gameObject);
        }

        SceneManager.LoadScene(escenaJuego);
    }
    // Método para regresar al menú principal (opcional, para un botón)
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        // Destruir el Singleton para que se reinicie al iniciar el juego
        if (controlJuego.Instancia != null)
        {
            Destroy(controlJuego.Instancia.gameObject);
        }

        // Carga la escena del menú (cambia "Menu01" por el nombre de tu escena de menú)
        SceneManager.LoadScene(escenaInicio);
    }
}