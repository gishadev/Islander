using UnityEngine;

public class FPSMover : MonoBehaviour
{
    [SerializeField] private float playerHeight = 2f;

    [Header("Movement")] [SerializeField] float moveSpeed = 6f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float waterMultiplier = 1f;

    [Header("Sprinting")] [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float acceleration = 10f;

    [Header("Jumping")] [SerializeField]
    private float jumpForce = 5f;

    [Header("Keybindings")] [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Drag")] [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float waterDrag = 10f;

    [Header("Ground Detection")] [SerializeField]
    private Transform groundCheck;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;

    private bool IsGrounded { get; set; }
    private bool IsSwimming { get; set; }

    private float _xInput, _yInput, _zInput;

    private Vector3 _moveDirection, _slopeMoveDirection;
    private RaycastHit _slopeHit;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        IsGrounded = CheckGround();
        IsSwimming = CheckSwimming();

        if (!IsSwimming)
            ApplyBasicSettings();
        else
            ApplyWaterSettings();

        GetInput();
        ControlDrag();
        ControlSpeed();

        if (!IsSwimming)
        {
            if (Input.GetKeyDown(jumpKey) && IsGrounded)
                Jump();
            _slopeMoveDirection = Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _zInput = Input.GetAxisRaw("Vertical");
        _yInput = 0f;

        if (IsSwimming)
        {
            if (Input.GetKey(jumpKey))
                _yInput = 1f;
            else if (Input.GetKey(crouchKey))
                _yInput = -1f;
            else
                _yInput = -0.5f;
        }

        _moveDirection = transform.forward * _zInput + transform.right * _xInput + transform.up * _yInput;
    }

    private void ApplyBasicSettings()
    {
        _rb.useGravity = true;
    }

    private void ApplyWaterSettings()
    {
        _rb.useGravity = false;
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
        if (IsSwimming)
            _rb.drag = waterDrag;
        else if (IsGrounded && !IsSwimming)
            _rb.drag = groundDrag;
        else if (!IsGrounded && !IsSwimming)
            _rb.drag = airDrag;
    }

    private void MovePlayer()
    {
        bool isOnSlope = CheckSlope();
        if (IsSwimming)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * movementMultiplier * waterMultiplier,
                ForceMode.Acceleration);
        else if (!IsSwimming && IsGrounded && !isOnSlope)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        else if (!IsSwimming && IsGrounded && isOnSlope)
            _rb.AddForce(_slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        else if (!IsSwimming && !IsGrounded)
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

    private bool CheckGround()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private bool CheckSwimming()
    {
        return transform.position.y < 0f;
    }
}