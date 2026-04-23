using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class WaypointAgent : MonoBehaviour
{
    public Transform[] waypoints;
    public float stopDistance = 0.8f;
    public float pauseDuration = 1.5f;

    [Header("Vitesse")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;
    public float walkDistanceThreshold = 3f; // distance pour passer en marche

    private NavMeshAgent agent;
    private Animator animator;
    private int currentTarget = -1;
    private bool waiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.stoppingDistance = stopDistance;
        GoToNext();
    }

    void Update()
    {
        if (waiting) return;

        // Adapter la vitesse selon la distance au waypoint
        if (currentTarget >= 0 && currentTarget < waypoints.Length)
        {
            float distanceToTarget = Vector3.Distance(
                transform.position,
                waypoints[currentTarget].position
            );

            if (distanceToTarget <= walkDistanceThreshold)
                agent.speed = walkSpeed; // proche → marche
            else
                agent.speed = runSpeed;  // loin → course
        }

        if (!agent.pathPending &&
            agent.remainingDistance <= stopDistance)
        {
            StartCoroutine(PauseAndGoNext());
        }
    }

    IEnumerator PauseAndGoNext()
    {
        waiting = true;
        agent.isStopped = true;

        if (animator != null)
        {
            animator.ResetTrigger("Arrived");
            animator.SetTrigger("Arrived");
        }

        yield return new WaitForSeconds(pauseDuration);

        agent.isStopped = false;
        waiting = false;
        GoToNext();
    }

    void GoToNext()
    {
        if (waypoints.Length == 0) return;
        int next;
        do {
            next = Random.Range(0, waypoints.Length);
        } while (next == currentTarget && waypoints.Length > 1);
        currentTarget = next;
        agent.SetDestination(waypoints[currentTarget].position);
    }
}