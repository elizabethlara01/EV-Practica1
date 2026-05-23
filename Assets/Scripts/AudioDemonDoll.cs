using UnityEngine;

public class AudioMuñeca : MonoBehaviour
{
    public AudioClip voz;
    public float distanciaParaSonar = 3f;
    public float tiempoEntreRepeticiones = 10f; // Cada 10 segundos

    private AudioSource audioSource;
    private Transform jugador;
    private float tiempoUltimaVez = -10f;

    void Start()
    {
        jugador = GameObject.FindWithTag("MainCamera").transform;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.volume = 1.5f;
        audioSource.clip = voz;
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);
        float tiempoActual = Time.time;

        if (distancia <= distanciaParaSonar && tiempoActual - tiempoUltimaVez >= tiempoEntreRepeticiones)
        {
            tiempoUltimaVez = tiempoActual;
            audioSource.Play();
        }
    }
}