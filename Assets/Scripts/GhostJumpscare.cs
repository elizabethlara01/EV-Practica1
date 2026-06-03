using System.Collections;
using UnityEngine;

public class GhostJumpscare : MonoBehaviour
{
    [Header("Referencias")]
    public Transform centerEyeAnchor;
    public Animator animator;
    public JumpscareUI jumpscareUI;

    [Header("Configuración")]
    public float distanciaActivacion = 1f;
    public float distanciaFrontal = 0.8f;
    public float alturaCaraDesdeRaiz = 1.6f; // ajustar según el modelo del fantasma

    private bool jumpscareActivado = false;
    private GhostController ghostController;
    private GhostDetection ghostDetection;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        ghostController = GetComponent<GhostController>();
        ghostDetection = GetComponent<GhostDetection>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (jumpscareActivado) return;

        // Distancia solo en XZ: el fantasma está al nivel del suelo y el eye anchor está a la altura de la cabeza
        Vector2 ghostXZ = new(transform.position.x, transform.position.z);
        Vector2 anchorXZ = new(centerEyeAnchor.position.x, centerEyeAnchor.position.z);
        float distancia = Vector2.Distance(ghostXZ, anchorXZ);
        if (distancia <= distanciaActivacion)
        {
            StartCoroutine(EjecutarJumpscare());
        }
    }

    IEnumerator EjecutarJumpscare()
    {
        jumpscareActivado = true;
        ghostDetection.jumpscareActivo = true;

        agent.isStopped = true;
        agent.enabled = false;

        // Dirección horizontal del fantasma hacia el jugador
        Vector3 direccionAlJugador = centerEyeAnchor.position - transform.position;
        direccionAlJugador.y = 0f;
        direccionAlJugador.Normalize();

        // Colocar el fantasma delante del jugador (en la dirección desde la que viene)
        // y bajar el root para que la cara quede a la altura de los ojos
        Vector3 nuevaPosicion = centerEyeAnchor.position - direccionAlJugador * distanciaFrontal;
        nuevaPosicion.y = centerEyeAnchor.position.y - alturaCaraDesdeRaiz;

        transform.position = nuevaPosicion;

        // El fantasma mira al jugador
        transform.LookAt(new Vector3(centerEyeAnchor.position.x, nuevaPosicion.y, centerEyeAnchor.position.z));

        animator.CrossFade("agatha_RIG_skeleton|jumpscare3", 0.05f);
        jumpscareUI.MostrarJumpscare();

        yield return new WaitForSeconds(2.8f);

        jumpscareUI.OcultarJumpscare();
        agent.enabled = true;
        ghostDetection.jumpscareActivo = false;
        animator.CrossFade("agatha_RIG_skeleton|idle", 0.25f);
        ghostController.ReanudarMovimiento();
    }
}