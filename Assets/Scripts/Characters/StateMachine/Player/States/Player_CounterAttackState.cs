using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_CombatState combatState;
    private bool countered;

    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combatState = player.GetComponent<Player_CombatState>();
    }

    public override void Enter()
    {
        base.Enter();

        countered = combatState.CounterAttackPerformed();
        anim.SetBool("counterAttackPerformed", countered);
        stateTimer = combatState.GetCounterAttackRecoveryDuration();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.linearVelocity.y);
        
        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);

        if (stateTimer < 0 && countered == false)
            stateMachine.ChangeState(player.idleState);
    }
}
