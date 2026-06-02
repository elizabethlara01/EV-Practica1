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

        float distancia = Vector3.Distance(transform.position, centerEyeAnchor.position);
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

        // Usar la dirección del fantasma hacia el jugador (funciona en simulador y VR real)
        Vector3 direccionAlJugador = (centerEyeAnchor.position - transform.position).normalized;
        direccionAlJugador.y = 0f;

        // Posición delante del jugador, a la altura de los ojos
        Vector3 nuevaPosicion = centerEyeAnchor.position + direccionAlJugador * distanciaFrontal;
        nuevaPosicion.y = centerEyeAnchor.position.y - 0.3f; // ligero ajuste para que la cara quede centrada

        transform.position = nuevaPosicion;

        // El fantasma mira al jugador
        transform.LookAt(new Vector3(centerEyeAnchor.position.x, nuevaPosicion.y, centerEyeAnchor.position.z));

        animator.CrossFade("agatha_RIG_skeleton|jumpscare3", 0.05f);
        jumpscareUI.MostrarJumpscare();

        yield return new WaitForSeconds(2.8f);

        jumpscareUI.OcultarJumpscare();
        agent.enabled = true;
        ghostDetection.jumpscareActivo = false;
        ghostController.ReanudarMovimiento();
    }
}