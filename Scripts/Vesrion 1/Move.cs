using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Move : CharacterBody2D
{
    
}


// public partial class Move : CharacterBody2D
// {
// 	private CharacterController input;
// 	[ExportGroup("Movement")]
// 	[Export]
// 	//private float maxSpeed = 200f,
// 	//	maxAcceleration = 1000f,
// 	//	maxAirAcceleration = 1000f;
// 	[ExportGroup("Jump")]
// 	[Export]
// 	private float jumpHeight = 3f,
// 		downwardMovementMultiplier = 3f,
// 		upwardMovementMultiplier = 1.7f;
// 	[Export]
// 	private int maxAirJumps = 1;
// 	[ExportGroup("Misc")]
// 	[Export]
// 	private float gravity = 100f;

// 	private Vector2 direction, velocity;
// 	private float desiredVelocity, maxSpeedChange, acceleration;

// 	private int jumpPhase;
// 	private float defaultGravityScale;
// 	private bool desiredJump;

// 	private float gravityScale;

// 	public override void _Ready()
// 	{
// 		input = new CharacterController();
// 		defaultGravityScale = 1f;
// 		gravityScale = defaultGravityScale;
// 	}

// 	public override void _Process(double delta)
// 	{
// 		// compute desiredVelocity vector
// 		//direction.X = input.RetriveMoveInput();
// 		desiredVelocity = direction.X * maxSpeed;

// 		// jump handling
// 		// allow to mantain the jump input between frames and need to be manually disabled
// 		//desiredJump |= input.RetriveJumpInput();
// 	}

// 	public override void _PhysicsProcess(double delta)
// 	{
// 		velocity = Velocity;

// 		// add gravity
// 		velocity.Y = !IsOnFloor() ? velocity.Y + gravity * gravityScale * (float)delta : 0f;

// 		// accelerate towards desiredVelocity
// 		acceleration = IsOnFloor() ? maxAcceleration : maxAirAcceleration;
// 		maxSpeedChange = acceleration * (float)delta;
// 		velocity.X = Mathf.MoveToward(velocity.X, desiredVelocity, maxSpeedChange);

// 		// handle jump
// 		if (IsOnFloor())
// 		{
// 			jumpPhase = 0;
// 		}

// 		if (desiredJump)
// 		{
// 			// check if a jump has been requested
// 			desiredJump = false;
// 			velocity = JumpAction(velocity);
// 		}

// 		if (velocity.Y > 0f)
// 		{
// 			gravityScale = downwardMovementMultiplier;
// 		}	
// 		else if (velocity.Y < 0f)
// 		{
// 			gravityScale = upwardMovementMultiplier;
// 		}
// 		else if (velocity.Y == 0f)
// 		{
// 			gravityScale = defaultGravityScale;
// 		}

// 		// apply velocty to the character controller
// 		Velocity = velocity;
// 		MoveAndSlide();
// 	}

// 	private Vector2 JumpAction(Vector2 velocity)
// 	{
// 		if (IsOnFloor() || jumpPhase < maxAirJumps)
// 		{
// 			float jumpVelocity = velocity.Y;
// 			jumpPhase += 1; // jump used

// 			// compute jump using jump height forumula
// 			float jumpSpeed = -Mathf.Sqrt(2f * gravity * jumpHeight);

// 			if (jumpVelocity < 0f)
// 			{
// 				jumpSpeed = Mathf.Max(jumpSpeed - jumpVelocity, 0f);
// 			}
// 			velocity.Y = jumpSpeed;
// 		}
// 		return velocity;
// 	}
// }

