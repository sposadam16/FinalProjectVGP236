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
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogError("FishFlee: No object with tag 'Player' found in the scene!");
        }

        fish = GetComponent<Fish>();
        originalSpeed = fish.swimSpeed;
    }
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            FleeFromPlayer();
        }
        else
        {
            StopFleeing();
        }
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
