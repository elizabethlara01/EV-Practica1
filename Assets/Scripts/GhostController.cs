using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    [Header("Velocidades según ecos activos")]
    public float speed3Echoes = 5f;   // muy rápido
    public float speed2Echoes = 3.5f;
    public float speed1Echo = 2f;
    public float speed0Echoes = 0.5f; // casi no aparece

    [Header("Patrulla")]
    public Transform[] patrolPoints;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    [Header("Comportamiento en patrol points")]
    public float lookAroundTime = 3f;      // segundos mirando alrededor
    public float lookAroundSpeed = 60f;    // grados por segundo al rotar

    private bool isLookingAround = false;
    private float lookAroundTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);
        GoToNextPatrolPoint();
    }

    void Update()
    {
        UpdateSpeed();
        float targetSpeed = agent.velocity.magnitude;
        float currentSpeed = animator.GetFloat("Speed");
        animator.SetFloat("Speed", Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f));

        if (!isChasing)
        {
            if (isLookingAround)
            {
                // Rota lentamente mientras espera
                transform.Rotate(0, lookAroundSpeed * Time.deltaTime, 0);
                lookAroundTimer -= Time.deltaTime;

                if (lookAroundTimer <= 0f)
                {
                    isLookingAround = false;
                    GoToNextPatrolPoint();
                }
            }
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                // Ha llegado al punto — empieza a mirar alrededor
                isLookingAround = true;
                lookAroundTimer = lookAroundTime;
                agent.ResetPath(); // se detiene
            }
        }
    }

    void UpdateSpeed()
    {
        int echoes = EchoManager.Instance.echoesAlive;
        switch (echoes)
        {
            case 3: agent.speed = speed3Echoes; break;
            case 2: agent.speed = speed2Echoes; break;
            case 1: agent.speed = speed1Echo; break;
            case 0: agent.speed = speed0Echoes; break;
        }
    }

    public void ChasePlayer(Vector3 playerPos)
    {
        isChasing = true;
        agent.SetDestination(playerPos);
    }

    public void Patrol()
    {
        isChasing = false;
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    public void PausarMovimiento()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        isChasing = false;
        isLookingAround = false;
    }

    public void ReanudarMovimiento()
    {
        agent.isStopped = false;
        GoToNextPatrolPoint();
    }
}