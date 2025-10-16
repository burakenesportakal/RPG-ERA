using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity entity;

    void Awake()
    {
        entity = GetComponentInParent<Player>();
    }
    public void CurrentStateTrigger()
    {
        entity.CallAnimationTrigger();
    }
}
