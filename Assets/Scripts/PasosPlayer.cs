using UnityEngine;
using UnityEngine.InputSystem;

public class PasosJugador : MonoBehaviour
{
    public AudioClip[] sonidosPasos;
    public float tiempoEntrePasos = 0.5f;
    
    private AudioSource audioSource;
    private float tiempoUltimoPaso = 0f;
    private CharacterController characterController;

    private Vector3 posicionAnterior;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.volume = 2f;
        characterController = GetComponent<CharacterController>();

        posicionAnterior = transform.position;
    }

    void Update()
    {
        // Detecta si el jugador se está moviendo
        //bool moviendose = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).magnitude > 0.1f || OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).magnitude > 0.1f;
        Vector3 movimiento = transform.position - posicionAnterior;
        bool moviendose = movimiento.magnitude > 0.005f;
        posicionAnterior = transform.position;

        //Debug.Log("Moviendose: " + moviendose);
        if (moviendose && Time.time - tiempoUltimoPaso >= tiempoEntrePasos)
        {
            //Debug.Log("Reproduciendo paso");
            tiempoUltimoPaso = Time.time;
            int indice = Random.Range(0, sonidosPasos.Length);
            audioSource.PlayOneShot( sonidosPasos[indice]);
            audioSource.Play();
        }
    }
}