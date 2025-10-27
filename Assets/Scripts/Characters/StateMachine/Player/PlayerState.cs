using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState : Entity_State
{
    protected Player player;
    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine,animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }
    public override void Update()
    {
        base.Update();
        
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);

    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private bool CanDash()
    {
        if (player.wallDetected || stateMachine.currentState == player.dashState || stateMachine.currentState == player.basicAttackState)
            return false;
        return true;
    }
}
