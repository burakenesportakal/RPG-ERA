using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Random Rotation")]
    [SerializeField] private float minRotate = 0;
    [SerializeField] private float maxRotate = 360;


    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;    
    [Space]
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;
    void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRotation();

        if (autoDestroy)
            Destroy(gameObject, destroyDelay);
    }

    private void ApplyRandomOffset()
    {
        if (randomOffset == false) return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position = transform.position + new Vector3(xOffset, yOffset); 
    }

    private void ApplyRandomRotation()
    {
        if (randomRotation == false) return;

        float zRotation = Random.Range(minRotate,maxRotate);
        transform.Rotate(0,0,zRotation);
    }
}
