using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float rotationSmooth = 8f;
    public float positionSmooth = 6f;

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;

        // Direction de déplacement de l'agent
        UnityEngine.AI.NavMeshAgent agent = target.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 moveDir = Vector3.zero;

        if (agent != null && agent.velocity.sqrMagnitude > 0.1f)
            moveDir = agent.velocity.normalized;
        else
            moveDir = target.forward;

        // Position cible : derrière le joueur selon sa direction
        Vector3 desiredPos = target.position
                           - moveDir * distance
                           + Vector3.up * height;

        transform.position = Vector3.SmoothDamp(
            transform.position, desiredPos,
            ref currentVelocity, 1f / positionSmooth);

        // Regarde le joueur
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }
}