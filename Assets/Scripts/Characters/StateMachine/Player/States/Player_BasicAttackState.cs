using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;

    private const int FirstComboIndex = 0;
    private int attackDirection;
    private int comboIndex = 0;
    private int comboLimit = 2;
    private float lastTimeAttacked;
    private bool comboAttackQueued;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit + 1 != player.attackVelocity.Length)
        {
            comboLimit = player.attackVelocity.Length;
        }
    }
    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ComboIndexResetter();

        attackDirection = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDirection;

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();
    
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;

    }
    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
    }


    private void ComboIndexResetter()
    {
        if (comboIndex > comboLimit || Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex];

        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);    
    }
}
