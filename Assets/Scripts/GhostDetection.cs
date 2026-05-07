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

    void Start()
    {
        ghostController = GetComponent<GhostController>();
    }

    void Update()
    {
        if (CanSeePlayer())
            ghostController.ChasePlayer(player.position);
        else
            ghostController.Patrol();
    }

    bool CanSeePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Siempre detecta si está muy cerca
        if (distance < alwaysDetectRadius) return true;

        // Fuera del radio general → no ve
        if (distance > detectionRadius) return false;

        // Comprueba ángulo
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > fieldOfViewAngle / 2f) return false;

        // Comprueba que no haya pared por delante (raycast)
        if (Physics.Raycast(transform.position, dirToPlayer, distance, obstacleMask))
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