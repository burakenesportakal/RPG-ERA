using UnityEngine;

public class Skeleton : Enemy , ICounterable
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
        deadState = new Enemy_DeadState(this, stateMachine, "empty");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.F))
            HandleCounter();
    }
    public void HandleCounter()
    {
        stateMachine.ChangeState(stunnedState);
    }
}
