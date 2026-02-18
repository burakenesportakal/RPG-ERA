using UnityEngine;

public class Chest : MonoBehaviour, IDamageble
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private BoxCollider2D boxCollider;
    private Animator animator => GetComponentInChildren<Animator>();
    private Entity_VFX chestVFX => GetComponent<Entity_VFX>();

    [SerializeField] Vector2 chestOpeningVelocity;
    bool isOpened = false;
    int hitcount = 1;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool TakeDamage(float damage, Transform damageDealer)
    {
        Vector2 chestColliderSize = boxCollider.size;
        Vector2 chestColliderOffset = boxCollider.offset;
        float originalHeight = chestColliderSize.y;
        

        if (isOpened)
        {
            rb.linearVelocity = new Vector2(2, 4);
            chestVFX.PlayOnDamageVFX();
            hitcount++;
            if (hitcount == 5) {
                animator.SetBool("break", true);
                chestColliderSize.y *= .5f;
                boxCollider.size = chestColliderSize;
                chestColliderOffset.y -= (originalHeight - chestColliderSize.y) / 2;
                boxCollider.offset = chestColliderOffset;
                chestVFX.PlayOnDamageVFX();
                rb.linearVelocity = chestOpeningVelocity;
            }
            return true;
        }

        chestVFX.PlayOnDamageVFX();
        isOpened = true;
        animator.SetBool("chestOpen", true);
        rb.linearVelocity = chestOpeningVelocity;
        return true;
    }
}
