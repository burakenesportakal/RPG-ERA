using UnityEngine;

public class Player_CombatState : Entity_Combat
{
    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecovery;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCountered = false;  

        foreach(var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();
            
            if (counterable == null) continue;
            
            if(counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasPerformedCountered = true;
            }
        }

        return hasPerformedCountered;
    }

    public float GetCounterAttackRecoveryDuration() => counterRecovery;
}