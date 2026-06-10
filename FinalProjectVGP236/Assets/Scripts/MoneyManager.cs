using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int money = 0;
    public TopRightUI ui;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Invoke(nameof(UpdateMoneyUI), 0.05f);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }
    void UpdateMoneyUI()
    {
        if (ui == null)
        {
            Debug.LogWarning("MoneyManager: UI reference missing!");
            return;
        }

        ui.SetText("Cash: " + money);
    }
}
