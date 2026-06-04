using UnityEngine;

public class TiempoPartida : MonoBehaviour
{
    public static TiempoPartida Instance;

    private float tiempoInicio;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            tiempoInicio = Time.time;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float ObtenerTiempo() => Time.time - tiempoInicio;

    public void DetenerYGuardar()
    {
        float tiempo = Time.time - tiempoInicio;
        PlayerPrefs.SetFloat("UltimoTiempo", tiempo);
        PlayerPrefs.Save();
    }
}
