using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    public float normalSpeed = 3.5f;
    public float crawlSpeed = 0.8f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("speed", speed, 0.1f, Time.deltaTime);
    }

    // Appelle ces méthodes depuis un trigger collider
    public void StartCrawl()
    {
        animator.SetBool("isCrawling", true);
        agent.speed = crawlSpeed;
    }

    public void StopCrawl()
    {
        animator.SetBool("isCrawling", false);
        agent.speed = normalSpeed;
    }
}