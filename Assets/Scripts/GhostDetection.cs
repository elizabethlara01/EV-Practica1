using UnityEngine;

public class GhostDetection : MonoBehaviour
{
    [Header("Distancia")]
    public float detectionRadius = 10f;     // radio general
    public float alwaysDetectRadius = 2f;   // si estás aquí, siempre te ve

    [Header("Campo de visión")]
    public float fieldOfViewAngle = 90f;    // ángulo total del cono
    public LayerMask obstacleMask;          // paredes que bloquean la vista

    public Transform player;
    private GhostController ghostController;

    [HideInInspector] public bool jumpscareActivo = false;

    void Start()
    {
        ghostController = GetComponent<GhostController>();
        player = GetComponent<GhostJumpscare>()?.centerEyeAnchor;
    }

    void Update()
    {
        if (jumpscareActivo) return; // No detecta durante el jumpscare

        if (CanSeePlayer())
            ghostController.ChasePlayer(player.position);
        else
            ghostController.Patrol();
    }

    bool CanSeePlayer()
    {
        // Todos los cálculos en XZ: el fantasma camina en el suelo y el jugador está a distinta altura
        Vector3 ghostFlat  = new(transform.position.x, 0f, transform.position.z);
        Vector3 playerFlat = new(player.position.x,    0f, player.position.z);

        float distance = Vector3.Distance(ghostFlat, playerFlat);

        // Siempre detecta si está muy cerca
        if (distance < alwaysDetectRadius) return true;

        // Fuera del radio general → no ve
        if (distance > detectionRadius) return false;

        // Comprueba ángulo en horizontal
        Vector3 dirToPlayer  = (playerFlat - ghostFlat).normalized;
        Vector3 forwardFlat  = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        float angle = Vector3.Angle(forwardFlat, dirToPlayer);
        if (angle > fieldOfViewAngle / 2f) return false;

        // Raycast en 3D real para respetar obstáculos físicos
        Vector3 dir3D  = (player.position - transform.position).normalized;
        float   dist3D = Vector3.Distance(transform.position, player.position);
        if (Physics.Raycast(transform.position, dir3D, dist3D, obstacleMask))
            return false;

        return true;
    }

    // Visualizar el cono en el editor (muy útil para debuggear)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alwaysDetectRadius);
    }
}