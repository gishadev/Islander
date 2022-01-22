using UnityEngine;

public class FPSMover : MonoBehaviour
{
    [SerializeField] private float playerHeight = 2f;

    [Header("Movement")] [SerializeField] float moveSpeed = 6f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float movementMultiplier = 10f;

    [Header("Sprinting")] [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float acceleration = 10f;

    [Header("Jumping")] [SerializeField] private float jumpForce = 5f;

    [Header("Keybinds")] [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")] [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    [Header("Ground Detection")] [SerializeField]
    private Transform groundCheck;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;

    private bool IsGrounded { get; set; }

    private float _horizontalMovement;
    private float _verticalMovement;

    private Vector3 _moveDirection;
    private Vector3 _slopeMoveDirection;
    private RaycastHit _slopeHit;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        GetInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && IsGrounded)
            Jump();

        _slopeMoveDirection = Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        _moveDirection = transform.forward * _verticalMovement + transform.right * _horizontalMovement;
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && IsGrounded)
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        else
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
    }

    private void ControlDrag()
    {
        if (IsGrounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = airDrag;
    }

    void MovePlayer()
    {
        if (IsGrounded && !CheckSlope())
            _rb.AddForce(_moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        else if (IsGrounded && CheckSlope())
            _rb.AddForce(_slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        else if (!IsGrounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier,
                ForceMode.Acceleration);
    }

    private bool CheckSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, playerHeight / 2 + 0.5f))
        {
            if (_slopeHit.normal != Vector3.up)
                return true;
        }

        return false;
    }
}