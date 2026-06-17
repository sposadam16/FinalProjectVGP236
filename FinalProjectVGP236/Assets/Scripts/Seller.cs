using UnityEngine;
using UnityEngine.InputSystem;

public class Seller : MonoBehaviour
{
    [SerializeField]
    private MoneyManager _moneyManager = null;

    private FishInventory _playerInventory = null;
    private bool _playerNearby = false;

    private void Update()
    {
        if (_playerNearby &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            int fishSold =
                _playerInventory.SellAllFish();

            _moneyManager.AddMoney(fishSold * 100);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInventory =
                collision.GetComponent<FishInventory>();

            _playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerNearby = false;
        }
    }
}