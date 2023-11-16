/**
 * Simple character controller class that handles 
 * main techniques to improve input response and 
 * controllability.
*/

using Godot;
using System;
using System.ComponentModel;

public partial class CharacterController : CharacterBody2D
{
    private CharacterInput input;
    private Vector2 currentVelocity;

    public override void _Ready()
    {
        input = new CharacterInput();
        currentVelocity = Vector2.Zero;

        // pre-compute jump variables on start
        InitJumpStats();
    }

    public override void _PhysicsProcess(double delta)
    {
        ComputeWalk(delta);
        ComputeGravity(delta);
        ComputeJump(delta);
        Dash(delta);

        ApplyMovement();
    }

    #region Walk
    [ExportGroup("Movement")]
    [ExportSubgroup("Walk")]
    [Export]
    private float maxSpeed = 10f,
        maxAcceleration = 2f,
        maxAirAcceleration = 1f;
    
    private float currentAcceleration;
    private float targetHVelocity;

    private void ComputeWalk(double delta)
    {
        if (!isDashing)
        {
            float hDir = input.RetriveMoveInput();

            // get current velocity
            currentVelocity.X = Velocity.X;
            // set max acceleration, slower acc while in air
            currentAcceleration = IsOnFloor() ? maxAcceleration : maxAirAcceleration;
            // compute target velocity
            targetHVelocity = hDir * maxSpeed / (float)delta;

            // interpolate between current velocity and target velocity, with acceleration as step
            currentVelocity.X = Mathf.MoveToward(currentVelocity.X, targetHVelocity, currentAcceleration);
        }
        else {
            currentVelocity.X *= dashSpeedMultiplier;
        }
    }
    #endregion 

    #region Gravity
    private float GetGravity()
    {
        return Velocity.Y < 0f ? jumpGravity : fallGravity;
    }

    private void ComputeGravity(double delta)
    {
        if (!isDashing)
        {
            currentVelocity.Y = Velocity.Y;
            currentVelocity.Y += GetGravity() * (float)delta;
        }
        else
        {
            currentVelocity.Y = 0f;
        }
    }
    #endregion

    #region Jump
    [ExportGroup("Movement")]
    [ExportSubgroup("Jump")]
    [Export]
    private float jumpHeight = 1f, 
        timeToPeak = 0.5f, 
        timeToFall = 0.5f,
        jumpBufferLimit = 0.2f,
        coyoteTime = 0.2f,
        jumpHeightScale = 2.0f;
    [Export]
    private int maxAirJumps = 1;

    private int jumpPhase = 0;
    private float jumpVelocity, jumpGravity, fallGravity;
    private bool jumpBuffer = false, canJump = false;
    private float jumpBufferTime;
    private float coyoteTimeCount;
    private float jumpHeightCount;

    private void InitJumpStats()
    {
        jumpVelocity = -(2.0f * jumpHeight) / timeToPeak;
        //jumpGravity = 2.0f * jumpHeight / (timeToPeak * timeToPeak);
        fallGravity = 2.0f * jumpHeight / (timeToFall * timeToFall);
        jumpBufferTime = jumpBufferLimit;
    }

    private void ComputeJump(double delta)
    {
        bool isOnFloor = IsOnFloor();
        
        jumpBuffer = input.RetriveJumpInput();   

        // reset jump vars
        if (isOnFloor) 
        {
            jumpPhase = 0;
            jumpGravity = 2.0f * jumpHeight / (timeToPeak * timeToPeak);   
            jumpHeightCount = 0f;
        }

        // coyote time
        coyoteTimeCount = isOnFloor ? coyoteTime : coyoteTimeCount - (float)delta; 
        if (coyoteTimeCount <= 0f) jumpPhase = maxAirJumps;

        // change jumpGravity if released before reaching the peak
        if (!jumpBuffer && currentVelocity.Y < 0f) 
            jumpGravity = 2.0f * jumpHeight / (jumpHeightCount * timeToPeak);

        // jump button pressed
        if (jumpBuffer)
        {
            // jump height
            if (jumpHeightCount < timeToPeak) jumpHeightCount += (float)delta;

            // jump buffering
            if (jumpBufferTime > 0f)
            {
                jumpBufferTime -= (float)delta;
            }
            else
            {
                jumpBufferTime = jumpBufferLimit;
                jumpBuffer = false;
                return;
            }

            // set jump speed
            if (canJump && jumpPhase < maxAirJumps)
            {
                canJump = false;
                jumpBuffer = false;
                jumpPhase += 1;
                coyoteTimeCount = 0f;
                currentVelocity.Y = jumpVelocity;
            }
        }
        else 
        {
            canJump = true;
        }
    }
    #endregion

    #region Dash
    [ExportGroup("Movement")]
    [ExportSubgroup("Dash")]
    [Export]
    private float dashTime = 0.5f,
        dashSpeedMultiplier = 1.1f,
        dashCooldown = 0.5f;
    private float dashTimeCount, dashCooldownCount;
    private bool dashBuffer, isDashing;
    public void Dash(double delta)
    {
        dashBuffer = input.RetriveDashInput();

        if (dashCooldownCount > 0f)
        {
            dashCooldownCount -= (float)delta;
            return;
        }

        if (isDashing)
        {
            dashTimeCount -= (float)delta;
            // end of dash
            if (dashTimeCount < 0f) 
            {
                dashCooldownCount = dashCooldown; 
                isDashing = false;
            }
        }

        if (dashBuffer)
        {
            if (!isDashing && Velocity.X != 0f)
            {
                isDashing = true;
                dashTimeCount = dashTime;
            }
        }
    }
    #endregion

    #region ApplyMovement
    private void ApplyMovement()
    {
        Velocity = currentVelocity;
        MoveAndSlide();
    }
    #endregion
}
