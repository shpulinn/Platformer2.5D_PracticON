using UnityEngine;

public class SwordColliderActivation : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;

    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }
    
    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }
}
