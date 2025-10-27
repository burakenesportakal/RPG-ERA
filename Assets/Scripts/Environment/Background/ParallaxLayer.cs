using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMutliplier;
    [SerializeField] private float imageWidthOffset = 10f;
    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMoveCamera)
    {
        background.position += Vector3.right * (distanceToMoveCamera * parallaxMutliplier);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
    }
}
