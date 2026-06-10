using UnityEngine;

public class FishFlee : MonoBehaviour
{
    [Header("Flee Settings")]
    public float detectionRange = 3f;   // How close the player must be
    public float fleeSpeedMultiplier = 2f; // How much faster the fish swims when fleeing

    private Transform player;
    private Fish fish;
    private bool isFleeing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fish = GetComponent<Fish>();
    }
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            StartFleeing();
        }
        else
        {
            StopFleeing();
        }
    }
    void StartFleeing()
    {
        if (isFleeing) return;

        isFleeing = true;
        fish.swimSpeed *= fleeSpeedMultiplier;

        // Flip direction AWAY from player
        Vector2 direction = (transform.position - player.position).normalized;
        transform.right = direction;
    }

    void StopFleeing()
    {
        if (!isFleeing) return;

        isFleeing = false;
        fish.swimSpeed /= fleeSpeedMultiplier;
    }
}
