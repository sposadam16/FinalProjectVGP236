using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Fish Stats")]
    public string fishName = "Basic Fish";
    public int value = 10;
    public float swimSpeed = 2f;
    public float wiggleAmplitude = 0.2f;
    public float wiggleFrequency = 2f;

    [Header("Damage Settings")]
    public bool canDealDamage = false;
    public float pushStrength = 0.3f;

    private Vector3 startPos;
    private float timeOffset;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        Swim();
    }

    void Swim()
    {
        transform.Translate(Vector2.right * swimSpeed * direction * Time.deltaTime);

        float yOffset = Mathf.Sin((Time.time + timeOffset) * wiggleFrequency) * wiggleAmplitude;
        transform.position = new Vector3(transform.position.x, startPos.y + yOffset, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            direction *= -1;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (other.CompareTag("Player"))
        {
            Vector3 push = (transform.position - other.transform.position).normalized * pushStrength;
            transform.position += push;

            if (canDealDamage)
            {
                LivesManager.instance.LoseLife();   
            }

            CatchFish();
        }
    }

    public void CatchFish()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            FishInventory inv = player.GetComponent<FishInventory>();
            if (inv != null)
                inv.AddFish();
        }

        Destroy(gameObject);
    }
}
