using UnityEngine;

public class Chest : MonoBehaviour, IDamageble
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Animator animator => GetComponentInChildren<Animator>();
    private Entity_VFX chestVFX => GetComponent<Entity_VFX>();
    [SerializeField] Vector2 chestOpeningVelocity;
    bool isOpened = false;
    int hitcount;
    public void TakeDamage(float damage, Transform damageDealer)
    {
        if (isOpened)
        {
            hitcount++;
            if (hitcount == 5) {
                animator.SetBool("break", true);
                chestVFX.PlayOnDamageVFX();
                rb.linearVelocity = chestOpeningVelocity;
            }
            return;
        }

        chestVFX.PlayOnDamageVFX();
        isOpened = true;
        animator.SetBool("chestOpen", true);
        rb.linearVelocity = chestOpeningVelocity;
    }
}
