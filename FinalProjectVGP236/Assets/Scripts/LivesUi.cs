using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LivesUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public LivesManager livesManager;

    private List<Image> hearts = new List<Image>();

    void Start()
    {
        if (livesManager == null)
        {
            Debug.LogError("LivesUI: livesManager is not assigned!");
            return;
        }

        CreateHearts();
        UpdateHearts();
    }

    void CreateHearts()
    {
        // clear old hearts just in case
        foreach (var h in hearts)
            Destroy(h.gameObject);
        hearts.Clear();

        for (int i = 0; i < livesManager.maxLives; i++)
        {
            GameObject h = Instantiate(heartPrefab, transform);
            Image img = h.GetComponent<Image>();
            hearts.Add(img);
        }
    }

    public void UpdateHearts()
    {
        if (hearts.Count == 0)
        {
            Debug.LogWarning("LivesUI: No hearts created. Did CreateHearts() run?");
            return;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = (i < livesManager.lives) ? fullHeart : emptyHeart;
        }
    }
}
