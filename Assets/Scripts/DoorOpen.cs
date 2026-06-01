using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private bool abierta = false;

    [Header("Sonido")]
    public AudioClip sonidoPuerta;
    public float duracionSonido = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 1f;
            audioSource.clip = sonidoPuerta;
        }
    }

    public void OnSelect()
    {
        abierta = !abierta;
        animator.SetBool("Abierta", abierta);

        if (audioSource != null && sonidoPuerta != null)
        {
            audioSource.Stop();
            audioSource.time = 0f;
            audioSource.Play();
            Invoke("PararSonido", duracionSonido);
        }

        Debug.Log("Puerta " + (abierta ? "abierta" : "cerrada"));
    }

    void PararSonido()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}