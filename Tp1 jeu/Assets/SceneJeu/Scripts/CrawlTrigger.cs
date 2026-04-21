using UnityEngine;

public class CrawlTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag("Player")) return; // ignore tout sauf le joueur

    AnimationController ac = other.GetComponent<AnimationController>();
    if (ac != null) ac.StartCrawl();
}

void OnTriggerExit(Collider other)
{
    if (!other.CompareTag("Player")) return;

    AnimationController ac = other.GetComponent<AnimationController>();
    if (ac != null) ac.StopCrawl();
}
}