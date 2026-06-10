using UnityEngine;
using TMPro;

public class TopRightUI : MonoBehaviour
{
    private TextMeshProUGUI textUI;

    void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();

        if (textUI == null)
            Debug.LogError("TopRightUI: No TextMeshProUGUI found on this object!");
    }
    public void SetText(string newText)
    {
        if (textUI == null)
            return;

        textUI.text = newText;
    }
}
