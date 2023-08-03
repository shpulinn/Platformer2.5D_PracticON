using UnityEngine;

public class HealingPickUp : MonoBehaviour
{
    [SerializeField] private int healingAmount = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            if (playerHealth.Health < playerHealth.MaxHealth)
            {
                playerHealth.TakeHealing(healingAmount);
                Destroy(gameObject);
            }
        }
    }
}
