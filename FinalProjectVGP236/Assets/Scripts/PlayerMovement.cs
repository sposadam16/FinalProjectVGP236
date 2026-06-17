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

    private Animator _animator;


    private Rigidbody2D _rigidbody = null;
    private InputSystem_Actions _playerInput = null;
    private InputAction _moveAction = null;

    private Vector2 _moveInput = Vector2.zero;

    private PlayerState _currentState = PlayerState.OnBoat;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

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

        if (_moveInput.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_moveInput.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
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

        if (_currentState == PlayerState.Swimming)
        {
            _rigidbody.linearVelocity =
                _moveInput * _moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            _currentState = PlayerState.Swimming;

            _rigidbody.gravityScale = 0.0f;
            _animator.SetBool("IsSwimming", true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            _currentState = PlayerState.OnBoat;

            _rigidbody.gravityScale = 1.0f;
            _animator.SetBool("IsSwimming", false);


        }
    }
}