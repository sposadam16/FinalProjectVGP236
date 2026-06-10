using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Fish Stats")]
    public string fishName = "Basic Fish";
    public int value = 10;               // Money 
    public float swimSpeed = 2f;        
    public float wiggleAmplitude = 0.2f; // Movement
    public float wiggleFrequency = 2f;   // Speed

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
    public int CatchFish()
    {
        Destroy(gameObject);
        return value;
    }
}