using System;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerObj;
    [SerializeField] private Rigidbody _rb;
    private Controls _controller;

    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        _controller = GetComponent<Controls>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        
        Vector3 viewDirection = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDirection.normalized;

        Vector2 input = _controller.input.RetrieveMoveInput();
        Vector3 inputDirection = _orientation.forward * input.y + _orientation.right * input.x;
        
        if(inputDirection != Vector3.zero)
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
    }
}
