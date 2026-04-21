using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class WaypointAgent : MonoBehaviour
{
    public Transform[] waypoints;
    public float stopDistance = 0.8f;
    public float pauseDuration = 1.5f;

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

        // Déclenche l'animation d'arrivée
        if (animator != null)
            animator.SetTrigger("arrived");

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