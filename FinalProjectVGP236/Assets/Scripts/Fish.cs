using UnityEngine;

public class Fish : MonoBehaviour
{
    public string fishName = "Basic Fish";
    public int value = 10;
    public float swimSpeed = 2f;

    public float wiggleFrequency = 4f;
    public float wiggleAngle = 10f;

    public bool canDealDamage = false;
    public float pushStrength = 0.3f;

    public FishSpawner spawner;

    float timeOffset;
    int direction = 1;
    bool alreadyCollected = false;
    float originalScaleX;

    void Start()
    {
        timeOffset = Random.Range(0f, 10f);
        originalScaleX = transform.localScale.x;
        ApplyFacing();
    }

    void Update()
    {
        Swim();
        Wiggle();
    }

    void Swim()
    {
        transform.Translate(Vector2.right * swimSpeed * direction * Time.deltaTime);
        ApplyFacing();
    }

    void Wiggle()
    {
        float angle = Mathf.Sin((Time.time + timeOffset) * wiggleFrequency) * wiggleAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle * direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            direction *= -1;
            ApplyFacing();
        }

        if (other.CompareTag("Player"))
        {
            Vector3 push = (transform.position - other.transform.position).normalized * pushStrength;
            transform.position += push;

            if (canDealDamage)
            {
                LivesManager.instance.LoseLife();
            }
            else
            {
                CatchFish();
            }
        }
    }

    public void CatchFish()
    {
        if (alreadyCollected)
            return;

        alreadyCollected = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            FishInventory inv = player.GetComponent<FishInventory>();
            if (inv != null)
                inv.AddFish();
        }
    }

    public void KillFish()
    {
        if (spawner != null)
            spawner.FishDied();

        Destroy(gameObject);
    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    void ApplyFacing()
    {
        Vector3 scale = transform.localScale;
        scale.x = originalScaleX * -direction;
        transform.localScale = scale;
    }
}
