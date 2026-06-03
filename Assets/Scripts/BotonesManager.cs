using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesManager : MonoBehaviour
{
    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
