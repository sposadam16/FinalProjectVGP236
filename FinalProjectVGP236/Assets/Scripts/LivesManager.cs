using UnityEngine;
using System.Collections;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;

    public int maxLives = 3;
    public int lives = 3;

    public LivesUI ui;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart()
    {
        yield return null; 
        if (ui != null)
            ui.UpdateHearts();
        else
            Debug.LogError("LivesManager: ui is not assigned!");
    }
    public void LoseLife()
    {
        lives--;
        if (lives < 0) lives = 0;
        ui.UpdateHearts();
    }

    public void AddLife()
    {
        lives++;
        if (lives > maxLives) lives = maxLives;
        ui.UpdateHearts();
    }
}
