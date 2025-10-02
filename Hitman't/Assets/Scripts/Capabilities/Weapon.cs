using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private float _projectileSpeed = 30;
    [SerializeField] private float _projectileLifeTime = 3f;
    [SerializeField] private Transform _playerCamera;
    
    private Controls _controls;
    private bool _shotRequested;

    private void Awake()
    {
        _controls = GetComponent<Controls>();
    }
    
    void Update()
    {
        _shotRequested = _controls.input.RetrieveWeaponInput();
    }

    void FixedUpdate()
    {
        if (_shotRequested)
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        Camera cam = Camera.main;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            targetPoint = hit.point;  
        }
        else
        {
            targetPoint = cam.transform.position + cam.transform.forward * 1000f; 
        }

        Vector3 direction = (targetPoint - _projectileSpawn.position).normalized;
        Quaternion spawnRot = Quaternion.LookRotation(direction);

        GameObject projectile = Instantiate(_projectilePrefab, _projectileSpawn.position, spawnRot);
        projectile.GetComponent<Rigidbody>().AddForce(direction * _projectileSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyProjectile(projectile, _projectileLifeTime));
    }

    private IEnumerator DestroyProjectile(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }
}
