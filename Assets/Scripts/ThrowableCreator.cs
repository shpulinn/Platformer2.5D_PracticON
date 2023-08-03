using UnityEngine;

public class ThrowableCreator : MonoBehaviour
{
    [SerializeField] private GameObject fakeProjectileInHand;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float throwForce = 10f;

    private Rigidbody _rb;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    public void SpawnProjectile()
    {
        HideFakeProjectileInHand();
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        _rb = projectile.GetComponent<Rigidbody>();

        Vector3 toPlayer = (_playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position,_playerTransform.position);
        throwForce = distanceToPlayer > 7 ? 10f : 3f;
        _rb.velocity = toPlayer * throwForce;
    }

    public void ShowFakeProjectileInHand()
    {
        fakeProjectileInHand.SetActive(true);
    }

    public void HideFakeProjectileInHand()
    {
        fakeProjectileInHand.SetActive(false);
    }
}
