using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    OnBoat,
    Swimming
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5.0f;

    private Rigidbody2D _rigidbody = null;
    private InputSystem_Actions _playerInput = null;
    private InputAction _moveAction = null;

    private Vector2 _moveInput = Vector2.zero;

    private PlayerState _currentState = PlayerState.OnBoat; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _playerInput = new InputSystem_Actions();
        _moveAction = _playerInput.Player.Move;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_currentState == PlayerState.OnBoat)
        {
            _rigidbody.linearVelocity =
        new Vector2(
        _moveInput.x * _moveSpeed,
        _rigidbody.linearVelocity.y);
        }
    }
}