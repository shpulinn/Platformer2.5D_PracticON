using Unity.Mathematics;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 3f;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private int damage;
    [SerializeField] private float explosionDamageRadius = 3f;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        Invoke(nameof(Explode), timeToDestroy);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionDamageRadius, layerMask, QueryTriggerInteraction.Ignore);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
        }
        if (explosionParticles)
        {
            Instantiate(explosionParticles, transform.position, quaternion.identity);
        }
        else
        {
            Debug.Log("No <b>particles</b> assigned to " + this.gameObject.name);
        }

        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionDamageRadius);
    }
}
