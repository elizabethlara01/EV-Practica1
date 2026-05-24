using UnityEngine;

public class FlickeringLightSound : MonoBehaviour
{
    private Light luz;
    private AudioSource audioSource;

    [Header("Intensidad")]
    public float intensidadMin = 0f;
    public float intensidadMax = 2.0f;

    [Header("Velocidad de parpadeo")]
    public float intervaloMin = 0.05f;
    public float intervaloMax = 0.3f;

    [Header("Sonido")]
    public AudioClip sonidoChisporroteo;   // arrastra tu audio aquí en el Inspector
    public float volumenMin = 0.3f;
    public float volumenMax = 1.0f;
    public float pitchMin = 0.8f;          // variar el pitch evita que suene repetitivo
    public float pitchMax = 1.2f;
    public float probabilidadSonido = 0.4f; // 0 a 1, para que no suene en cada parpadeo

    void Start()
    {
        luz = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();

        // Configuración del AudioSource
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 1 = sonido 3D (se escucha según distancia)

        StartCoroutine(Parpadear());
    }

    System.Collections.IEnumerator Parpadear()
    {
        while (true)
        {
            luz.intensity = Random.Range(intensidadMin, intensidadMax);

            // Suena solo a veces, no en cada parpadeo
            if (sonidoChisporroteo != null && Random.value < probabilidadSonido)
            {
                audioSource.pitch = Random.Range(pitchMin, pitchMax);
                audioSource.PlayOneShot(sonidoChisporroteo, Random.Range(volumenMin, volumenMax));
            }

            yield return new WaitForSeconds(Random.Range(intervaloMin, intervaloMax));
        }
    }
}