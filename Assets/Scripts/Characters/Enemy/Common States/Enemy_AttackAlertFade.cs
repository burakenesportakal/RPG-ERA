using UnityEngine;
using System.Collections;


public class Enemy_AttackAlertFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float fadeDuration = 0.3f;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; 
            float t = Mathf.Clamp01(elapsed / fadeDuration); 

            color.a = Mathf.SmoothStep(0f, 1f, t);

            spriteRenderer.color = color;

            yield return null;
        }

        color.a = 1f;
        spriteRenderer.color = color;
    }
}
