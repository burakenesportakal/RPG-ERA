using UnityEngine;
using System.Collections;


public class Enemy_AttackAlertFade : MonoBehaviour
{
    // SpriteRenderer referans� (sprite��n g�rselini kontrol etmek i�in)
    private SpriteRenderer spriteRenderer;

    // Fade efektinin s�resi (Inspector�dan ayarlanabilir)
    [SerializeField] private float fadeDuration = 0.3f;

    private void Awake()
    {
        // Nesne aktif oldu�unda SpriteRenderer bile�enini bul
        // GetComponentInChildren: e�er SpriteRenderer alt nesnede ise onu da bulur
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // Nesne her aktif oldu�unda fade-in coroutine'ini ba�lat
        StartCoroutine(FadeInCoroutine());
    }

    // Sprite�� yava��a g�r�n�r hale getiren coroutine
    private IEnumerator FadeInCoroutine()
    {
        // Sprite��n mevcut rengini al ve alfa (�effafl�k) de�erini 0 yap
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        float elapsed = 0f;

        // Belirtilen s�re boyunca (fadeDuration) alfa de�erini 0'dan 1'e art�r
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; // Ge�en zaman� art�r
            float t = Mathf.Clamp01(elapsed / fadeDuration); // 0�1 aras� normalle�tirilmi� s�re

            // SmoothStep: ge�i�i daha yumu�ak hale getirir (ani de�i�im olmaz)
            color.a = Mathf.SmoothStep(0f, 1f, t);

            // SpriteRenderer��n rengini g�ncelle
            spriteRenderer.color = color;

            // Bir sonraki frame�e kadar bekle (animasyonun ak�c� g�r�nmesini sa�lar)
            yield return null;
        }

        // D�ng� tamamlan�nca alfa de�erini tamamen opak yap (g�venlik i�in)
        color.a = 1f;
        spriteRenderer.color = color;
    }
}
