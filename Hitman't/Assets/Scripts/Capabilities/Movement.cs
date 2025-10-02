using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Controls), typeof(CollisionDataRetriever))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5;  
    [SerializeField] private float _maxSpeed = 7;  
    [SerializeField] private float _groundDragSpeed = 0.9f;
    [SerializeField] private float _forceMultiplier = 10f;
    [SerializeField] private Transform _cameraTransform;
    private Rigidbody _rb;
    
    private Controls _controls;
    private CollisionDataRetriever _collisionDataRetriever;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = GetComponent<Controls>();
        _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
        if (_cameraTransform == null && Camera.main != null)
            _cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        _moveInput = _controls.input.RetrieveMoveInput();
        Vector3 camForward = _cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = _cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        _moveDirection = camForward * _moveInput.y + camRight * _moveInput.x;
    }

    private void FixedUpdate()
    {
        Debug.Log(_rb.linearVelocity.magnitude);
        bool onGround = _collisionDataRetriever.OnGround;
        if (_moveInput.magnitude > 0.01f)
        {
            _rb.AddForce(_moveDirection.normalized * _speed * _forceMultiplier, ForceMode.Force);
        }

        if (onGround && _moveInput.magnitude <= 0.01f)
        {
            Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            horizontalVelocity *= _groundDragSpeed;
            _rb.linearVelocity = new Vector3(horizontalVelocity.x, _rb.linearVelocity.y, horizontalVelocity.z);
        }

        Vector3 currentHorizontal = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        if (currentHorizontal.magnitude > _maxSpeed)
        {
            currentHorizontal = currentHorizontal.normalized * _maxSpeed;
            _rb.linearVelocity = new Vector3(currentHorizontal.x, _rb.linearVelocity.y, currentHorizontal.z);
        }
    }
}
