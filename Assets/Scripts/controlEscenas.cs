using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControlEscenas : MonoBehaviour
{
    [SerializeField] private Button botonJugar;
    private void Start()
    {
        if (botonJugar != null)
        {
            botonJugar.onClick.AddListener(cargarJuego);
        }
    }
    public void cargarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Juego02");
    }
}
