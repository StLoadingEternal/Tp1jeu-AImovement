using UnityEngine;

using UnityEngine;
using UnityEngine.AI;

public class SpeedZone : MonoBehaviour
{
    public float slowSpeed = 1.5f;
    public float normalSpeed = 3.5f;

    void OnTriggerEnter(Collider other)
    {
        NavMeshAgent ag = other.GetComponent<NavMeshAgent>();
        if (ag != null) ag.speed = slowSpeed;
    }

    void OnTriggerExit(Collider other)
    {
        NavMeshAgent ag = other.GetComponent<NavMeshAgent>();
        if (ag != null) ag.speed = normalSpeed;
    }
}