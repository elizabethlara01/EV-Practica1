using UnityEngine;

public class AudioMuñeca : MonoBehaviour
{
    public AudioClip voz;
    public AudioClip sonidoLlorando;
    public AudioClip sonidoTareando;
    public AudioClip sonidoRespiracion;

    public float distanciaParaSonar = 3f;
    public float tiempoEntreRepeticiones = 10f;

    private AudioSource audioSource;
    private Transform jugador;
    private float tiempoUltimaVez = -10f;
    private Animator animator;    

    
/*
    void Awake()
    {
        jugador = GameObject.FindWithTag("MainCamera").transform;
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.volume = 4f;
        
    }
*/
    void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.volume = 4f;
        }
        if (jugador == null)
            jugador = GameObject.FindWithTag("MainCamera").transform;
        if (animator == null)
            animator = GetComponent<Animator>();
    }
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);
        float tiempoActual = Time.time;

        if (tiempoActual - tiempoUltimaVez >= tiempoEntreRepeticiones)
        {
            if (animator.GetBool("llorando"))
            {
                tiempoUltimaVez = tiempoActual;
                audioSource.clip = sonidoLlorando;
                audioSource.Play();
            }
            else if (animator.GetBool("sentada"))
            {
                tiempoUltimaVez = tiempoActual;
                audioSource.clip = sonidoTareando;
                audioSource.Play();
            }
            else if (!animator.GetBool("llorando")&&!animator.GetBool("sentada")&&distancia <= distanciaParaSonar)
            {
                tiempoUltimaVez = tiempoActual;
                audioSource.clip = voz;
                audioSource.Play();
            }
        }
    }

    public void ReproducirRespiracion()
    {
        audioSource.clip = sonidoRespiracion;
        audioSource.Play();
    }
}