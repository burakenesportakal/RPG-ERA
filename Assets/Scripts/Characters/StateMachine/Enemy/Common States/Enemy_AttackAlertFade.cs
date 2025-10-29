using UnityEngine;
using System.Collections;


public class Enemy_AttackAlertFade : MonoBehaviour
{
    // SpriteRenderer referansý (sprite’ýn görselini kontrol etmek için)
    private SpriteRenderer spriteRenderer;

    // Fade efektinin süresi (Inspector’dan ayarlanabilir)
    [SerializeField] private float fadeDuration = 0.3f;

    private void Awake()
    {
        // Nesne aktif olduðunda SpriteRenderer bileþenini bul
        // GetComponentInChildren: eðer SpriteRenderer alt nesnede ise onu da bulur
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // Nesne her aktif olduðunda fade-in coroutine'ini baþlat
        StartCoroutine(FadeInCoroutine());
    }

    // Sprite’ý yavaþça görünür hale getiren coroutine
    private IEnumerator FadeInCoroutine()
    {
        // Sprite’ýn mevcut rengini al ve alfa (þeffaflýk) deðerini 0 yap
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        float elapsed = 0f;

        // Belirtilen süre boyunca (fadeDuration) alfa deðerini 0'dan 1'e artýr
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; // Geçen zamaný artýr
            float t = Mathf.Clamp01(elapsed / fadeDuration); // 0–1 arasý normalleþtirilmiþ süre

            // SmoothStep: geçiþi daha yumuþak hale getirir (ani deðiþim olmaz)
            color.a = Mathf.SmoothStep(0f, 1f, t);

            // SpriteRenderer’ýn rengini güncelle
            spriteRenderer.color = color;

            // Bir sonraki frame’e kadar bekle (animasyonun akýcý görünmesini saðlar)
            yield return null;
        }

        // Döngü tamamlanýnca alfa deðerini tamamen opak yap (güvenlik için)
        color.a = 1f;
        spriteRenderer.color = color;
    }
}
