using UnityEngine;

public class FishFlee : MonoBehaviour
{
    [Header("Flee Settings")]
    public float detectionRange = 3f;
    public float fleeSpeedMultiplier = 2f;
    public float turnSpeed = 5f;

    private Transform player;
    private Fish fish;
    private bool isFleeing = false;
    private float originalSpeed;

    void Start()
    {
        // Try to find player, but don't crash if missing
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        fish = GetComponent<Fish>();
        originalSpeed = fish.swimSpeed;
    }

    void Update()
    {
        // If player doesn't exist yet, do nothing
        if (player == null)
        {
            TryFindPlayerAgain();
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange)
            FleeFromPlayer();
        else
            StopFleeing();
    }

    void TryFindPlayerAgain()
    {
        // This lets the fish detect the player later when you add it
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void FleeFromPlayer()
    {
        if (!isFleeing)
        {
            isFleeing = true;
            fish.swimSpeed = originalSpeed * fleeSpeedMultiplier;
        }

        Vector2 fleeDirection = (transform.position - player.position).normalized;

        Vector3 newDir = Vector3.Lerp(transform.right, fleeDirection, Time.deltaTime * turnSpeed);
        transform.right = newDir;
    }

    void StopFleeing()
    {
        if (!isFleeing) return;

        isFleeing = false;
        fish.swimSpeed = originalSpeed;
    }
}
