using UnityEngine;
using UnityEngine.AI;

public class GhostFootsteps : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip[] sonidosPasos;
    public AudioSource audioSource;

    [Header("Configuración")]
    public float velocidadMinima = 0.3f;
    public float intervaloBase = 0.5f; // segundos entre pasos a velocidad 1 m/s

    private NavMeshAgent agent;
    private float timer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!agent.enabled || sonidosPasos.Length == 0) return;

        float velocidad = agent.velocity.magnitude;
        if (velocidad < velocidadMinima)
        {
            timer = 0f;
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            AudioClip clip = sonidosPasos[Random.Range(0, sonidosPasos.Length)];
            audioSource.PlayOneShot(clip);
            timer = intervaloBase / velocidad;
        }
    }
}
