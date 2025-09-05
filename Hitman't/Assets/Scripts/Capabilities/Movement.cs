using System;
using UnityEngine;

[RequireComponent(typeof(Controls), typeof(CollisionDataRetriever))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5;  
    [SerializeField] private float _maxSpeed = 7;  
    [SerializeField] private float _groundDragSpeed = 0.9f;
    [SerializeField] private float _forceMultiplier = 10f;
    [SerializeField] private Transform _orientation; 
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
    }
    
    void Update()
    {
        _moveInput = _controls.input.RetrieveMoveInput();
        _moveDirection = _orientation.forward * _moveInput.y + _orientation.right * _moveInput.x;
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
