using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public int maxFish = 10;

    BoxCollider2D area;
    int currentFish = 0;

    void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        for (int i = 0; i < maxFish; i++)
            SpawnFish();
    }

    public void FishDied()
    {
        currentFish--;
        SpawnFish();
    }

    void SpawnFish()
    {
        if (currentFish >= maxFish)
            return;

        Vector2 spawnPos = GetRandomPointInArea();
        int index = Random.Range(0, fishPrefabs.Length);

        GameObject fish = Instantiate(fishPrefabs[index], spawnPos, Quaternion.identity);
        Fish f = fish.GetComponent<Fish>();
        if (f != null)
        {
            f.SetDirection(Random.value > 0.5f ? 1 : -1);
            f.spawner = this;
        }

        currentFish++;
    }

    Vector2 GetRandomPointInArea()
    {
        Vector2 center = area.bounds.center;
        Vector2 size = area.bounds.size;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(x, y);
    }
}
