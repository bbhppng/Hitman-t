using UnityEngine;

[RequireComponent(typeof(Controls), typeof(CollisionDataRetriever))]
public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 2.5f;
    private Rigidbody _rb;
    private Controls _controls;
    private CollisionDataRetriever _collisionDataRetriever;
    private bool _jumpRequested;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = GetComponent<Controls>();
        _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
    }

    private void Update()
    {
        _jumpRequested = _controls.input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        bool onGround = _collisionDataRetriever.OnGround;
        if (onGround && _jumpRequested)
        {
            Vector3 velocity = _rb.linearVelocity;
            _rb.linearVelocity = new Vector3(velocity.x, 0f, velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}
