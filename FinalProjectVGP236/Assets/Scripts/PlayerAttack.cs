using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    private Transform _attackPoint = null;

    [SerializeField]
    private float _attackDistance = 0.5f;
    [SerializeField]
    private float _attackRadius = 0.5f;

    private InputSystem_Actions _playerInput = null;
    private InputAction _attackAction = null;

    private void Awake()
    {
        _playerInput = new InputSystem_Actions();

        _attackAction = _playerInput.Player.Attack;
    }

    private void OnEnable()
    {
        _attackAction.Enable();

        _attackAction.performed += OnAttack;
    }

    private void OnDisable()
    {
        _attackAction.performed -= OnAttack;

        _attackAction.Disable();
    }

    private void Update()
    {
        RotateAttackPoint();
    }

    private void RotateAttackPoint()
    {
        Vector3 mousePosition =
            Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mousePosition.z = 0.0f;

        Vector2 direction =
            (mousePosition - transform.position).normalized;

        _attackPoint.position =
            transform.position +
            (Vector3)(direction * _attackDistance);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("STAB!");

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            _attackPoint.position,
            _attackRadius);

        foreach (Collider2D hit in hits)
        {
            Fish fish = hit.GetComponent<Fish>();

            if (fish != null)
            {
                fish.CatchFish();

                Debug.Log("Fish Captured!");

                break;
            }
        }
    }
}