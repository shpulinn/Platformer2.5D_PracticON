using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private int damageValue;

    public int GetDamage => damageValue;

    private void OnTriggerEnter(Collider other)
    {
        // TakeDamageCollider takeDamageCollider = other.GetComponent<TakeDamageCollider>();
        // if (takeDamageCollider)
        // {
        //     takeDamageCollider.TakeDamage(damageValue);
        // }
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damageValue);
            
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
    }

    public void HandleDeath()
    {
        Transform parent = transform.parent;
        parent.parent = null;
        BoxCollider boxCol = GetComponent<BoxCollider>();
        Rigidbody rb = GetComponentInParent<Rigidbody>();
        boxCol.isTrigger = false;
        boxCol.enabled = true;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }
}
