using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class FPSMover : MonoBehaviourPun
    {
        [SerializeField] private float playerHeight = 2f;

        [Header("Movement")] [SerializeField] float moveSpeed = 6f;
        [SerializeField] private float airMultiplier = 0.4f;
        [SerializeField] private float movementMultiplier = 10f;
        [SerializeField] private float waterMultiplier = 1f;

        [Header("Sprinting")] [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float sprintSpeed = 6f;
        [SerializeField] private float acceleration = 10f;

        [Header("Jumping")] [SerializeField] private float jumpForce = 5f;

        [Header("Keybindings")] [SerializeField]
        private KeyCode jumpKey = KeyCode.Space;

        [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

        [Header("Drag")] [SerializeField] private float groundDrag = 6f;
        [SerializeField] private float airDrag = 2f;
        [SerializeField] private float waterDrag = 10f;

        [Header("Ground Detection")] [SerializeField]
        private Transform groundCheck;

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundDistance = 0.2f;
        [SerializeField] private float groundDistanceWaterMultiplier = 2f;

        private bool IsGrounded { get; set; }
        public bool IsSwimming { get; private set; }

        private float _xInput, _yInput, _zInput;

        private Vector3 _moveDirection;
        private RaycastHit _slopeHit;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
        }

        private void Update()
        {
            if (!photonView.IsMine)
                return;

            IsGrounded = CheckGround();
            IsSwimming = CheckSwimming();

            if (!IsSwimming)
                ApplyBasicSettings();
            else
                ApplyWaterSettings();

            GetInput(out var inputMoveDirection);
            ControlDrag();
            ControlSpeed();

            if (Input.GetKeyDown(jumpKey) && IsGrounded && !IsSwimming)
                Jump();

            // Setting move direction according to slopes under the player.
            if (CheckForSlope() && IsGrounded)
                _moveDirection = Vector3.ProjectOnPlane(inputMoveDirection, _slopeHit.normal);
            else
                _moveDirection = inputMoveDirection;
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine)
                return;
            
            MovePlayer();
        }

        private void GetInput(out Vector3 inputMoveDirection)
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

            inputMoveDirection = transform.forward * _zInput + transform.right * _xInput + transform.up * _yInput;
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
            float envMultiplier;
            if (!IsGrounded && !IsSwimming)
                envMultiplier = airMultiplier;
            else if (!IsGrounded && IsSwimming)
                envMultiplier = waterMultiplier;
            else
                envMultiplier = 1f;

            _rb.AddForce(_moveDirection.normalized * moveSpeed * movementMultiplier * envMultiplier,
                ForceMode.Acceleration);
        }

        private bool CheckForSlope()
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
            float dst = IsSwimming ? groundDistance * groundDistanceWaterMultiplier : groundDistance;
            return Physics.CheckSphere(groundCheck.position, dst, groundMask);
        }

        private bool CheckSwimming()
        {
            return transform.position.y < 0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _moveDirection * 2f);
        }
    }
}