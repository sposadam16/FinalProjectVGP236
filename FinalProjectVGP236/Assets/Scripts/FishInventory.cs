using UnityEngine;

public class FishInventory : MonoBehaviour
{
    public int fishCount = 0;

    public void AddFish()
    {
        fishCount++;
    }

    public int SellAllFish()
    {
        int sold = fishCount;
        fishCount = 0;
        return sold;
    }
}
