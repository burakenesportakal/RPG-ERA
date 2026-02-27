using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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

    [Header("Element Colors")]
    [SerializeField] private Color IceEffectVFXColor = Color.cyan;
    [SerializeField] private Color FireEffectVFXColor = Color.red;
    private Color originalHitVFXColor;
    private Color originalCritHitVFXColor;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        entity = GetComponent<Entity>();
        originalMaterial = sr.sharedMaterial;
        originalHitVFXColor = hitVFXColor;
        originalCritHitVFXColor = critHitVFXColor;
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

    public void UpdateOnHitColor(ElementType elementType)
    {
        if (elementType == ElementType.Ice)
        {
            hitVFXColor = IceEffectVFXColor;
            critHitVFXColor = IceEffectVFXColor;
        }
        if (elementType == ElementType.None)
        {
            hitVFXColor = originalHitVFXColor;
            critHitVFXColor = originalCritHitVFXColor;
        }
    }

    public void PlayOnStatusEffectVFX(float duration, ElementType elementType)
    {
        if (elementType == ElementType.Ice)
            StartCoroutine(PlayStatusEffectVFXCoroutine(duration, IceEffectVFXColor));
        if (elementType == ElementType.Fire)
            StartCoroutine(PlayStatusEffectVFXCoroutine(duration, FireEffectVFXColor));
    }

    private IEnumerator PlayStatusEffectVFXCoroutine( float duration, Color effectColor)
    {
        float tickInterval = .2f;
        float timer = 0;

        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * .8f;

        bool toggle = false;

        while (timer < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(tickInterval);
            timer = timer + tickInterval;
        }

        sr.color = Color.white;
    }
}
