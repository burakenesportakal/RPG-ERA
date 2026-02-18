using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVFXCoroutine;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVFXColor = Color.white;
    [SerializeField] private Color critHitVFXColor = Color.white;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject critHitVFX;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        entity = GetComponent<Entity>();
        originalMaterial = sr.sharedMaterial;
    }

    public void CreateOnHitVFX(Transform target, bool isCrit)
    {
        GameObject hitPrefab = isCrit ? critHitVFX : hitVFX;
        GameObject onHitVFX = Instantiate(hitPrefab, target.position, Quaternion.identity);
        if (isCrit)
            onHitVFX.GetComponentInChildren<SpriteRenderer>().color = critHitVFXColor;
        else
            onHitVFX.GetComponentInChildren<SpriteRenderer>().color = hitVFXColor;

        if (entity.facingDirection == -1 && isCrit)
            onHitVFX.transform.Rotate(0, 180, 0);
    }

    public void PlayOnDamageVFX()
    {
        if (onDamageVFXCoroutine != null)
            StopCoroutine(onDamageVFXCoroutine);

       onDamageVFXCoroutine = StartCoroutine(OnDamageVFXCoroutine());
    }

    private IEnumerator OnDamageVFXCoroutine()
    {
        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVFXDuration);
        sr.material = originalMaterial;
    }
}
