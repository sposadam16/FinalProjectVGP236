using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Fish Stats")]
    public string fishName = "Basic Fish";
    public int value = 10;               
    public float swimSpeed = 2f;         
    public float wiggleAmplitude = 0.2f; 
    public float wiggleFrequency = 2f;   

    private Vector3 startPos;
    private float timeOffset;

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
        transform.Translate(Vector2.right * swimSpeed * Time.deltaTime);
        float yOffset = Mathf.Sin((Time.time + timeOffset) * wiggleFrequency) * wiggleAmplitude;
        transform.position = new Vector3(transform.position.x, startPos.y + yOffset, transform.position.z);
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
